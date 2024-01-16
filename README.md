# Glutinum.Feliz.ReactIntersectionObserver

Binding for [react-intersection-observer](https://github.com/thebuilder/react-intersection-observer)

## Usage

### Hooks

```fs
open Feliz
open Glutinum.Feliz.ReactIntersectionObserver
open type Glutinum.Feliz.ReactIntersectionObserver.Exports

[<ReactComponent>]
let IntersectionBlock () =
    let inViewInfo =
        useInView (
            IntersectionOptions(
                threshold = 0.25,
                onChange =
                    (fun inView _ ->
                        printfn "Is in view: %b" inView
                    )
            )
        )

    Html.div [
        prop.ref inViewInfo.ref
        prop.text "Intersection block"
    ]
```

### Render Props

```fs
open Feliz
open Glutinum.Feliz.ReactIntersectionObserver
open type Glutinum.Feliz.ReactIntersectionObserver.Exports

InView [
    inView.threshold threshold
    inView.children (fun props ->
        Html.div [
            prop.ref props.ref_case1
            prop.text $"Intersection block (is in view: %b{props.inView})"
        ]
    )
]
```

### Plain child

```fs
open Feliz
open Glutinum.Feliz.ReactIntersectionObserver
open type Glutinum.Feliz.ReactIntersectionObserver.Exports

InView [
    inView.``as`` ReactIntersectionObserverAs.div
    inView.threshold 0.25
    inView.onChange (
        fun inView _ ->
            printfn "Is in view: %b" inView
    )
    inView.children (
        Html.div [
            prop.text "Intersection block"
        ]
    )
]
```
