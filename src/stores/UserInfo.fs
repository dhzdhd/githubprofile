/// ? To implement loading action on changing user

namespace App

open Elmish
open Feliz

module UserInfoStore =
    type Model = Option<Result<Api.User * List<Api.Repository>,Api.Error>>
    
    type Msg =
        | SetUserInfo of Model
    
    let init (): Model * Cmd<'a> =
        None, Cmd.none
        
    let update (msg: Msg) (_: Model) =
        match msg with
        | SetUserInfo state -> state, Cmd.none
        
    let userInfoContext: Fable.React.IContext<Model> = React.createContext "userInfo"
    let userInfoDispatchContext: Fable.React.IContext<Msg -> unit> = React.createContext "userInfoDispatch"