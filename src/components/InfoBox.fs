namespace App

open Feliz
open Fss
open Fss.Types

type Components () =
    [<ReactComponent>]
    static member InfoBox (title: string) (content: int) =
        let themeState = React.useContext ThemeStore.themeContext
        
        let boxStyle = [
            Height.value (rem 4)
            Width.value (rem 10)
            BorderStyle.solid
            BorderWidth.value (rem 0.1)
            BorderColor.value themeState.Theme.AccentColor
            BorderRadius.value (px 5)
            Display.flex
            FlexDirection.column
            JustifyContent.center
            TextAlign.center
            TransitionDuration.value (ms 300)
            
            Hover [
                BackgroundColor.value themeState.Theme.AccentColor
                Color.value themeState.Theme.PrimaryColor
            ]
        ]
        
        Html.div [
            prop.fss boxStyle
            prop.children [
                Html.span [
                    prop.fss [ FontSize.xLarge ]
                    prop.text title
                ]
                Html.span [
                    prop.fss [ FontSize.large ]
                    prop.text content
                ]
            ]
        ]
