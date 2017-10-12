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

let stackToString (currentStack:List<Tile>) = 
    currentStack |> List.fold (fun string tile -> string + " " + tile.Id.ToString()) ""
    

let findEdgeOnLeft potentialTile edgeToFind = 
    let potentialTiles = [potentialTile; rotate potentialTile 1; rotate potentialTile 2; rotate potentialTile 3;]
    potentialTiles |> List.filter (fun x -> x.Left = edgeToFind)

let findEdgeOnTop potentialTile edgeToFind = 
    let potentialTiles = [potentialTile; rotate potentialTile 1; rotate potentialTile 2; rotate potentialTile 3;]
    potentialTiles |> List.filter (fun x -> x.Top = edgeToFind)

let findEdgeOnLeftAndTop potentialTile leftEdgeToFind topEdgeToFind = 
    let potentialTiles = [potentialTile; rotate potentialTile 1; rotate potentialTile 2; rotate potentialTile 3;]
    potentialTiles |> List.filter (fun x -> x.Top = topEdgeToFind && x.Left = leftEdgeToFind)

//is there a way to avoid this function "grouping"? can i unwind the recursion?
let rec recurse (input:List<Tile>) =
    match input.Length with
    | 0 -> start input
    | 9 -> stackToString input
    | 3 | 6 -> recurseBottom input
    | 1 | 2 -> recurseRight input
    | _ -> recurseRightAndBottom input
and start (input:List<Tile>) =
    let solutions = bag |> List.map (fun x-> recurse(x::input))
    let allSolutions = if solutions.Length > 0 then solutions |> List.reduce (fun x y -> x + "\r\n" + y) else ""
    allSolutions
and recurseRight (input:List<Tile>) = 
    let nextEdgeValue = input.[0].Right * -1
    let unusedTiles = bag |> List.filter (fun x -> not(stackContainsId input x.Id))
    let unusedTilesWithEdgeMatchOnLeft = unusedTiles |> List.collect (fun x -> findEdgeOnLeft x nextEdgeValue)
    let solutions = unusedTilesWithEdgeMatchOnLeft |> List.map (fun x -> recurse(x::input))
    let allSolutions = if solutions.Length > 0 then solutions |> List.reduce (fun x y -> x + "\r\n" + y) else ""
    allSolutions
and recurseBottom (input:List<Tile>) =
    let nextEdgeValue = input.[2].Bottom * -1
    let unusedTiles = bag |> List.filter (fun x -> not(stackContainsId input x.Id))
    let unusedTilesWithEdgeMatchOnTop = unusedTiles |> List.collect (fun x -> findEdgeOnTop x nextEdgeValue)
    let solutions = unusedTilesWithEdgeMatchOnTop |> List.map (fun x -> recurse(x::input))
    let allSolutions = if solutions.Length > 0 then solutions |> List.reduce (fun x y -> x + "\r\n" + y) else ""
    allSolutions
and recurseRightAndBottom (input:List<Tile>) = 
    let nextLeftEdgeValue = input.[0].Right * -1    
    let nextTopEdgeValue = input.[2].Bottom * -1
    let unusedTiles = bag |> List.filter (fun x -> not(stackContainsId input x.Id))
    let unusedTilesWithEdgeMatchOnTop = unusedTiles |> List.collect (fun x -> findEdgeOnLeftAndTop x nextLeftEdgeValue nextTopEdgeValue)
    let solutions = unusedTilesWithEdgeMatchOnTop |> List.map (fun x -> recurse(x::input))
    let allSolutions = if solutions.Length > 0 then solutions |> List.reduce (fun x y -> x + "\r\n" + y) else ""
    allSolutions


[<EntryPoint>]
let main argv = 
    printfn "%A" argv
    let usedStack = []

    let allSolutions = recurse usedStack
    printfn "%s" allSolutions        

    System.Console.ReadLine() |> ignore

    0 // return an integer exit code

