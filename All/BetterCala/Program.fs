open System

module Program =

type Operator = | Addition | Subtraction | Multiplication | Division

type MaybeBuilder() =
   member this.Bind(x, f) =
      match x with
      | None -> None
      | Some a -> f a

   member this.Return(x) =
      Some x

let parseOperator = fun op ->
   match op with
   | "+" -> Some(Addition)
   | "-" -> Some(Subtraction)
   | "*" -> Some(Multiplication)
   | "/" -> Some(Division)
   | _ -> None

let tryParseDouble = fun (str: string) ->
   match Double.TryParse(str) with
   | (true, double) -> Some(double)
   | _ -> None

let exitProgram str =
   if (str.Equals "exit") then 
    Environment.Exit(-1)
   0
    

[<EntryPoint>]
let main _ =
   while true do
    let maybe = new MaybeBuilder()
    Console.WriteLine "Введите цифру, оператор, цифру"
    let result = maybe {
     let input1 = Console.ReadLine()
     exitProgram input1 |> ignore
     let! value1 = input1 |> tryParseDouble
     let! op = Console.ReadLine() |> parseOperator
     let! value2 = Console.ReadLine() |> tryParseDouble

     return
        match op with
           | Addition -> value1 + value2
           | Subtraction -> value1 - value2
           | Multiplication -> value1 * value2
           | Division -> value1 / value2
    }

    if result.IsSome then
      let res = String.Join(" ", "Result: " + result.Value.ToString());
      Console.WriteLine res
    else
      Console.WriteLine "Неверный ввод попробуйте снова"
   0
 