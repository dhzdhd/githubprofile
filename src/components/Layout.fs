namespace App

open Feliz

type Components () =
    [<ReactComponent>]
    static member Layout (child: Fable.React.ReactElement) =
        React.fragment [
            App.Components.Header ()
            Html.main [ child ]
            App.Components.Footer ()
        ]

