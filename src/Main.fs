module Main

open Feliz
open Browser.Dom
open Fable.Core.JsInterop

importSideEffects "./styles/global.sass"

ReactDOM.render(
    App.Home.Index (),
    document.getElementById "feliz-app"
)
