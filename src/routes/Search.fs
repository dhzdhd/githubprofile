namespace App

open Feliz
open Fss
open Fss.Types

type Routes () =
    [<ReactComponent>]
    static member Search () =
        let loading, setLoading = React.useState true
        let themeState = React.useContext ThemeStore.themeContext
        let mutable userInfo = None
        
        let effect () =
            if userInfo = None then 
                userInfo = Some (Api.getData "dhzdhd") |> ignore
                setLoading false
            ()
            
        (React.useEffect effect, [||]) |> ignore
        
        let containerStyle = [
            MinHeight.value (vh 100)
            Display.flex
            JustifyContent.center
            AlignItems.center
            BackgroundColor.value themeState.Theme.SecondaryColor
            Color.value themeState.Theme.TextColor
        ]
        
        Html.div [
            prop.fss containerStyle
            prop.children [
                if loading then Html.h1 [
                    prop.text "Loading"
                    prop.fss [ Color.black ]
                ]
                else Html.section []
            ]
        ]
