namespace App

open Feliz
open Fss
open Fss.Types

//.button-28 {
//  appearance: none;
//  background-color: transparent;
//  border: 2px solid #1A1A1A;
//  border-radius: 15px;
//  box-sizing: border-box;
//  color: #3B3B3B;
//  cursor: pointer;
//  display: inline-block;
//  font-size: 16px;
//  font-weight: 600;
//  line-height: normal;
//  margin: 0;
//  min-height: 60px;
//  min-width: 0;
//  outline: none;
//  padding: 16px 24px;
//  text-align: center;
//  text-decoration: none;
//  transition: all 300ms cubic-bezier(.23, 1, 0.32, 1);
//  user-select: none;
//  -webkit-user-select: none;
//  touch-action: manipulation;
//  width: 100%;
//  will-change: transform;
//}
//
//.button-28:disabled {
//  pointer-events: none;
//}
//
//.button-28:hover {
//  color: #fff;
//  background-color: #1A1A1A;
//  box-shadow: rgba(0, 0, 0, 0.25) 0 8px 15px;
//  transform: translateY(-2px);
//}
//
//.button-28:active {
//  box-shadow: none;
//  transform: translateY(0);
//}

type Components () =
    [<ReactComponent>]
    static member Header () =
        let themeState = React.useContext ThemeStore.themeContext
        let themeDispatch = React.useContext ThemeStore.themeDispatchContext
        
        let headerStyle = [
            Height.value (rem 5)
            Width.value (vw 100)
            Position.fixed'
            Display.flex
            FlexDirection.row
            JustifyContent.spaceBetween
            AlignItems.center
            BackgroundColor.value themeState.Theme.PrimaryColor
            Padding.value (rem 0, rem 2)
        ]
        
        let headingStyle = [
            FontSize.xxLarge
            Color.value themeState.Theme.TextColor
        ]
            
        let containerStyle = [
            Display.flex
            FlexDirection.row
            AlignItems.center
            JustifyContent.center
        ]
            
        let inputStyle =  [
            Height.value (rem 3)
            BackgroundColor.value themeState.Theme.SecondaryColor
            Color.value themeState.Theme.TextColor
            BorderWidth.value (rem 0.1)
            BorderStyle.solid
            BorderColor.value themeState.Theme.AccentColor
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
            BackgroundColor.value themeState.Theme.AccentColor
            FontSize.value (rem 1.2)
        ]
        
        let themeButtonStyle = [
            yield! searchButtonStyle
            MarginRight.value (rem 1)
            BorderRadius.value (rem 0.5)
        ]
              
        let icon =
            match themeState.Theme.Type with
            | ThemeStore.ThemeType.Dark -> "fa-moon"
            | ThemeStore.ThemeType.Light -> "fa-sun"
              
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
                    ]
                ]
            ]
        ]
