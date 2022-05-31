namespace App

open Feliz
open Fss
open Fss.Types

type Components () =
    [<ReactComponent>]
    static member Footer () =
        let themeState = React.useContext ThemeStore.themeContext
        
        let footerStyle = [
            Height.value (rem 3)
            BackgroundColor.value themeState.Theme.PrimaryColor
            Color.value themeState.Theme.TextColor
            Display.flex
            GridGap.value (rem 2)
            FlexDirection.row
            AlignItems.center
            JustifyContent.center
         ] 
        
        let linkStyle = [
            TextDecoration.none
            Color.value themeState.Theme.TextColor
         ]
        
        Html.footer [
            prop.fss footerStyle
            prop.children [
                Html.span [
                    prop.text "Built with Feliz + F#"
                ]
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
