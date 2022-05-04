namespace App

open Feliz
open Fss
open Fss.Types

type Components () =
    [<ReactComponent>]
    static member Footer () =
        let footerStyle = [
            Height.value (rem 3)
            BackgroundColor.white
            Display.flex
            FlexDirection.row
            AlignItems.center
            JustifyContent.center
            Color.black
            
            Media.query [
            Fss.Types.Media.PrefersColorScheme Fss.Types.Media.ColorScheme.Dark
            ] [
                Color.white
                BackgroundColor.black
            ]
         ] 
        
        let linkStyle = [
            TextDecoration.none
         ]
        
        Html.footer [
            prop.fss footerStyle
            prop.children [
                Html.a [
                    prop.fss linkStyle
                    prop.href "https://github.com/dhzdhd/githubprofile"
                    prop.target "_blank"
                    prop.attributeName "noreferrer"
                    prop.attributeName "noopener"
                    prop.text "Github"
                ]
            ]
        ]
