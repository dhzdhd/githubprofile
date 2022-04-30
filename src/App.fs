namespace App

open Feliz
open Fss
open Feliz.Recoil

type Home () =
    [<ReactComponent>] 
    static member Index () =
        let mainStyle =
            fss [
                Height.value (vh 100)
                Width.value (vw 100)
                BackgroundColor.black
            ]
        
        Recoil.root [
            Html.main [
                prop.className mainStyle
                prop.children [
                    Html.div []
                ]
            ]
        ]
