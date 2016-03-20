#I "./Modules/FSharp.Data/lib/net40"

#r "FSharp.Data.dll"

open FSharp.Data

//_id,parent_id,weapon_name,wtype,creation_cost,upgrade_cost,attack,max_attack,element,element_attack,element_2,element_2_attack,awaken_element,awaken_element_attack,defense,red,orange,yellow,green,blue,white,purple,red_plus,orange_plus,yellow_plus,green_plus,blue_plus,white_plus,purple_plus,sharpness,affinity,horn_notes,shelling_type,phial,charges,coatings,recoil,reload_speed,rapid_fire,deviation,ammo,special_ammo,sharpness_file,num_slots,tree_depth,final,create

type WeaponCSVData = CsvProvider<"./mh4u.csv">

type KinsectType =
  | Balance
  | Power
  | Stamina
  | Speed

let convertStringToKinsectType (stringKinsectType : string) =
  match (stringKinsectType.ToLower()) with
    | "balance" -> Balance
    | "power" -> Power
    | "stamina" -> Stamina
    | "speed" -> Speed
    | _ -> raise <| new System.Exception(sprintf "Missing Kinsect Type %s" stringKinsectType)

type RequiredStats =
  {
    Power : int
    Stamina : int
    Speed : int
  }

let convertIntsToRequiredStats power stamina speed =
  {
    Power = power
    Stamina = stamina
    Speed = speed
  }

type KinsectData =
  {
    Name : string
    Type : KinsectType
    Skill : string
    RequiredStats : RequiredStats
  }

//The kinsectTable data is derived from http://monsterhunter.wikia.com/wiki/MH4U:_Kinsect_Tree
//I had to remove the cascading table headers and seperate them into
//multiple tables so the type provider can work properly
//Table1 : Blunt Path
//Table2 : Blunt Path Event Quest Only
//Table3 : Blunt Path Japanese Event Quest Only
//Table4 : Cutting Path
//Table5 : Cutting Path Japanese Event Quest Only
//I also removed the required stats column and replaced it with headers power, stamina, speed
//removed rowspan from headers because it messed up the type provider


type KinsectHtmlData = HtmlProvider<"./kinsectTable.html">

let kinsectHtmlData = KinsectHtmlData.Load("./kinsectTable.html")
kinsectHtmlData.Tables.Table1.Rows.[0]

let bluntPathData =
  kinsectHtmlData.Tables.Table1.Rows
  |> Array.map(fun row ->
     {
       Name = row.Name
       Type = convertStringToKinsectType row.Type
       Skill = row.Skill
       RequiredStats = convertIntsToRequiredStats row.Power row.Stamina row.Speed
     })

let bluntPathEventQuestData =
  kinsectHtmlData.Tables.Table2.Rows
  |> Array.map(fun row ->
     {
       Name = row.Name
       Type = convertStringToKinsectType row.Type
       Skill = row.Skill
       RequiredStats = convertIntsToRequiredStats row.Power row.Stamina row.Speed
     })

let bluntPathJapaneseEventData =
  kinsectHtmlData.Tables.Table3.Rows
  |> Array.map(fun row ->
     {
       Name = row.Name
       Type = convertStringToKinsectType row.Type
       Skill = row.Skill
       RequiredStats = convertIntsToRequiredStats row.Power row.Stamina row.Speed
     })

let cuttingPathData =
  kinsectHtmlData.Tables.Table4.Rows
  |> Array.map(fun row ->
     {
       Name = row.Name
       Type = convertStringToKinsectType row.Type
       Skill = row.Skill
       RequiredStats = convertIntsToRequiredStats row.Power row.Stamina row.Speed
     })

let cuttingPathJapaneseEventData =
  kinsectHtmlData.Tables.Table5.Rows
  |> Array.map(fun row ->
     {
       Name = row.Name
       Type = convertStringToKinsectType row.Type
       Skill = row.Skill
       RequiredStats = convertIntsToRequiredStats row.Power row.Stamina row.Speed
     })
