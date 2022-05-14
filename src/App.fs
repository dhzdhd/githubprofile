namespace App

open Feliz
open Feliz.UseListener
open Fss
open Feliz.Router
open Feliz.UseElmish
open Fss.Types

type Home () =
    [<ReactComponent>]
    static member Router () =
        let currentUrl, updateUrl = React.useState (Router.currentUrl ())
        let themeState = React.useContext ThemeStore.themeContext

        let containerStyle = [
            Height.value (vh 100)
            Display.flex
            FlexDirection.column
            GridGap.value (rem 1)
            AlignItems.center
            JustifyContent.center
            BackgroundColor.value themeState.Theme.PrimaryColor
        ]
        
        let errorPage =
            Html.div [
                prop.fss containerStyle
                prop.children [
                    Html.h1 [
                        prop.fss [ FontSize.xxxLarge; Color.value themeState.Theme.AccentColor ]
                        prop.text "404"
                    ]
                    Html.h2 [
                        prop.fss [ FontSize.xxLarge; Color.value themeState.Theme.TextColor ]
                        prop.text "Page does not exist!"
                    ]
                ]
            ]
        
        React.router [
            router.onUrlChanged updateUrl
            router.children [
                match currentUrl with
                | [] -> Components.Layout (Routes.Home ())
                | [ "search" ] -> Components.Layout (Routes.Search ())
                | _ -> errorPage
            ]
        ]
    
    [<ReactComponent>] 
    static member Index () =
        let state, dispatch = React.useElmish (ThemeStore.init, ThemeStore.update, [||])
        React.contextProvider (
            ThemeStore.themeContext,
            state ,
            React.contextProvider (
                ThemeStore.themeDispatchContext,
                dispatch,
                Home.Router()
            )
        )        
    
