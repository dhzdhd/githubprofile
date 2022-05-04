namespace App

open Feliz
open Fss
open Fss.Types

type Components () =
    [<ReactComponent>]
    static member Header () =
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
            Border.none
            Outline.value Keywords.Unset
            BorderRadius.value (rem 0.5, rem 0, rem 0, rem 0.5)
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
                                Svg.svg [
                                    svg.xmlns "http://www.w3.org/2000/svg"
                                    svg.viewBox (0, 0, 512, 512)
                                    svg.children [
                                        Svg.path [
                                            svg.d "M500.3 443.7l-119.7-119.7c27.22-40.41 40.65-90.9 33.46-144.7C401.8 87.79 326.8 13.32 235.2 1.723C99.01-15.51-15.51 99.01 1.724 235.2c11.6 91.64 86.08 166.7 177.6 178.9c53.8 7.189 104.3-6.236 144.7-33.46l119.7 119.7c15.62 15.62 40.95 15.62 56.57 0C515.9 484.7 515.9 459.3 500.3 443.7zM79.1 208c0-70.58 57.42-128 128-128s128 57.42 128 128c0 70.58-57.42 128-128 128S79.1 278.6 79.1 208z"
                                        ]
                                    ]
                                ]
                            ]
                        ]
                    ]
                ]
            ]
        ]
