module Main

open Feliz
open App
open Browser.Dom
open Fable.Core.JsInterop

importSideEffects "./styles/global.sass"

ReactDOM.render(
    Home.Index (), 
    document.getElementById "feliz-app"
)
