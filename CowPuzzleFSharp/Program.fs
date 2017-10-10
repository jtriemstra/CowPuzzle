// Learn more about F# at http://fsharp.net
// See the 'F# Tutorial' project for more help.
let NOSE_LEFT = 1
let SMALL_HEAD = 2
let BIG_HEAD = 3
let LAY_TOP = 4
let LAY_BOTTOM = LAY_TOP * -1
let BIG_TAIL = BIG_HEAD * -1
let SMALL_TAIL = SMALL_HEAD * -1
let NOSE_RIGHT = NOSE_LEFT * -1

type Tile = 
  { Top: int;
    Right: int;
    Bottom: int;
    Left: int }
    
let bag = [|
        {Top=NOSE_LEFT; Right=SMALL_TAIL; Bottom=LAY_BOTTOM; Left=BIG_TAIL},
        {Top=LAY_BOTTOM; Right=LAY_BOTTOM; Bottom=BIG_HEAD; Left=SMALL_HEAD},
        {Top=SMALL_HEAD; Right=BIG_HEAD; Bottom=LAY_BOTTOM; Left=SMALL_HEAD},
        {Top=BIG_TAIL; Right=NOSE_LEFT; Bottom=LAY_BOTTOM; Left=NOSE_RIGHT},
        {Top=LAY_TOP; Right=BIG_TAIL; Bottom=NOSE_RIGHT; Left=BIG_TAIL},
        {Top=LAY_BOTTOM; Right=SMALL_TAIL; Bottom=NOSE_LEFT; Left=BIG_TAIL},
        {Top=NOSE_RIGHT; Right=BIG_HEAD; Bottom=LAY_TOP; Left=SMALL_HEAD},
        {Top=BIG_HEAD; Right=SMALL_TAIL; Bottom=NOSE_LEFT; Left=NOSE_RIGHT},
        {Top=LAY_TOP; Right=BIG_TAIL; Bottom=NOSE_RIGHT; Left=SMALL_HEAD}
    |]

let rotate input num = 
    match num with
    | 0 -> input
    | 1 -> {Top=input.Left; Right=input.Top; Bottom=input.Right; Left=input.Bottom}
    | 2 -> {Top=input.Bottom; Right=input.Left; Bottom=input.Top; Left=input.Right}
    | 3 -> {Top=input.Right; Right=input.Bottom; Bottom=input.Left; Left=input.Top}
    | _ -> failwith "Invalid rotation"


let push currentStack input = 
    input::currentStack

let pop currentStack = 
    match currentStack with
    | top::remainder ->
        (top,remainder)
    | [] ->
        failwith "Stack is empty, can't pop"

let printStack currentStack =
    currentStack
    |> List.map (fun x -> printf x + " ")

//is there a way to avoid this function "grouping"? can i unwind the recursion?
let rec recurse (input:List<Tile>) =
    match input.Length with
    | 9 -> ""
    | 3 | 6 -> ""
    | 0 | 1 | 2 -> recurseRight input
    | _ -> ""
and recurseRight (input:List<Tile>) = 
    let nextEdgeValue = input.[0].Right * -1
    
    //temp return string
    ""
and recurseBottom (input:List<Tile>) =
    //temp return string
    ""

let solveInnerLoop currentStack tile =
//should i be returning a stack or a string? neither?
    let newStack = push currentStack tile
    recurse newStack
    //don't think i need to pop b/c the stack is immutable



let rotationLoop tile =
    [0..3]
    |> List.map innerLoop tile

[<EntryPoint>]
let main argv = 
    printfn "%A" argv
    let usedStack = []

    for tile in bag do
        for rotation in [0..3] do
            tile
            |> rotate rotation
            //let newStack = push tile usedStack
            //let x = pop usedStack
            //()    
            

    0 // return an integer exit code

