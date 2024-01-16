namespace Glutinum.Feliz.ReactIntersectionObserver

open Feliz
open Fable.Core
open Fable.Core.JsInterop

[<Erase>]
type Exports =

    [<Hook; Import("useInView", "react-intersection-observer")>]
    static member useInView<'T>
        (?props: IntersectionOptions)
        : InViewHookResponse
        =
        jsNative

    static member inline InView
        (properties: #IReactIntersectionObserverProperty list)
        =
        Interop.reactApi.createElement (
            import "InView" "react-intersection-observer",
            createObj !!properties
        )
