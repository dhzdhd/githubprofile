namespace App

open Feliz
open Feliz.Recharts
open Feliz.Recharts.bar
open Fss
open Fss.Types
open Api

type Point = { name: string; pv: int; }


type Components () =
    [<ReactComponent>]  
    static member Card (repo: List<Repository>) =
        let themeState = React.useContext ThemeStore.themeContext
        
        let data =
            repo
            |> List.map ( fun e ->
                match e.Language with
                | Some lang -> lang
                | None -> "" )
            |> List.filter ( fun e -> e <> "" )
            |> List.countBy id
            |> List.map (fun (lang, num) -> { name = lang; pv = num })
            |> List.sortBy ( fun data -> data.pv )         
        
        let cardStyle = [
            MaxWidth.value (rem 25)
            MinHeight.value (rem 30)
            Padding.value (rem 2, rem 2, rem 2, rem 2)
            BackgroundColor.value themeState.Theme.PrimaryColor
            Display.flex
            FlexDirection.column
            GridGap.value (rem 1)
            BorderRadius.value (rem 1)
        ]
        
        let headingStyle = [
            FontSize.xxLarge
        ]
        
        Html.div [
            prop.fss cardStyle
            prop.children [
                Html.span [
                    prop.text "Most used"
                    prop.fss headingStyle
                ]
                Recharts.responsiveContainer [
                    responsiveContainer.height 400
                    responsiveContainer.chart (Recharts.barChart [
                        barChart.data data
                        barChart.children [
                            Recharts.cartesianGrid [ cartesianGrid.strokeDasharray(3, 3) ]
                            Recharts.xAxis [ xAxis.dataKey (fun point -> point.name) ]
                            Recharts.tooltip [ ]
                            Recharts.bar [
                                bar.dataKey (fun point -> point.pv)
                                bar.fill "#8884d8"
                            ]
                        ]
                    ])
                ]
            ]
        ]
