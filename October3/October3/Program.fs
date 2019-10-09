namespace October3

open System
open Microsoft.Extensions.Logging

module Program =

let tryParseDouble (arg : string) =
    match Double.TryParse arg with
    | (true,d) -> Some(d)
    | _ -> None
   
let add x y = x+y
let sub x y = x-y
let mul x y = x*y
let div x y = x/y
        
type MaybeBuilder() =
    member this.Bind(x, f) =
        match x with
        | None -> None
        | Some a -> f a

let add =
    fun (op : string) (v1: double option) (v2: double option) ->
        if (v1.IsSome && v2.IsSome)
        then 
            if op = "+" then v1.Value + v2.Value
            if op = "-" then v1.Value - v2.Value
            if op = "*" then v1.Value * v2.Value
            if op = "/" then v1.Value / v2.Value
        else (double)0;
            
[<EntryPoint>]
let main args =

Console.WriteLine "Retard Alert"
let first = Console.ReadLine() |> tryParseDouble
let op = Console.ReadLine()
let second = Console.ReadLine() |> tryParseDouble

let maybe = MaybeBuilder()

if op = "+" then Console.WriteLine("Result:" + (add first second).ToString())
//if op = "*" then Console.WriteLine("Result:" + (add first second))
//if op = "-" then Console.WriteLine("Result:" + (sub first second))
//if op = "/" then Console.WriteLine("Result:" + (div first second))  