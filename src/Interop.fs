namespace Glutinum.Feliz.ReactIntersectionObserver

module Interop =

    let inline mkReactResizeDetectorProperty
        (key: string)
        (value: obj)
        : IReactIntersectionObserverProperty
        =
        unbox (key, value)
