module App

open Fable.Core.JsInterop
open Fable.Import
open Browser.Types
open Browser.Dom

open MVU.Model
open MVU.Core

open Elmish
open Lit.Elmish

let token = Lit.HMR.createToken()
module Program =
    /// <summary>
    /// Program with user-defined orders instead of usual command.
    /// Orders are processed by <code>execute</code> which can dispatch messages,
    /// called in place of usual command processing.
    /// </summary>
    let mkHiddenProgramWithOrderExecute
            (init: 'arg' -> 'model * 'order)
            (update: 'msg -> 'model -> 'model * 'order)
            (execute: 'order -> Dispatch<'msg> -> unit) =
        let convert (model, order) = 
            model, order |> execute |> Cmd.ofEffect
        Program.mkHidden
            (init >> convert)
            (fun msg model -> update msg model |> convert)
            
    let mkProgramWithOrderExecute
            (init: 'arg' -> 'model * 'order)
            (update: 'msg -> 'model -> 'model * 'order)
            (view: 'model -> Dispatch<'msg> ->'view)
            (execute: 'order -> Dispatch<'msg> -> unit) =
        let convert (model, order) = 
            model, order |> execute |> Cmd.ofEffect

        Program.mkProgram
            (init >> convert)
            (fun msg model -> update msg model |> convert)
            view
// let testWrapper = document.querySelector(".test-wrapper")  :?> HTMLElement
// let testArea = document.querySelector("#test-area") :?> HTMLTextAreaElement
// let originText = document.querySelector("#origin-text p").innerHTML
// let resetButton = document.querySelector("#reset") :?> HTMLButtonElement
// let theTimer : HTMLElement = failwith "get timer element"

// let model = 
//     { Status = Initial; 
//         CurrentText = ""; 
//         TargetText = originText; 
//         Time = failwith "set initial time"
//     } : TypingModel
        
// let viewTime (timer : Time) =
//     let leadingZero section =
//         if (section <= 9) then
//             failwith "add leading zero"
//         else
//             section.ToString()
//     let currentTime = leadingZero(timer.[0]) + ":" + leadingZero(timer.[1]) + ":" + leadingZero(timer.[2]);
//     theTimer.innerHTML <- currentTime;

// let stopTimer () =
//     window.clearInterval !!(window?myInterval)
//     window?myInterval <- null

// let view {Status = status ; Time = time} (dispatcher: MailboxProcessor<Message>) =
//     match status with
//     | Initial ->
//         testArea.value <- ""
//         theTimer.innerHTML <- "00:00:00"
//         stopTimer()
//     | JustStarted ->
//         failwith "clear myInterval"
//     | Correct ->
//         testWrapper?style?borderColor <- "#65CCf3"

//     | Wrong ->
//         // change border color
//         failwith "Not implemented"
//     | Complete ->
//         testWrapper?style?borderColor <- "#429890"
//         // stop timer
//         failwith "Change the border color"

//     viewTime time



// #nowarn "40"
// let rec dispatcher = MailboxProcessor<Message>.Start(fun inbox->

//     // the message processing function
//     let rec messageLoop (model : TypingModel) = async{
//         // read a message
       
//         let! msg = failwith "Read Message"
//         // process a message
//         let newModel = update model msg
//         view newModel dispatcher
//         // loop to top
//         return! messageLoop newModel}

//     // start the loop
//     messageLoop model)

// testArea.addEventListener("keyup", fun e -> dispatcher.Post (TextUpdated !!(e.target?value)) |> ignore)
// testArea.addEventListener("keypress", fun _ -> dispatcher.Post (KeyPress) |> ignore)

// failwith "Add event listener for reset button"

open Elmish
open Lit
open Lit.Elmish

let stopTimer () =
    window.clearInterval !!(window?myInterval)
    window?myInterval <- null

let startTimer dispatch =
    if !!(window?myInterval) |> isNull then
        let interval = window.setInterval  ((fun () -> dispatch Tick), 10, [])
        window?myInterval <- interval

let rec execute  order dispatch =
    match order with
    | NoEffect -> ()
    | StartTimer -> startTimer dispatch
    | StopTimer -> stopTimer ()
let viewTime (timer : Time) =
    let leadingZero section =
        if (section <= 9) then
            "0" + section.ToString()
        else
            section.ToString()
    leadingZero(timer.[0]) + ":" + leadingZero(timer.[1]) + ":" + leadingZero(timer.[2]);
 
[<HookComponent()>]
let view (model: TypingModel) dispatch =
    Hook.useHmr token
    let classes = Lit.classes [
        match model.Status with
        | Correct ->  "correct", true
        | Wrong ->  "wrong", true
        | Complete ->  "complete", true
        | _ -> "", false
    ]

    html $"""
        <article class="intro">
            <p>Thiss is a typing test. Your goal is to duplicate the provided text, EXACTLY, in the field below. The timer starts when you start typing, and only stops when you match this text exactly. Good Luck!</p>
        </article>
        <section class="test-area">
            <div id="origin-text">
                <p>{model.TargetText}</p>
            </div>

            <div class="test-wrapper {classes}">
                <textarea id="test-area" @keyup={Ev( fun e -> dispatch (TextUpdated !!(e.target?value)))} 
                    @keypress={Ev( fun _ -> dispatch (KeyPress))}
                    name="textarea" rows="6" .value={model.CurrentText} placeholder="The clock starts when you start typing."></textarea>
            </div>

            <div class="meta">
                <section id="clock">
                    <div class="timer">{viewTime(model.Time)}</div>
                </section>

                <button id="reset" @click={Ev( fun e -> dispatch (StartOver))} >Start over</button>
            </div>
        </section> 
            """

    
[<LitElement("fable-typing")>]
let LitElement () =
    Hook.useHmr token
 
    

    let host, prop = LitElement.init (fun config -> 
   
        config.useShadowDom <- false
    )
    
    let program =
        Program.mkHiddenProgramWithOrderExecute 
            (init) (update) (execute)
            |> Program.withConsoleTrace

    let model, dispatch = Hook.useElmish program
    view model dispatch

// Program.mkProgramWithOrderExecute MVU.Core.init (MVU.Core.update) view execute
// |> Program.withLitOnElement (document.querySelector "main")
// |> Program.withConsoleTrace
// |> Program.run