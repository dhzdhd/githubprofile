namespace App

open Feliz
open Fss
open Fss.Types
open Api

type Point = { name: string; pv: int; }


type Components () =
    [<ReactComponent>]  
    static member RepoCard (repo: Repository) =
        let themeState = React.useContext ThemeStore.themeContext

        let cardStyle = [
            MinHeight.value (rem 15)
            MinWidth.value (rem 20)
            BackgroundColor.value themeState.Theme.PrimaryColor
            Color.value themeState.Theme.TextColor
            BorderRadius.value (rem 1)
            Display.flex
            FlexDirection.column
            JustifyContent.spaceBetween
            Padding.value (rem 2, rem 2, rem 2, rem 2)
            TextDecoration.none
            TransitionDuration.value (ms 200)
            
            Hover [
                Transform.value [
                    Transform.scale 1.02
                ]
            ]
            
            ! Html.Span [
                Hover [ TextDecoration.none ]
            ]
        ]
        
        let divStyle = [
            Display.flex
            FlexDirection.column
            GridGap.value (rem 1)
            Overflow.clip
        ]
        
        let bottomDivStyle = [
            Display.flex
            FlexDirection.row
            JustifyContent.spaceBetween
        ]
        
        let iconDivStyle = [
            Display.flex
            GridGap.value (rem 0.5)
            AlignItems.center
        ]
        
        Html.a [
            prop.href repo.HtmlUrl
            prop.fss cardStyle
            prop.children [
                Html.div [
                    prop.fss divStyle
                    prop.children [
                        Html.div [
                            prop.fss [ FontSize.xLarge; Display.flex; AlignItems.center; GridGap.value (rem 1) ]
                            prop.children [
                                Html.span [
                                    prop.className "fas fa-book"
                                ]
                                Html.span [
                                    prop.fss [ FontSize.xxLarge; TextOverflow.clip; Overflow.clip ]
                                    prop.text repo.Name
                                ]       
                            ]
                        ]
                        Html.span [
                            prop.text (match repo.Description with
                                       | Some desc -> desc
                                       | None -> "No description")
                            prop.fss [ FontSize.large ]
                        ]
                    ]
                ]
                Html.div [
                    prop.fss bottomDivStyle
                    prop.children [
                        Html.div [
                            prop.fss iconDivStyle
                            prop.children [
                                Html.span [
                                    prop.className "fas fa-star"
                                ]
                                Html.span [
                                    prop.text (repo.StargazersCount.ToString ())
                                ]       
                            ]
                        ]
                        Html.div [
                            prop.fss iconDivStyle
                            prop.children [
                                Html.span [
                                    prop.className "fa-solid fa-code-fork"
                                ]
                                Html.span [
                                    prop.text (repo.ForksCount.ToString ())
                                ]       
                            ]
                        ]
                    ]
                ]
            ]
        ]