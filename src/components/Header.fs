namespace App

open Feliz
open Fss
open Fss.Types

type Components () =
    [<ReactComponent>]
    static member Header () =
        let a = React.useReducer
        
        let headerStyle = [
            Height.value (rem 5)
            Display.flex
            FlexDirection.row
            JustifyContent.spaceBetween
            AlignItems.center
            Padding.value (rem 0, rem 2)
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
        
        let buttonStyle = [
            Height.value (rem 3)
            Width.value (rem 3)
            Border.none
            BorderRadius.value (rem 0, rem 0.5, rem 0.5, rem 0)
        ]
        
        let headingStyle = [
            FontSize.xxLarge
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
                            prop.fss buttonStyle
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
