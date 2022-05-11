namespace App

open Feliz

type Routes () =
    [<ReactComponent>]
    static member Search () =
        let effect () =
            ()
            
        let _ = React.useEffect effect, [||]
        
        Html.div [
            Html.h1 "efgiej"
        ]
