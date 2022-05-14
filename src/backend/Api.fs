﻿namespace App

open Fable.SimpleHttp
open Thoth.Json

module Api =
    type User =
        { Name: string
          Login: string
          Avatar: Option<string>
          HtmlUrl: string
          Blog: Option<string>
          Location: Option<string>
          Email: Option<string>
          Bio: Option<string>
          PublicRepos: int
          Followers: int
          Following: int }
        
    type Repository =
        { Name: string
          FullName: string
          HtmlUrl: string
          Description: Option<string>
          Fork: bool
          Language: string
          ForksCount: int
          Archived: bool
          OpenIssuesCount: int
          License: {| Key: string
                      Name: string
                      Url: string |}
          Topics: List<string>
          Forks: int
          StargazersCount: int }
    
    let getData (user: string) =
        let url = $"https://api.github.com/users/{user}"
        
        async {
            let! userResponse =
                Http.request url 
                |> Http.method GET
                |> Http.send
                
            let! repoResponse =
                Http.request (url + "/repos")
                |> Http.method GET
                |> Http.send
                
            printfn $"{repoResponse.content}"
            
            match userResponse.content, repoResponse.content with
            | ResponseContent.Text rawUser, ResponseContent.Text rawRepo ->
                let decodedUser = Decode.Auto.fromString<User> (rawUser, caseStrategy = SnakeCase)
                let decodedRepo = Decode.Auto.fromString<List<Repository>> (rawRepo, caseStrategy = SnakeCase)
                match decodedUser, decodedRepo with
                | Ok user, Ok repo -> Some (user, repo)
                | Error errUser, Error errRepo -> None
                | _, _ -> None
            | _ ->
                None
        } |> Async.Start