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
    Left: int;
    Id: int }
    
let bag = [
        {Top=NOSE_LEFT; Right=SMALL_TAIL; Bottom=LAY_BOTTOM; Left=BIG_TAIL; Id=0};
        {Top=LAY_BOTTOM; Right=LAY_BOTTOM; Bottom=BIG_HEAD; Left=SMALL_HEAD; Id=1};
        {Top=SMALL_HEAD; Right=BIG_HEAD; Bottom=LAY_BOTTOM; Left=SMALL_HEAD; Id=2};
        {Top=BIG_TAIL; Right=NOSE_LEFT; Bottom=LAY_BOTTOM; Left=NOSE_RIGHT; Id=3};
        {Top=LAY_TOP; Right=BIG_TAIL; Bottom=NOSE_RIGHT; Left=BIG_TAIL; Id=4};
        {Top=LAY_BOTTOM; Right=SMALL_TAIL; Bottom=NOSE_LEFT; Left=BIG_TAIL; Id=5};
        {Top=NOSE_RIGHT; Right=BIG_HEAD; Bottom=LAY_TOP; Left=SMALL_HEAD; Id=6};
        {Top=BIG_HEAD; Right=SMALL_TAIL; Bottom=NOSE_LEFT; Left=NOSE_RIGHT; Id=7};
        {Top=LAY_TOP; Right=BIG_TAIL; Bottom=NOSE_RIGHT; Left=SMALL_HEAD; Id=8};
    ]

let rotate input num = 
    match num with
    | 0 -> input
    | 1 -> {Top=input.Left; Right=input.Top; Bottom=input.Right; Left=input.Bottom; Id=input.Id}
    | 2 -> {Top=input.Bottom; Right=input.Left; Bottom=input.Top; Left=input.Right; Id=input.Id}
    | 3 -> {Top=input.Right; Right=input.Bottom; Bottom=input.Left; Left=input.Top; Id=input.Id}
    | _ -> failwith "Invalid rotation"


let push currentStack input = 
    input::currentStack

let pop currentStack = 
    match currentStack with
    | top::remainder ->
        (top,remainder)
    | [] ->
        failwith "Stack is empty, can't pop"

let stackContainsId stack id =
    stack
    |> List.exists (fun x -> x.Id = id)

let printStack currentStack =
    currentStack
    |> List.map (fun x -> printf x + " ")

let findEdgeOnLeft potentialTile edgeToFind = 
    let potentialTiles = [potentialTile; rotate potentialTile 1; rotate potentialTile 2; rotate potentialTile 3;]
    potentialTiles |> List.filter (fun x -> x.Left = edgeToFind)

//is there a way to avoid this function "grouping"? can i unwind the recursion?
let rec recurse (input:List<Tile>) =
    match input.Length with
    | 9 -> ""
    | 3 | 6 -> ""
    | 0 | 1 | 2 -> recurseRight input
    | _ -> ""
and recurseRight (input:List<Tile>) = 
    let nextEdgeValue = input.[0].Right * -1
    let unusedTiles = bag |> List.filter (fun x -> not(stackContainsId input x.Id))
    let unusedTilesWithEdgeMatchOnLeft = unusedTiles |> List.collect (fun x -> findEdgeOnLeft x nextEdgeValue)
    for newTile in unusedTilesWithEdgeMatchOnLeft do
        recurse (newTile::input) |> ignore
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

