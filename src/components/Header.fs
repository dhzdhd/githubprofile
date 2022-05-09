namespace App

open Feliz
open Fss
open Fss.Types

type Components () =
    [<ReactComponent>]
    static member Header () =
        let themeState = React.useContext ThemeStore.themeContext
        let themeDispatch = React.useContext ThemeStore.themeDispatchContext
        
        let headerStyle = [
            Height.value (rem 5)
            Display.flex
            FlexDirection.row
            JustifyContent.spaceBetween
            AlignItems.center
            BackgroundColor.value themeState.Theme.PrimaryColor
            Padding.value (rem 0, rem 2)
        ]
        
        let headingStyle = [
            FontSize.xxLarge
            Color.value themeState.Theme.SecondaryColor
        ]
            
        let containerStyle = [
            Display.flex
            FlexDirection.row
            AlignItems.center
            JustifyContent.center
        ]
            
        let inputStyle =  [
            Height.value (rem 3)
            BorderWidth.value (rem 0.1)
            BorderStyle.solid
            BorderColor.black
            BorderRadius.value (rem 0.5, rem 0, rem 0, rem 0.5)
            
            Focus [
                BorderWidth.value (rem 0.2)
            ]
        ]
        
        let searchButtonStyle = [
            Height.value (rem 3)
            Width.value (rem 3)
            Border.none
            BorderRadius.value (rem 0, rem 0.5, rem 0.5, rem 0)
        ]
        
        let themeButtonStyle = [
            Height.value (rem 3)
            Width.value (rem 3)
            Border.none
            BorderRadius.value (rem 0.2, rem 0.2, rem 0.2, rem 0.2)
            MarginLeft.value (rem 2)
        ]
        
        Html.header [
            prop.fss headerStyle
            prop.children [
                Html.span [
                    prop.text "Github Profile"
                    prop.fss headingStyle
                ]
                Html.div [
                    prop.fss containerStyle
                    prop.children [
                        Html.input [
                            prop.type'.search
                            prop.fss inputStyle
                        ]
                        Html.button [
                            prop.fss searchButtonStyle
                            prop.children [
                                Html.span [
                                    prop.className "fas fa-search"
                                ]
                            ]
                        ]
                        Html.button [
                            prop.fss themeButtonStyle
                            prop.onClick (fun _ ->
                                themeDispatch (ThemeStore.SetTheme ThemeStore.ThemeType.Light))
                            prop.children [
                                Html.span [
                                    prop.className "fas fa-palette"
                                ]
                            ]
                        ]
                    ]
                ]
            ]
        ]
