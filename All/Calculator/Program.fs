namespace October3

open System

module Program =

let sub = fun (x:double, y:double) -> x - y
let add = fun (x:double, y:double) -> x + y
let mul = fun (x:double, y:double) -> x * y
let div = fun (x:double, y:double) -> x / y
            
[<EntryPoint>]
let main args =

Console.WriteLine "Введите цифру, оператор, цифру"

let first = Console.ReadLine()
let (res1, r1) = Double.TryParse first
if res1 = false then
    Console.WriteLine("Неверные параметры ввода")
    0
else


let op = Console.ReadLine()
if op <> "+" && op <> "-" && op <> "*" && op <> "/" then
    Console.WriteLine("Неверный оператор")
    0
else
    

let second = Console.ReadLine()
let (res2, r2) = Double.TryParse second // then
if res2 = false then
    Console.WriteLine("Неверные параметры ввода")
    0
else
    
Console.WriteLine("Processing...")
if op = "+" then Console.WriteLine("Result:" + (add(r1, r2)).ToString())
if op = "*" then Console.WriteLine("Result:" + (mul(r1, r2)).ToString())
if op = "-" then Console.WriteLine("Result:" + (sub(r1, r2)).ToString())
if op = "/" then Console.WriteLine("Result:" + (div(r1, r2)).ToString())
0 