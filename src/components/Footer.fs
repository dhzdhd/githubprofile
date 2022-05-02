namespace App

open Feliz
open Fss

type Components () =
    [<ReactComponent>]
    static member Footer () =
        let footerStyle =
            fss [
                Width.value (vw 100)
                Height.value (rem 3)
                BackgroundColor.black
                Display.flex
                FlexDirection.row
                AlignItems.center
                JustifyContent.center
                Color.white
            ] 
        
        let linkStyle =
            fss [
                TextDecoration.none
                Color.white
            ]
        
        Html.footer [
            prop.className footerStyle
            prop.children [
                Html.a [
                    prop.className linkStyle
                    prop.href "https://www.google.com"
                    prop.target "_blank"
                    prop.attributeName "noreferrer"
                    prop.attributeName "noopener"
                    prop.text "Github"
                ]
            ]
        ]
