﻿namespace App

open Browser
open Feliz
open Feliz.Router
open Fss
open Fss.Types

type Components () =
    [<ReactComponent>]
    static member Header () =
        let themeState = React.useContext ThemeStore.themeContext
        let themeDispatch = React.useContext ThemeStore.themeDispatchContext
        let input, setInput = React.useState ""
        
        let headerStyle = [
            Height.value (rem 5)
            MaxWidth.value (vw 100)
            MinWidth.value (vw 100)
            Position.fixed'
            Display.flex
            FlexDirection.row
            AlignItems.center
            BackgroundColor.value themeState.Theme.PrimaryColor
            Padding.value (rem 0, rem 2)
            ZIndex.value 100
            
            Media.query [Types.Media.MinWidth Utils.md] [
                JustifyContent.spaceBetween
            ]
        ]
        
        let headingStyle = [
            Display.none
            Color.value themeState.Theme.TextColor
            FontSize.xLarge
            Content.none
            TextDecoration.none
            
            Media.query [Types.Media.MinWidth Utils.md] [
                Display.block
                FontSize.xxLarge
            ]
            
            Hover [
                TextDecoration.revert
            ]
        ]
            
        let containerStyle = [
            Display.flex
            FlexDirection.row
            AlignItems.center
            JustifyContent.spaceBetween
            GridGap.value (rem 1)
            Width.value (vw 100)
            
            Media.query [Types.Media.MinWidth Utils.md] [
                JustifyContent.revert
                Width.revert
            ]
        ]
        
        let inputContainerStyle = [
            Display.flex
            AlignItems.center
        ]
            
        let inputStyle =  [
            Height.value (rem 3)
            MaxWidth.value (rem 13)
            BackgroundColor.value themeState.Theme.SecondaryColor
            Color.value themeState.Theme.TextColor
            Padding.value (rem 0, rem 0.5, rem 0, rem 0.5)
            BorderWidth.value (rem 0.1)
            BorderStyle.solid
            BorderColor.value themeState.Theme.AccentColor
            BorderRadius.value (rem 0.5, rem 0, rem 0, rem 0.5)
            FontSize.large
            
            Focus [
                BorderWidth.value (rem 0.2)
            ]
            
            Media.query [Types.Media.MinWidth Utils.md] [
                MaxWidth.value (rem 15)
            ]
        ]
        
        let searchButtonStyle = [
            Height.value (rem 3)
            MinWidth.value (rem 3)
            Border.none
            BorderRadius.value (rem 0, rem 0.5, rem 0.5, rem 0)
            BackgroundColor.value themeState.Theme.AccentColor
            FontSize.value (rem 1.2)
        ]
        
        let themeButtonStyle = [
            yield! searchButtonStyle
            BorderRadius.value (rem 0.5)
        ]
              
        let icon =
            match themeState.Theme.Type with
            | ThemeStore.ThemeType.Dark -> "fa-moon"
            | ThemeStore.ThemeType.Light -> "fa-sun"
              
        Html.header [
            prop.fss headerStyle
            prop.children [
                Html.a [
                    prop.text "Github Profile"
                    prop.className "fa-brands fa-github-alt"
                    prop.fss headingStyle
                    prop.href "/"
                ]
                Html.div [
                    prop.fss containerStyle
                    prop.children [
                        Html.button [
                            prop.fss themeButtonStyle
                            prop.onClick (fun _ ->
                                let themeType =
                                    match themeState.Theme.Type with
                                    | ThemeStore.ThemeType.Light -> ThemeStore.ThemeType.Dark
                                    | ThemeStore.ThemeType.Dark -> ThemeStore.ThemeType.Light
                                themeDispatch (ThemeStore.SetTheme themeType))
                            prop.children [
                                Html.span [
                                    prop.className $"fas {icon}"
                                ]
                            ]
                        ]
                        Html.div [
                            prop.fss inputContainerStyle
                            prop.children [
                                Html.input [
                                    prop.fss inputStyle
                                    prop.value input
                                    prop.onChange setInput
                                    prop.onKeyPress (fun key ->
                                        if key.which = 13 then Router.navigate ("search", ["user", input]))
                                ]
                                Html.button [
                                    prop.fss searchButtonStyle
                                    prop.onClick (fun _ -> Router.navigate ("search", ["user", input]))
                                    prop.children [
                                        Html.span [
                                            prop.className "fas fa-search"
                                        ]
                                    ]
                                ]
                            ]
                        ]
                    ]
                ]
            ]
        ]
