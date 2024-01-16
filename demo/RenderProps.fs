module Demo.Hook

open Feliz
open Browser
open Fable.Core.JsInterop
open Glutinum.Feliz.ReactIntersectionObserver
open type Glutinum.Feliz.ReactIntersectionObserver.Exports

let private classes: CssModules.style = importDefault "./style.module.css"

[<ReactComponent>]
let private Component () =
    let threshold, setThreshold = React.useState 0.25

    Html.div [
        prop.className classes.container
        prop.children [
            Html.div [
                prop.classes [
                    classes.block
                    classes.topBlock
                ]
                prop.children [
                    Html.div "Scroll down"
                    Html.br []
                    Html.input [
                        prop.type' "range"
                        prop.min 0
                        prop.max 1
                        prop.step 0.05
                        prop.value threshold
                        prop.onChange setThreshold
                    ]
                    Html.br []
                    Html.span [
                        prop.text (sprintf "Threshold: %.2f" threshold)
                    ]
                ]
            ]

            InView [
                inView.threshold threshold
                inView.children (fun props ->
                    Html.div [
                        prop.classes [
                            classes.block
                            classes.middleBlock
                            if props.inView then
                                classes.isInView
                        ]

                        prop.ref props.ref_case1
                        prop.text
                            $"Intersection block (is in view: %b{props.inView})"
                    ]
                )
            ]

            Html.div [
                prop.classes [
                    classes.block
                    classes.bottomBlock
                ]
                prop.text "Scroll up"
            ]
        ]
    ]

let root = document.getElementById ("root") |> ReactDOM.createRoot

root.render (Component())
