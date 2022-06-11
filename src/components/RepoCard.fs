namespace App

open Feliz
open Feliz.Recharts
open Feliz.Recharts.bar
open Fss
open Fss.Types
open Api

type Point = { name: string; pv: int; }


type Components () =
    [<ReactComponent>]  
    static member RepoCard (repo: Repository) =
        let themeState = React.useContext ThemeStore.themeContext

        let cardStyle = [
            Height.value (rem 15)
            MaxWidth.value (rem 25)
            BackgroundColor.value themeState.Theme.PrimaryColor
            Color.value themeState.Theme.TextColor
            BorderRadius.value (rem 1)
            Display.flex
            FlexDirection.column
            GridGap.value (rem 1)
            Padding.value (rem 2, rem 2, rem 2, rem 2)
            
            ! Html.Span [
                Hover [ TextDecoration.none ]
            ]
        ]
        
        Html.a [
            prop.href repo.HtmlUrl
            prop.fss cardStyle
            prop.children [
                Html.div [
                    prop.fss [ FontSize.xLarge; Display.flex; AlignItems.center; GridGap.value (rem 1) ]
                    prop.children [
                        Html.span [
                            prop.className "fas fa-book"
                        ]
                        Html.span [
                            prop.fss [ FontSize.xxLarge ]
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
                Html.div [
                    Html.span [
                        
                    ]
                ]
            ]
        ]