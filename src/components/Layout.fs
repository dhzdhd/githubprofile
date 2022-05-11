namespace App

open Feliz
open Fss
open Fss.Types

type Components () =
    [<ReactComponent>]
    static member Layout (child: Fable.React.ReactElement) =
        let mainStyle =
            fss [
                MinHeight.value (vh 100)
                MaxWidth.value (vw 100)
            ]
        
        React.fragment [
            App.Components.Header ()
            Html.main [
                prop.className mainStyle
                prop.children [ child ]
            ]
            App.Components.Footer ()
        ]

