module Core

open Model
open System

let updateTime (timer:int list) =
    let t3 =  (timer.[3] + 1) |> float
    let t0 =  (t3/100./60.) |> Math.Floor
    let t1 =  (t3/100. - t0 * 60.) |> Math.Floor
    let t2 =  (t3 - t1 * 100.- t0 * 6000.) |> Math.Floor
    failwith "return time as a list"



let update (model : TypingModel) = function
    | Tick -> { model with Time = updateTime model.Time}
    | StartOver ->  
        failwith "Reset the timer and the text area and everything"
    | KeyPress when model.Status = Initial -> { model with Status = JustStarted}
    | TextUpdated text ->
        let model = {model with CurrentText = text}
        let originTextMatch = model.TargetText.Substring(0,model.CurrentText.Length);
        if model.CurrentText = model.TargetText then
            failwith "decide status"
        else if model.CurrentText = originTextMatch then
            failwith "decide status"
        else
            failwith "decide status"
    | _ -> model
