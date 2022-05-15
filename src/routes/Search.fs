namespace App

open Feliz
open Fss
open Fss.Types

type Routes () =
    [<ReactComponent>]
    static member Search () =
        let themeState = React.useContext ThemeStore.themeContext
        let userInfo, setUserInfo = React.useState None
        
        let effect () =
            if userInfo = None then 
                Api.getData "dhzdhd" setUserInfo
            ()
            
        (React.useEffect effect, [||]) |> ignore
        
        let containerStyle = [
            MinHeight.value (vh 100)
            Width.value (Percent 100)
            Padding.value (rem 7, rem 3, rem 3, rem 3)
            Display.flex
            BackgroundColor.value themeState.Theme.SecondaryColor
            Color.value themeState.Theme.TextColor
        ]
        
        let firstSectionStyle = [
            Display.flex
            FlexDirection.row
            Width.value (Percent 100)
        ]
        
        let imageContainerStyle = [
            Display.flex
            JustifyContent.center
            AlignItems.center
            MinWidth.value (Percent 50)
        ]
        
        let imageStyle = [
            Height.value (rem 15)
            BorderRadius.value (Percent 50)
            TransitionDuration.value (sec 0.5)
            
            Hover [
                BorderRadius.value (Percent 0)
            ]
        ]
        
        let userContainerStyle = [
            MinWidth.value (Percent 50)
            Display.flex
            FlexDirection.column
            JustifyContent.center
            GridGap.value (rem 1)
        ]
        
        match userInfo with
        | Some (user, repo) ->  
            Html.div [
                prop.fss containerStyle
                prop.children [
                    Html.section [
                        prop.fss firstSectionStyle
                        prop.children [
                            Html.div [
                                prop.fss imageContainerStyle
                                prop.children [
                                    Html.img [
                                        prop.src user.AvatarUrl
                                        prop.alt "Avatar"
                                        prop.fss imageStyle
                                    ]
                                ]
                            ]
                            Html.div [
                                prop.fss userContainerStyle
                                prop.children [
                                    Html.a [
                                        prop.fss [ Color.value themeState.Theme.AccentColor; FontSize.xxxLarge ]
                                        prop.text $"@{user.Login}"
                                        prop.href user.HtmlUrl
                                        prop.target "_blank"
                                    ]
                                    Html.p [
                                        prop.text user.Bio.Value
                                        prop.fss [ FontSize.xLarge ]
                                    ]
                                    Html.div [
                                        prop.fss [Display.flex; AlignItems.center; GridGap.value (rem 1)]
                                        prop.children [
                                            Html.span [
                                                prop.className "fas fa-calendar"
                                            ]
                                            Html.span [
                                                prop.text (user.CreatedAt.Substring (0, 10))
                                            ]
                                        ]
                                    ]
                                ]
                            ]
                        ]
                    ]
                    Html.section [
                        
                    ]
                    Html.section [
                        
                    ]
                ]
            ]
        | None ->
            Html.h1 [ prop.text "Loading" ]
