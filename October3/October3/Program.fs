namespace October3

open System

module Program =

let tryParseDouble (arg : string) =
    match Double.TryParse arg with
    | (true,d) -> d
    | _ -> Double.NaN
   
//let add x y = x+y
let sub = fun (x:double, y:double) -> x - y
let add = fun (x:double, y:double) -> x + y
let mul = fun (x:double, y:double) -> x * y
let div = fun (x:double, y:double) -> x / y
//let mul x y = x*y
//let div x y = x/y
        
//thisype MaybeBuilder() =
//    member this.Bind(x, f) =
//        match x with
//        | None -> None
//        | Some a -> f a

//let add =
//    fun (op : string) (v1: double option) (v2: double option) ->
//        if (v1.IsSome && v2.IsSome)
//        then 
//            if op = "+" then v1.Value + v2.Value
//            if op = "-" then v1.Value - v2.Value
//            if op = "*" then v1.Value * v2.Value
//            if op = "/" then v1.Value / v2.Value
//        else
//            0.0;
            
[<EntryPoint>]
let main args =

Console.WriteLine "Retard Alert"
let first = Console.ReadLine() |> tryParseDouble
if first = Double.NaN then
    Console.WriteLine("Некорректный ввод, нужно вводить цифру")
    0
else
    
let op = Console.ReadLine()
let second = Console.ReadLine() |> tryParseDouble

if second = Double.NaN then
    Console.WriteLine("Некорректный ввод, нужно вводить цифру")
    0
else    
    
//let maybe = MaybeBuilder()
//maybe.Bind
let result = add(first, second)
if op = "+" then Console.WriteLine("Result:" + (add(first, second)).ToString())
if op = "*" then Console.WriteLine("Result:" + (mul(first, second)).ToString())
if op = "-" then Console.WriteLine("Result:" + (sub(first, second)).ToString())
if op = "/" then Console.WriteLine("Result:" + (div(first, second)).ToString())
0 