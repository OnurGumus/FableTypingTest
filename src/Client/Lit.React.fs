namespace Lit

open System
open Fable.Core
open Fable.Core.JsInterop
open Fable.React
open Browser.Types

/// <summary>
/// Directive that allows a react component to be rendered inside a Lit template.
/// </summary>
[<AttachMembers>]
type ReactDirective() =
    inherit Types.AsyncDirective()

    let mutable _domEl = Unchecked.defaultof<Element>
    let mutable _reactRoot = Unchecked.defaultof<_>

    member _.className = ""
    member _.renderFn = Unchecked.defaultof<obj -> ReactElement>

    member this.render(props: obj) =
        Lit.html $"""<div class={this.className} {Lit.refCallback(function
            | Some el when this.isConnected ->
                if(_reactRoot = Unchecked.defaultof<_>) then
                    _reactRoot <- ReactDomClient.createRoot(el)
                _domEl <- el
                let reactEl = this.renderFn props
                _reactRoot.render(reactEl)
            | _ -> ()
        )}></div>"""

    member _.disconnected() =
        if not(isNull _domEl) then
            ReactDom.unmountComponentAtNode(_domEl) |> ignore
type Prop =
    | Key of string
    | Ref of (Element->unit)
    /// To be used in combination with `useRef` hook
    | [<System.Obsolete("Use RefValue")>] [<CompiledName("ref")>] RefHook of IRefHook<Element option>
    | [<CompiledName("ref")>] RefValue of IRefValue<Element option>
    interface IHTMLProp

type HTMLAttr =
    | DefaultChecked of bool
    | DefaultValue of obj
    | Accept of string
    | AcceptCharset of string
    | AccessKey of string
    | Action of string
    | AllowFullScreen of bool
    | AllowTransparency of bool
    | Alt of string
    | [<CompiledName("aria-atomic")>] AriaAtomic of bool
    | [<CompiledName("aria-busy")>] AriaBusy of bool
    | [<CompiledName("aria-checked")>] AriaChecked of bool
    | [<CompiledName("aria-colcount")>] AriaColcount of int
    | [<CompiledName("aria-colindex")>] AriaColindex of int
    | [<CompiledName("aria-colspan")>] AriaColspan of int
    | [<CompiledName("aria-controls")>] AriaControls of string
    | [<CompiledName("aria-current")>] AriaCurrent of string
    | [<CompiledName("aria-describedby")>] AriaDescribedBy of string
    | [<CompiledName("aria-details")>] AriaDetails of string
    | [<CompiledName("aria-disabled")>] AriaDisabled of bool
    | [<CompiledName("aria-errormessage")>] AriaErrormessage of string
    | [<CompiledName("aria-expanded")>] AriaExpanded of bool
    | [<CompiledName("aria-flowto")>] AriaFlowto of string
    | [<CompiledName("aria-haspopup")>] AriaHasPopup of bool
    | [<CompiledName("aria-hidden")>] AriaHidden of bool
    | [<CompiledName("aria-invalid")>] AriaInvalid of string
    | [<CompiledName("aria-keyshortcuts")>] AriaKeyshortcuts of string
    | [<CompiledName("aria-label")>] AriaLabel of string
    | [<CompiledName("aria-labelledby")>] AriaLabelledby of string
    | [<CompiledName("aria-level")>] AriaLevel of int
    | [<CompiledName("aria-live")>] AriaLive of string
    | [<CompiledName("aria-modal")>] AriaModal of bool
    | [<CompiledName("aria-multiline")>] AriaMultiline of bool
    | [<CompiledName("aria-multiselectable")>] AriaMultiselectable of bool
    | [<CompiledName("aria-orientation")>] AriaOrientation of string
    | [<CompiledName("aria-owns")>] AriaOwns of string
    | [<CompiledName("aria-placeholder")>] AriaPlaceholder of string
    | [<CompiledName("aria-posinset")>] AriaPosinset of int
    | [<CompiledName("aria-pressed")>] AriaPressed of bool
    | [<CompiledName("aria-readonly")>] AriaReadonly of bool
    | [<CompiledName("aria-relevant")>] AriaRelevant of string
    | [<CompiledName("aria-required")>] AriaRequired of bool
    | [<CompiledName("aria-roledescription")>] AriaRoledescription of string
    | [<CompiledName("aria-rowcount")>] AriaRowcount of int
    | [<CompiledName("aria-rowindex")>] AriaRowindex of int
    | [<CompiledName("aria-rowspan")>] AriaRowspan of int
    | [<CompiledName("aria-selected")>] AriaSelected of bool
    | [<CompiledName("aria-setsize")>] AriaSetsize of int
    | [<CompiledName("aria-sort")>] AriaSort of string
    | [<CompiledName("aria-valuemax")>] AriaValuemax of float
    | [<CompiledName("aria-valuemin")>] AriaValuemin of float
    | [<CompiledName("aria-valuenow")>] AriaValuenow of float
    | [<CompiledName("aria-valuetext")>] AriaValuetext of string
    | Async of bool
    | AutoComplete of string
    | AutoFocus of bool
    | AutoPlay of bool
    | Capture of bool
    | CellPadding of obj
    | CellSpacing of obj
    | CharSet of string
    | Challenge of string
    | Checked of bool
    | ClassID of string
    | ClassName of string
    /// Alias of ClassName
    | [<CompiledName("className")>] Class of string
    | Cols of int
    | ColSpan of int
    | Content of string
    | ContentEditable of bool
    | ContextMenu of string
    | Controls of bool
    | Coords of string
    | CrossOrigin of string
    // | Data of string
    | [<CompiledName("data-toggle")>] DataToggle of string
    | DateTime of string
    | Default of bool
    | Defer of bool
    | Dir of string
    | Disabled of bool
    | Download of obj
    | Draggable of bool
    | EncType of string
    | Form of string
    | FormAction of string
    | FormEncType of string
    | FormMethod of string
    | FormNoValidate of bool
    | FormTarget of string
    | FrameBorder of obj
    | Headers of string
    | Height of obj
    | Hidden of bool
    | High of float
    | Href of string
    | HrefLang of string
    | HtmlFor of string
    | HttpEquiv of string
    | Icon of string
    | Id of string
    | InputMode of string
    | Integrity of string
    | Is of string
    | KeyParams of string
    | KeyType of string
    | Kind of string
    | Label of string
    | Lang of string
    | List of string
    | Loop of bool
    | Low of float
    | Manifest of string
    | MarginHeight of float
    | MarginWidth of float
    | Max of obj
    | MaxLength of float
    | Media of string
    | MediaGroup of string
    | Method of string
    | Min of obj
    | MinLength of float
    | Multiple of bool
    | Muted of bool
    | Name of string
    | NoValidate of bool
    | Open of bool
    | Optimum of float
    | Pattern of string
    | Placeholder of string
    | Poster of string
    | Preload of string
    | RadioGroup of string
    | ReadOnly of bool
    | Rel of string
    | Required of bool
    | Role of string
    | Rows of int
    | RowSpan of int
    | Sandbox of string
    | Scope of string
    | Scoped of bool
    | Scrolling of string
    | Seamless of bool
    | Selected of bool
    | Shape of string
    | Size of float
    | Sizes of string
    | Span of float
    | SpellCheck of bool
    | Src of string
    | SrcDoc of string
    | SrcLang of string
    | SrcSet of string
    | Start of float
    | Step of obj
    | Summary of string
    | TabIndex of int
    | Target of string
    | Title of string
    | Type of string
    | UseMap of string
    | Value of obj
    /// Compiles to same prop as `Value`. Intended for `select` elements
    /// with `Multiple` prop set to `true`.
    | [<CompiledName("value")>] ValueMultiple of string[]
    | Width of obj
    | Wmode of string
    | Wrap of string
    | About of string
    | Datatype of string
    | Inlist of obj
    | Prefix of string
    | Property of string
    | Resource of string
    | Typeof of string
    | Vocab of string
    | AutoCapitalize of string
    | AutoCorrect of string
    | AutoSave of string
    // | Color of string // Conflicts with CSSProp, shouldn't be used in HTML5
    | ItemProp of string
    | ItemScope of bool
    | ItemType of string
    | ItemID of string
    | ItemRef of string
    | Results of float
    | Security of string
    | Unselectable of bool
    | [<Erase>] Custom of string * obj

    interface IHTMLProp
type React =
    /// <summary>
    /// Renders a React element into a Lit template
    /// </summary>
    /// <param name="reactComponent">The function that will be called to render the component.</param>
    /// <param name="className">The class name to apply to the rendered element.</param>
    /// <returns>A <see cref="Lit.TemplateResult">TemplateResult</see></returns>
    static member toLit (reactComponent: 'Props -> ReactElement, ?className: string): 'Props -> TemplateResult =
        emitJsExpr (jsConstructor<ReactDirective>, reactComponent, defaultArg className "")
            "class extends $0 { renderFn = $1; className = $2 }"
        |> LitBindings.directive :?> _

    /// <summary>
    /// Renders a Lit template into a React element
    /// </summary>
    /// <param name="template">A Lit template result.</param>
    /// <param name="tag">the name of the tag that will wrap the Lit template result .</param>
    /// <param name="className">a class name for the wrapper element.</param>
    /// <returns>A ReactElement</returns>
    static member inline ofLit (template: TemplateResult, ?tag: string, ?className: string) =
        let inline domEl (tag: string) (props: IHTMLProp seq) (children: ReactElement seq): ReactElement =
                    ReactBindings.React.createElement(tag, keyValueList CaseRules.LowerFirst props, children)
        let tag = defaultArg tag "div"
        let container = Hooks.useRef Unchecked.defaultof<Element option>
        Hooks.useEffect((fun () ->
            match container.current with
            | None -> ()
            | Some el -> template |> Lit.render (el :?> HTMLElement)
        ))
     
        domEl tag [
            Class (defaultArg className "")
            Prop.RefValue container
        ] []

    /// Renders a Lit HTML template as a ReactElement.
    /// Must be used at the root of a React functional component (like a hook).
    static member inline lit_html (s: FormattableString) =
        React.ofLit(Lit.html s)

    /// Renders a Lit SVG template as a ReactElement.
    /// svg is required for nested templates within an svg element.
    /// Must be used at the root of a React functional component (like a hook).
    static member inline lit_svg (s: FormattableString) =
        React.ofLit(Lit.html s)