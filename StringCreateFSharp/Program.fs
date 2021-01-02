// Learn more about F# at http://fsharp.org

open System
open BenchmarkDotNet.Running
open StringCreate

[<EntryPoint>]
let main argv =
    BenchmarkRunner.Run(typeof<Replace>.Assembly) |> ignore
    //printfn "%s" (Replace().StringCreateFor())
    0 // return an integer exit code
