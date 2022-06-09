namespace App

open Feliz
open Feliz.Recharts
open Feliz.Recharts.bar
open Fss
open Fss.Types
open Api

type Point = { name: string; pv: int; }


type Components () =
    [<ReactComponent>]  
    static member RepoCard (repo: List<Repository>) =
        let cardStyle = [
            
        ]
        
        Html.div [
            prop.fss cardStyle
            prop.children [
                
            ]
        ]