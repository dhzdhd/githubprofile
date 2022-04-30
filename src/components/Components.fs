namespace App

open Feliz
open Feliz.Router

type Components =
    [<ReactComponent>]
    static member Router () =
        let currentUrl, updateUrl = React.useState (Router.currentUrl ())

        React.router [
            router.onUrlChanged updateUrl
            router.children [
                match currentUrl with
                | [] -> Html.h1 "Index"
                | otherwise -> Html.h1 "Not found"
            ]
        ]
