namespace App

open Feliz
open Fss

type Components () =
    [<ReactComponent>]
    static member Header () =
        let headerStyle =
            fss [
                Width.value (vw 100)
                Height.value (rem 5)
                BackgroundColor.blue
                Display.flex
                FlexDirection.row
                JustifyContent.center
            ]
            
        let inputStyle =
            fss [
                
            ]
        
        Html.header [
            prop.className headerStyle
            prop.children [
                Html.input [
                    prop.type'.search
                    prop.className inputStyle
                ]
            ]
        ]
