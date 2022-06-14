namespace App

open System
open Fable.Core.JS
open Feliz
open Feliz.Recharts
open Feliz.Recharts.bar
open Fss
open Fss.Types
open Api

type PieUnit = { name: string; value: int }
type BarUnit = { name: string; pv: int }

type Components () =
    [<ReactComponent>]  
    static member Charts (repo: List<Repository>) =
        let themeState = React.useContext ThemeStore.themeContext
        
        let langData =
            repo
            |> List.map ( fun e ->
                match e.Language with
                | Some lang -> lang
                | None -> "" )
            |> List.filter ( fun e -> e <> "" )
            |> List.countBy id
            |> List.map (fun (lang, num) -> { name = lang; value = num })
            |> List.sortBy ( fun data -> data.value )
        
        let starredData =
            repo
            |> List.filter ( fun e -> e.StargazersCount <> 0 )
            |> List.sortByDescending (fun e -> e.StargazersCount ) 
            |> List.map ( fun e -> { name = e.Name; pv = e.StargazersCount } )
            
        let chartDivStyle = [
            Display.flex
            FlexDirection.column
            JustifyContent.center
            AlignItems.center
            GridGap.value (rem 1)
            
            Media.query [Types.Media.MinWidth Utils.md] [
                FlexDirection.row
            ]
        ]
        
        let cardStyle = [
            MaxWidth.value (rem 20)
            MinWidth.value (rem 15)
            MinHeight.value (rem 25)
            Padding.value (rem 2, rem 2, rem 2, rem 2)
            BackgroundColor.value themeState.Theme.PrimaryColor
            Display.flex
            FlexDirection.column
            GridGap.value (rem 1)
            BorderRadius.value (rem 1)
            
            Media.query [Types.Media.MinWidth Utils.md] [
                MinWidth.value (rem 30)
            ]
        ]
        
        let headingStyle = [
            FontSize.xxLarge
        ]
        
        let renderCustomLabel (input: IPieLabelProperties) =
            let radius = input.innerRadius + (input.outerRadius - input.innerRadius) * 0.5;
            let radian = Math.PI / 180.
            let x = (input.cx + radius * cos (-input.midAngle * radian))
            let y = (input.cy + radius * sin (-input.midAngle * radian))
            
            Svg.text [
                svg.fill color.white
                svg.x x
                svg.y y
                svg.dominantBaseline.central
                if x > input.cx then svg.textAnchor.startOfText else svg.textAnchor.endOfText
                svg.text (sprintf "%.0f%%" (100. * input.percent))
            ]
        
        let cells =
            langData
            |> List.map (fun e ->
                let color =
                    let colorPair = Utils.langColorMap.TryGetValue e.name
                    match colorPair with
                    | true , color -> color
                    | false, _ -> "#FFFFFF"
                
                Recharts.cell [
                    cell.fill color
                ])
        
        Html.div [
            prop.fss chartDivStyle
            prop.children [
                Html.div [
                    prop.fss cardStyle
                    prop.children [
                        Html.span [
                            prop.text "Most used"
                            prop.fss headingStyle
                        ]
                        Recharts.responsiveContainer [
                            responsiveContainer.chart (Recharts.pieChart [
                                pieChart.children [
                                    Recharts.tooltip [  ]
                                    Recharts.pie [
                                        pie.data langData
                                        pie.labelLine false
//                                        pie.outerRadius 110
//                                        pie.label renderCustomLabel
                                        pie.children cells
                                    ]
                                ]
                            ])
                        ]
                    ]
                ]
                Html.div [
                    prop.fss cardStyle
                    prop.children [
                        Html.span [
                            prop.text "Most starred"
                            prop.fss headingStyle
                        ]
                        Recharts.responsiveContainer [
                            responsiveContainer.chart (Recharts.barChart [
                                barChart.data starredData
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
            ]
        ]
