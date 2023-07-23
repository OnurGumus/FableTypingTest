module App

open Fable.Core.JsInterop
open Fable.Import
open Browser.Types
open Browser.Dom
open Model
open Core

let testWrapper = document.querySelector(".test-wrapper")  :?> HTMLElement
let testArea = document.querySelector("#test-area") :?> HTMLTextAreaElement
let originText = document.querySelector("#origin-text p").innerHTML
let resetButton = document.querySelector("#reset") :?> HTMLButtonElement
let theTimer : HTMLElement = failwith "get timer element"

let model = 
    { Status = Initial; 
        CurrentText = ""; 
        TargetText = originText; 
        Time = failwith "set initial time"
    } : TypingModel
        
let viewTime (timer : Time) =
    let leadingZero section =
        if (section <= 9) then
            failwith "add leading zero"
        else
            section.ToString()
    let currentTime = leadingZero(timer.[0]) + ":" + leadingZero(timer.[1]) + ":" + leadingZero(timer.[2]);
    theTimer.innerHTML <- currentTime;

let stopTimer () =
    window.clearInterval !!(window?myInterval)
    window?myInterval <- null

let view {Status = status ; Time = time} (dispatcher: MailboxProcessor<Message>) =
    match status with
    | Initial ->
        testArea.value <- ""
        theTimer.innerHTML <- "00:00:00"
        stopTimer()
    | JustStarted ->
        failwith "clear myInterval"
    | Correct ->
        testWrapper?style?borderColor <- "#65CCf3"

    | Wrong ->
        // change border color
        failwith "Not implemented"
    | Complete ->
        testWrapper?style?borderColor <- "#429890"
        // stop timer
        failwith "Change the border color"

    viewTime time



#nowarn "40"
let rec dispatcher = MailboxProcessor<Message>.Start(fun inbox->

    // the message processing function
    let rec messageLoop (model : TypingModel) = async{
        // read a message
       
        let! msg = failwith "Read Message"
        // process a message
        let newModel = update model msg
        view newModel dispatcher
        // loop to top
        return! messageLoop newModel}

    // start the loop
    messageLoop model)

testArea.addEventListener("keyup", fun e -> dispatcher.Post (TextUpdated !!(e.target?value)) |> ignore)
testArea.addEventListener("keypress", fun _ -> dispatcher.Post (KeyPress) |> ignore)

failwith "Add event listener for reset button"
