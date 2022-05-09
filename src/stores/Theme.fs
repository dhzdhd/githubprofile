namespace App

open Elmish
open Feliz
open Fss.Types

module ThemeStore =    
    type State =
        { PrimaryColor: Color
          SecondaryColor: Color
          AccentColor: Color }
        
    let darkTheme =
        { PrimaryColor = Color.Hex "#1d3557"
          SecondaryColor = Color.Hex "#457b9d"
          AccentColor = Color.Hex "#e63946" }
        
    let lightTheme =
        { PrimaryColor = Color.Hex "#457b9d"
          SecondaryColor = Color.Hex "#1d3557"
          AccentColor = Color.Hex "#e63946" }

    type Model =
        { Theme: State }
     
    type ThemeType =
        | Light
        | Dark
      
    type Msg =
        | SetTheme of ThemeType
        | NoOp
       
    let init () =
        { Theme = darkTheme },
        Cmd.none
     
    let update (msg: Msg) (model: Model) =
        match msg with
        | SetTheme type_ ->
            { model
              with Theme =
                    match type_ with
                    | Dark -> darkTheme
                    | Light -> lightTheme },
            Cmd.none
        | NoOp ->
            model, Cmd.none
            
    let themeContext: Fable.React.IContext<Model> = React.createContext "theme"
    let themeDispatchContext: Fable.React.IContext<Msg -> unit> = React.createContext "themeDispatch"
