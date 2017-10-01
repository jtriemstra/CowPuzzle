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

let pointless x =
    printfn "this is pointless"

let add x y =
    x + y

[<EntryPoint>]
let main argv = 
    printfn "%A" argv
    0 // return an integer exit code

