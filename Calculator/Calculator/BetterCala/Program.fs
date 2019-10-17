open System

module Program =

type MaybeBuilder() =
   member this.Bind(x, f) =
      match x with
      | None -> None
      | Some a -> f a

   member this.Return(x) =
      Some x

type Operator = Addition | Subtraction | Multiplication | Division

let tryParseDouble = fun (str : string) ->
   match Double.TryParse(str) with
   | (true, double) -> Some(double)
   | _ -> None

let parseOperator = fun (op) ->
   match op with
   | "+" -> Some(Addition)
   | "-" -> Some(Subtraction)
   | "*" -> Some(Multiplication)
   | "/" -> Some(Division)
   | _ -> None

[<EntryPoint>]
let main _ =
   let maybe = new MaybeBuilder()
   Console.WriteLine "Введите цифру, оператор, цифру"
   
   let result = maybe {
      let! value1 = Console.ReadLine() |> tryParseDouble
      let! op = Console.ReadLine() |> parseOperator
      let! value2 = Console.ReadLine() |> tryParseDouble

      return
         match op with
         | Addition -> value1 + value2
         | Subtraction -> value1 - value2
         | Multiplication -> value1 * value2
         | Division -> value1 / value2
   }

   Console.WriteLine(if result.IsSome then "Result: " + result.Value.ToString() else "Неверный ввод попробуйте снова")
   0