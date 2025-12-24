namespace MVU

module Model=

    type Time = int list

    type Status =
        | Initial
        | JustStarted
        | Complete
        | Wrong
        | Correct

    type Message = 
        | Tick
        | StartOver
        | KeyPress
        | TextUpdated of string

    type TypingModel = { Time : Time; Status : Status; CurrentText : string; TargetText : string }

    type SideEffect = 
        | NoEffect
        | StartTimer
        | StopTimer

module Core=

    open Model
    open System
    let init () = 
            {   Status = Initial; 
                CurrentText = ""; 
                TargetText = "This is a typing test.";
                Time = [0;0;0;0] } ,NoEffect
        
    let updateTime (timer:int list) =
        let t3 =  (timer.[3] + 1) |> float
        let t0 =  (t3/100./60.) |> Math.Floor
        let t1 =  (t3/100. - t0 * 60.) |> Math.Floor
        let t2 =  (t3 - t1 * 100.- t0 * 6000.) |> Math.Floor
        [int t0;int t1;int t2; int t3]



    let update (msg:Message) (model : TypingModel) =
        match msg with
        | Tick -> { model with Time = updateTime model.Time}, NoEffect
        | StartOver ->  {model with Status = Initial; Time = [0;0;0;0]; CurrentText = ""},StopTimer
        | KeyPress when model.Status = Initial -> { model with Status = JustStarted},StartTimer
        | TextUpdated text when model.Status <> Complete ->
            let model = {model with CurrentText = text}
            if model.CurrentText = model.TargetText then
                {model with Status = Complete},StopTimer
            else if model.CurrentText.Length < model.TargetText.Length
                 && model.TargetText.StartsWith model.CurrentText then
                { model with Status = Correct},NoEffect
            else
                {model with Status = Wrong},NoEffect
        | _ -> model,NoEffect
