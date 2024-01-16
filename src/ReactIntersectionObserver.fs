namespace rec Glutinum.Feliz.ReactIntersectionObserver

open Fable.Core
open Browser.Types
open Fable.React

[<AllowNullLiteral>]
type State =
    abstract inView: bool with get, set
    abstract entry: IntersectionObserverEntry option with get, set

[<Global>]
type IntersectionOptions [<ParamObject; Emit("$0")>] private () =

    [<ParamObject; Emit("$0")>]
    new(?root: Element,
        ?rootMargin: string,
        ?threshold: float,
        ?triggerOnce: bool,
        ?skip: bool,
        ?initialInView: bool,
        ?fallbackInView: bool,
        ?trackVisibility: bool,
        ?delay: float,
        ?onChange: (bool -> IntersectionObserverEntry -> unit)) =

        IntersectionOptions()

    [<ParamObject; Emit("$0")>]
    new(?root: Element,
        ?rootMargin: string,
        ?threshold: ResizeArray<float>,
        ?triggerOnce: bool,
        ?skip: bool,
        ?initialInView: bool,
        ?fallbackInView: bool,
        ?trackVisibility: bool,
        ?delay: float,
        ?onChange: (bool -> IntersectionObserverEntry -> unit)) =

        IntersectionOptions()

    // Should we activate this inheritance?
    // interface IntersectionObserverOptions with
    //     member this.root with get () = jsNative and set(_) = jsNative
    //     member this.rootMargin with get () = jsNative and set(_) = jsNative
    //     member this.threshold with get () = jsNative and set(_) = jsNative

    /// <summary>The IntersectionObserver interface's read-only <c>root</c> property identifies the Element or Document whose bounds are treated as the bounding box of the viewport for the element which is the observer's target. If the <c>root</c> is null, then the bounds of the actual document viewport are used.</summary>
    member val root: Element option = jsNative with get, set
    /// <summary>Margin around the root. Can have values similar to the CSS margin property, e.g. <c>10px 20px 30px 40px</c> (top, right, bottom, left).</summary>
    member val rootMargin: string option = jsNative with get, set

    /// <summary>Number between <c>0</c> and <c>1</c> indicating the percentage that should be visible before triggering. Can also be an <c>array</c> of numbers, to create multiple trigger points.</summary>
    member val threshold: U2<float, ResizeArray<float>> option =
        jsNative with get, set

    /// Only trigger the inView callback once
    member val triggerOnce: bool option = jsNative with get, set
    /// <summary>Skip assigning the observer to the <c>ref</c></summary>
    member val skip: bool option = jsNative with get, set
    /// <summary>Set the initial value of the <c>inView</c> boolean. This can be used if you expect the element to be in the viewport to start with, and you want to trigger something when it leaves.</summary>
    member val initialInView: bool option = jsNative with get, set
    /// Fallback to this inView state if the IntersectionObserver is unsupported, and a polyfill wasn't loaded
    member val fallbackInView: bool option = jsNative with get, set
    /// IntersectionObserver v2 - Track the actual visibility of the element
    member val trackVisibility: bool option = jsNative with get, set
    /// IntersectionObserver v2 - Set a minimum delay between notifications
    member val delay: float option = jsNative with get, set

    /// Call this function whenever the in view state changes
    member val onChange: (bool -> IntersectionObserverEntry -> unit) option =
        jsNative with get, set

[<Erase>]
type inView =

    /// <summary>The IntersectionObserver interface's read-only <c>root</c> property identifies the Element or Document whose bounds are treated as the bounding box of the viewport for the element which is the observer's target. If the <c>root</c> is null, then the bounds of the actual document viewport are used.</summary>
    static member inline root(element: Element) =
        Interop.mkReactResizeDetectorProperty "root" element

    /// <summary>Margin around the root. Can have values similar to the CSS margin property, e.g. <c>10px 20px 30px 40px</c> (top, right, bottom, left).</summary>
    static member inline rootMargin(margin: string) =
        Interop.mkReactResizeDetectorProperty "rootMargin" margin

    /// <summary>Number between <c>0</c> and <c>1</c> indicating the percentage that should be visible before triggering. Can also be an <c>array</c> of numbers, to create multiple trigger points.</summary>
    static member inline threshold(threshold: float) =
        Interop.mkReactResizeDetectorProperty "threshold" threshold

    /// <summary>Number between <c>0</c> and <c>1</c> indicating the percentage that should be visible before triggering. Can also be an <c>array</c> of numbers, to create multiple trigger points.</summary>
    static member inline threshold(threshold: ResizeArray<float>) =
        Interop.mkReactResizeDetectorProperty "threshold" threshold

    /// Only trigger the inView callback once
    static member inline triggerOnce(triggerOnce: bool) =
        Interop.mkReactResizeDetectorProperty "triggerOnce" triggerOnce

    /// <summary>Skip assigning the observer to the <c>ref</c></summary>
    static member inline skip(skip: bool) =
        Interop.mkReactResizeDetectorProperty "skip" skip

    /// <summary>Set the initial value of the <c>inView</c> boolean. This can be used if you expect the element to be in the viewport to start with, and you want to trigger something when it leaves.</summary>
    static member inline initialInView(initialInView: bool) =
        Interop.mkReactResizeDetectorProperty "initialInView" initialInView

    /// Fallback to this inView state if the IntersectionObserver is unsupported, and a polyfill wasn't loaded
    static member inline fallbackInView(fallbackInView: bool) =
        Interop.mkReactResizeDetectorProperty "fallbackInView" fallbackInView

    /// IntersectionObserver v2 - Track the actual visibility of the element
    static member inline trackVisibility(trackVisibility: bool) =
        Interop.mkReactResizeDetectorProperty "trackVisibility" trackVisibility

    /// IntersectionObserver v2 - Set a minimum delay between notifications
    static member inline delay(delay: float) =
        Interop.mkReactResizeDetectorProperty "delay" delay

    /// Call this function whenever the in view state changes
    static member inline onChange
        (onChange: (bool -> IntersectionObserverEntry -> unit))
        =
        Interop.mkReactResizeDetectorProperty
            "onChange"
            // Force fable to generate the correct function type
            (System.Func<_, _, _>(onChange))

    /// <summary>
    /// Children expects a function that receives an object
    /// contain an <c>inView</c> boolean and <c>ref</c> that should be
    /// assigned to the element root.
    /// </summary>
    static member inline children(children: (RenderProps -> ReactElement)) =
        Interop.mkReactResizeDetectorProperty "children" children

    static member inline children(children: ReactElement) =
        Interop.mkReactResizeDetectorProperty "children" children

    /// <summary>
    /// Render the wrapping element as this element.
    /// This needs to be an intrinsic element.
    /// If you want to use a custom element, please use the useInView
    /// hook to manage the ref explicitly.
    /// </summary>
    static member inline ``as``(value: ReactIntersectionObserverAs) =
        Interop.mkReactResizeDetectorProperty "as" value

// Should PlainChildrenProps be their own type to allows using standard HTML attributes?
/// Call this function whenever the in view state changes
// static member inline onChange (onChange: (bool -> IntersectionObserverEntry -> unit)) =
//     Interop.mkReactResizeDetectorProperty "onChange" onChange

[<RequireQualifiedAccess>]
[<StringEnum(CaseRules.None)>]
type ReactIntersectionObserverAs =
    | a
    | abbr
    | address
    | area
    | article
    | aside
    | audio
    | b
    | ``base``
    | bdi
    | bdo
    | big
    | blockquote
    | body
    | br
    | button
    | canvas
    | caption
    | center
    | cite
    | code
    | col
    | colgroup
    | data
    | datalist
    | dd
    | del
    | details
    | dfn
    | dialog
    | div
    | dl
    | dt
    | em
    | embed
    | fieldset
    | figcaption
    | figure
    | footer
    | form
    | h1
    | h2
    | h3
    | h4
    | h5
    | h6
    | head
    | header
    | hgroup
    | hr
    | html
    | i
    | iframe
    | img
    | input
    | ins
    | kbd
    | keygen
    | label
    | legend
    | li
    | link
    | main
    | map
    | mark
    | menu
    | menuitem
    | meta
    | meter
    | nav
    | noindex
    | noscript
    | ``object``
    | ol
    | optgroup
    | option
    | output
    | p
    | param
    | picture
    | pre
    | progress
    | q
    | rp
    | rt
    | ruby
    | s
    | samp
    | search
    | slot
    | script
    | section
    | select
    | small
    | source
    | span
    | strong
    | style
    | sub
    | summary
    | sup
    | table
    | template
    | tbody
    | td
    | textarea
    | tfoot
    | th
    | thead
    | time
    | title
    | tr
    | track
    | u
    | ul
    | [<CompiledName("var")>] var
    | video
    | wbr
    | webview
    | svg
    | animate
    | animateMotion
    | animateTransform
    | circle
    | clipPath
    | defs
    | desc
    | ellipse
    | feBlend
    | feColorMatrix
    | feComponentTransfer
    | feComposite
    | feConvolveMatrix
    | feDiffuseLighting
    | feDisplacementMap
    | feDistantLight
    | feDropShadow
    | feFlood
    | feFuncA
    | feFuncB
    | feFuncG
    | feFuncR
    | feGaussianBlur
    | feImage
    | feMerge
    | feMergeNode
    | feMorphology
    | feOffset
    | fePointLight
    | feSpecularLighting
    | feSpotLight
    | feTile
    | feTurbulence
    | filter
    | foreignObject
    | g
    | image
    | line
    | linearGradient
    | marker
    | mask
    | metadata
    | mpath
    | path
    | pattern
    | polygon
    | polyline
    | radialGradient
    | rect
    | stop
    | switch
    | symbol
    | text
    | textPath
    | tspan
    | ``use``
    | view

[<AllowNullLiteral>]
type RenderProps =
    abstract inView: bool with get, set
    abstract entry: IntersectionObserverEntry option with get, set
    abstract ref: U2<IRefValue<obj option>, ((Element) option -> unit)> with get, set

    [<Emit("$0.ref")>]
    abstract ref_case1: IRefValue<Browser.Types.HTMLElement option> with get, set

    [<Emit("$0.ref")>]
    abstract ref_case2: ((Element) option -> unit) with get, set

// module InViewHookResponse =

//     type ArrayDestructing =
//         ((Element) option -> unit) * bool * IntersectionObserverEntry option

//     type ObjectDestructing =
//         abstract ref: ((Element) option -> unit) with get, set
//         abstract inView: bool with get, set
//         abstract entry: IntersectionObserverEntry option with get, set

// [<Erase>]
// type InViewHookResponse =
//     | Array of InViewHookResponse.ArrayDestructing
//     | Object of InViewHookResponse.ObjectDestructing

type InViewHookResponse =
    abstract ref: (Element -> unit) with get, set
    abstract inView: bool with get, set
    abstract entry: IntersectionObserverEntry option with get, set
