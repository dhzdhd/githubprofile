namespace App

open Elmish
open Feliz
open Fss.Types

module ThemeStore =
    type ThemeType =
        | Light
        | Dark
      
    type Msg =
        | SetTheme of ThemeType
        | NoOp
    
    type State =
        { PrimaryColor: Color
          SecondaryColor: Color
          TextColor: Color
          AccentColor: Color
          Type: ThemeType }
        
    let darkTheme =
        { PrimaryColor = Color.Hex "#14213d"
          SecondaryColor = Color.Hex "#1d3557"
          TextColor = Color.Hex "#e5e5e5"
          AccentColor = Color.Hex "#fca311"
          Type = ThemeType.Dark }
        
    let lightTheme =
        { PrimaryColor = Color.Hex "#e5e5e5"
          SecondaryColor = Color.Hex "#edede9"
          TextColor = Color.Hex "#14213d"
          AccentColor = Color.Hex "#fca311"
          Type = ThemeType.Light }

    type Model =
        { Theme: State }
       
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
