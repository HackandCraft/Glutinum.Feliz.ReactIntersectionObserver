#r "nuget: Fun.Build, 1.0.5"

open Fun.Build
open System.IO

type PipelineBuilder with

    [<CustomOperation "collapseGithubActionLogs">]
    member inline this.collapseGithubActionLogs(build: Internal.BuildPipeline) =
        let build =
            this.runBeforeEachStage (
                build,
                (fun ctx ->
                    if ctx.GetStageLevel() = 0 then
                        printfn $"::group::{ctx.Name}"
                )
            )

        this.runAfterEachStage (
            build,
            (fun ctx ->
                if ctx.GetStageLevel() = 0 then
                    printfn "::endgroup::"
            )
        )

let options = {|
    GITHUB_ACTION =
        EnvArg.Create(
            "GITHUB_ACTION",
            description = "Run only in github action container"
        )
    HNC_NUGET_API_KEY =
        EnvArg.Create("HNC_NUGET_API_KEY", description = "NuGet API key")
|}

module Stages =

    let femtoValidation =
        stage "Femto - Validation" { run "dotnet femto --validate src" }

    let lint =
        stage "Lint" {
            stage "Format" {
                whenNot { envVar options.GITHUB_ACTION }
                run "dotnet fantomas src demo build.fsx"
            }

            stage "Check" {
                whenEnvVar options.GITHUB_ACTION
                run "dotnet fantomas --check src demo build.fsx"
            }
        }

pipeline "Watch" {
    description "Run the demo project and watch for changes"

    stage "Clean" {
        run (fun _ ->
            [|
                Directory.GetFiles(
                    "src",
                    "*.fs.js",
                    SearchOption.AllDirectories
                )
                Directory.GetFiles(
                    "demo",
                    "*.fs.js",
                    SearchOption.AllDirectories
                )
                [|
                    "./demo/CssModules.fs"
                |]
            |]
            |> Array.concat
            |> Array.iter File.Delete
        )
    }

    stage "Restore dependencies" {
        run "dotnet restore src"
        run "dotnet restore demo"
        run "pnpm install"
        run "dotnet husky install"
    }

    stage "Pre-build CssBindings" {
        workingDir "demo"
        run "npx fcm"
    }

    stage "Watch" {
        workingDir "demo"
        paralle

        // Verbose is required here because Fable seems to be doing some strange
        // in the console writting now. Investigation is in progress.
        run "dotnet fable --watch --sourceMaps --noCache --verbose"
        run "npx vite"
        run "npx nodemon -e module.css --exec npx fcm"
    }

    runIfOnlySpecified
}

pipeline "Lint" {
    Stages.lint
    Stages.femtoValidation

    runIfOnlySpecified
}

pipeline "Publish" {
    description "Build and push to NuGet"

    whenEnvVar options.GITHUB_ACTION
    whenEnvVar options.HNC_NUGET_API_KEY
    collapseGithubActionLogs

    Stages.lint
    Stages.femtoValidation

    stage "Build" { run "dotnet pack -c Release src" }

    stage "Publish packages to NuGet" {
        workingDir "src/bin/Release"

        run (fun ctx ->
            let key = ctx.GetEnvVar options.HNC_NUGET_API_KEY.Name

            ctx.RunSensitiveCommand
                $"""dotnet nuget push *.nupkg -s https://api.nuget.org/v3/index.json --skip-duplicate -k {key}"""
        )
    }

    runIfOnlySpecified
}

tryPrintPipelineCommandHelp ()
