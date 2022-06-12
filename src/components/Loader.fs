namespace App

open Feliz
open Fss
open Fss.Types

type Components () =
    [<ReactComponent>]
    static member LoadingSection () =
        let themeState = React.useContext ThemeStore.themeContext
        
        let loaderContainerStyle = [
            MinHeight.value (vh 100)
            Width.value (Percent 100)
            Padding.value (rem 7, rem 3, rem 3, rem 3)
            Display.flex
            FlexDirection.column
            GridGap.value (rem 5)
            BackgroundColor.value themeState.Theme.SecondaryColor
            Color.value themeState.Theme.TextColor
            AlignItems.center
            JustifyContent.center
            FontSize.xxxLarge
        ]
        
        Html.div [
            prop.fss loaderContainerStyle
            prop.children [
                Html.div [
                    prop.fss [
                        Label "Rotate"
                        AnimationName.value (keyframes [
                            frame 0 [ Transform.value [Transform.rotate (deg 0)] ]
                            frame 100 [ Transform.value [Transform.rotate (deg 360)] ]
                        ])
                        AnimationDuration.value (sec 1)
                        AnimationIterationCount.infinite
                    ]
                    prop.children [
                        Svg.svg [
                            svg.xmlns "http://www.w3.org/2000/svg"
                            svg.viewBox (0, 0, 50, 50)
                            svg.width 50
                            svg.height 50
                            svg.children [
                                Svg.circle [
                                    svg.r 5
                                    svg.cx 25
                                    svg.cy 25
                                    svg.fill (if themeState.Theme.Type = ThemeStore.ThemeType.Dark
                                              then "#ffffff"
                                              else "#000000")
                                    svg.strokeWidth 2
                                ]
                            ]
                        ]
                    ]
                ]
            ]
        ]
