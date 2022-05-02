namespace App

open Feliz
open Fss
open Feliz.Router

type Home () =
    static member private Fss () =
       0

    [<ReactComponent>] 
    static member Index () =
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
