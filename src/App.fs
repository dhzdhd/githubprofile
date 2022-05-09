namespace App

open Feliz
open Feliz.UseListener
open Fss
open Feliz.Router
open Feliz.UseElmish

type Home () =
    [<ReactComponent>]
    static member Router () =
        let currentUrl, updateUrl = React.useState (Router.currentUrl ())
        
        React.router [
            router.pathMode
            router.onUrlChanged updateUrl
            router.children [
                match currentUrl with
                | [] -> Components.Layout (Routes.Home ())
                | [ "e" ] -> Html.h1 "e"
                | otherwise -> Html.h1 "Not found"
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
    
