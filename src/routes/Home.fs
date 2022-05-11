namespace App

open Feliz
open Fss
open Fss.Types

type Routes () =
    [<ReactComponent>]
    static member Home () =
        let themeState = React.useContext ThemeStore.themeContext
        
        let containerStyle = [
            MinHeight.value (vh 100)
            BackgroundColor.value themeState.Theme.SecondaryColor
            Display.flex
            FlexDirection.column
            AlignItems.center
            JustifyContent.center
        ]
       
        Html.div [
            prop.fss containerStyle
            prop.children [
                Html.h1 [
                    prop.fss [ FontSize.value (rem 4); Color.value themeState.Theme.TextColor ]
                    prop.text "Welcome to Github Profile"
                ]
//                Html.h2 [
//                    prop.text "e"
//                ]
            ]
        ]
