namespace App

open Feliz
open Feliz.Recharts
open Fss
open Fss.Types
open Api

type Point = { name: string; pv: int; }


type Components () =
    [<ReactComponent>]  
    static member Card (repo: List<Repository>) =
        let themeState = React.useContext ThemeStore.themeContext
        
//        repo |> List.map (fun e -> e.)
        
        let data = [
            { name = "Elm"; pv = 2 }
            { name = "F#"; pv = 3 }
            { name = "D"; pv = 1 }
            { name = "Haskell"; pv = 5 }
            { name = "Python"; pv = 6 }
            { name = "Dart"; pv = 3 }
            { name = "Clojure"; pv = 6 }
        ]
        
        let cardStyle = [
            Width.value (rem 30)
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
                    prop.text "Most starred"
                    prop.fss headingStyle
                ]
                Recharts.barChart [
                    barChart.width 320
                    barChart.height 400
                    barChart.data data
                    barChart.children [
                        Recharts.cartesianGrid [ cartesianGrid.strokeDasharray(3, 3) ]
                        Recharts.xAxis [ xAxis.dataKey (fun point -> point.name) ]
                        Recharts.yAxis [ ]
                        Recharts.tooltip [ ]
                        Recharts.legend [ ]
                        Recharts.bar [
                            bar.dataKey (fun point -> point.pv)
                            bar.fill "#8884d8"
                        ]
                    ]
                ]
            ]
        ]
