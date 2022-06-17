namespace App

open Fable.React.Isomorphic
open Feliz
open Fss
open Fss.Types

type Routes () =
    [<ReactComponent>]
    static member Search userQuery =
        let themeState = React.useContext ThemeStore.themeContext
        let userInfo, setUserInfo = React.useState None
        
        let effect () =
            match userInfo with
            | None -> 
                Api.getData userQuery setUserInfo
            | Some x ->
                match x with
                | Ok (info, _) ->
                    match info.Login with
                    | x when x.ToLower() = userQuery -> ()
                    | _ -> Api.getData userQuery setUserInfo
                | Error err ->
                    match err.User with
                    | x when x.ToLower() = userQuery -> ()
                    | _ -> Api.getData userQuery setUserInfo
            
        (React.useEffect effect, [||]) |> ignore
        
        let containerStyle = [
            MinHeight.value (vh 100)
            Width.value (Percent 100)
            Padding.value (rem 7, rem 2, rem 3, rem 2)
            Display.flex
            FlexDirection.column
            GridGap.value (rem 5)
            BackgroundColor.value themeState.Theme.SecondaryColor
            Color.value themeState.Theme.TextColor
        ]
        
        let errorContainerStyle = [
            yield! containerStyle
            AlignItems.center
            JustifyContent.center
            FontSize.xxxLarge
            Color.value themeState.Theme.TextColor
        ]
        
        let firstSectionStyle = [
            Width.value (Percent 100)
            PaddingTop.value (rem 2)
            Display.flex
            FlexGrow.value 0
            JustifyContent.center
        ]
        
        let centeredContainerStyle = [
            Width.value (rem 60)
            MaxWidth.value (rem 60)
            Display.flex
            FlexDirection.column
            GridGap.value (rem 2)
                        
            Media.query [Types.Media.MinWidth Utils.md] [
                FlexDirection.row
                GridGap.normal
            ]
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
                BorderRadius.value (Percent 5)
            ]
        ]
        
        let userContainerStyle = [
            MinWidth.value (Percent 50)
            Display.flex
            FlexDirection.column
            JustifyContent.center
            GridGap.value (rem 1)
        ]
        
        let userNameStyle = [
            Color.value themeState.Theme.AccentColor
            FontSize.xxxLarge
            TextDecoration.none
            
            Hover [
                TextDecoration.revert
            ]
        ]
        
        let repoGridStyle = [
            Display.grid
            GridTemplateColumns.repeat (1, fr 1)
            GridGap.value (rem 1)
            
            Media.query [Types.Media.MinWidth Utils.md] [
                GridTemplateColumns.repeat (2, fr 1)
            ]
            
            Media.query [Types.Media.MinWidth Utils.lg] [
                GridTemplateColumns.repeat (3, fr 1)
            ]
            
            Media.query [Types.Media.MinWidth Utils.xxl] [
                GridTemplateColumns.repeat (4, fr 1)
            ]
        ]
        
        match userInfo with
        | Some res ->
            match res with
            | Ok (user, repo) ->
                Html.div [
                    prop.fss containerStyle
                    prop.children [
                        Html.section [
                            prop.id "about"
                            prop.fss firstSectionStyle
                            prop.children [
                                Html.div [
                                    prop.fss centeredContainerStyle
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
                                                    prop.fss userNameStyle
                                                    prop.text $"@{user.Login}"
                                                    prop.href user.HtmlUrl
                                                    prop.target "_blank"
                                                ]
                                                Html.p [
                                                    match user.Bio with
                                                    | Some bio -> prop.text bio
                                                    | None -> prop.text "No bio"
                                                    prop.fss [ FontSize.xLarge ]
                                                ]
                                                Html.div [
                                                    prop.fss [ Display.flex; AlignItems.center; GridGap.value (rem 1) ]
                                                    prop.children [
                                                        Html.span [
                                                            prop.className "fas fa-calendar"
                                                        ]
                                                        Html.span [
                                                            prop.text (user.CreatedAt.Substring (0, 10))
                                                        ]
                                                    ]
                                                ]
                                                Html.div [
                                                    prop.fss [ Display.flex; GridGap.value (rem 1); PaddingTop.value (rem 1) ]
                                                    prop.children [
                                                        Components.InfoBox "Followers" user.Followers
                                                        Components.InfoBox "Following" user.Following
                                                    ]
                                                ]
                                            ]
                                        ]
                                    ]
                                ]
                            ]
                        ]
                        Html.section [
                            prop.fss [ Display.grid; GridAutoFlow.row ]
                            prop.id "graphs"
                            prop.children [
                                Components.Charts repo 
                            ]
                        ]
                        Html.section [
                            prop.id "repos"
                            prop.fss [ Display.flex; FlexDirection.column; GridGap.value (rem 1) ]
                            prop.children [
                                Html.span [
                                    prop.fss [ FontSize.xxLarge ]
                                    prop.text "Repositories"
                                ]
                                Html.div [
                                    prop.fss repoGridStyle
                                    prop.children (
                                        match repo.Length with
                                        | x when x > 10 ->
                                            repo
                                            |> List.take 10
                                            |> List.map Components.RepoCard
                                        | _ -> repo |> List.map Components.RepoCard
                                    )
                                ]
                            ]
                        ]
                    ]
                ]
            | Error _ ->
                Html.div [
                    prop.fss errorContainerStyle
                    prop.children [
                        Html.h1 [
                            prop.fss [ FontSize.xxxLarge ]
                            prop.text "User not found"
                        ]
                    ]
                ]
        | None ->
            Components.LoadingSection () 
