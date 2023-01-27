namespace App

open Fable.SimpleHttp
open Fable.SimpleJson
open Thoth.Json

module Api =
    type User =
        { Name: string
          Login: string
          AvatarUrl: string
          HtmlUrl: string
          Blog: Option<string>
          Location: Option<string>
          Email: Option<string>
          Bio: Option<string>
          PublicRepos: int
          Followers: int
          Following: int
          CreatedAt: string }
        
    type Repository =
        { Name: string
          FullName: string
          HtmlUrl: string
          Description: Option<string>
          Fork: bool
          LanguagesUrl: string
          Language: Option<string>
          OrganizationsUrl: Option<string>
          ForksCount: int
          Archived: bool
          OpenIssuesCount: int
          Topics: Option<List<string>>
          Forks: int
          StargazersCount: int }
      
    type Error =
        { Message: string
          User: string }

    type Lang =
        { Languages: List<{| Lang: string |}> }
    
    let getLanguages (repoList: List<Repository>) (setState: Option<Result<Map<string, int>, Error>> -> unit) =
        // Works but due to GitHub's insanely high rate limits, not currently viable
        
        let mutable repoMap = Map.empty
        
        repoList |> List.map(fun e -> e.LanguagesUrl) |> List.iter (fun url -> 
            async {
                let! response =
                    Http.request url
                    |> Http.method GET
                    |> Http.send
                
                printfn $"{response.responseHeaders}"
                
                match response.statusCode with
                | 403 ->
                    setState (Some (Error { Message = "Rate limited!"; User = "" }))
                    return ()
                | _ ->
                    match response.content with
                    | ResponseContent.Text rawLang ->
                        let decodedLang = SimpleJson.tryParse rawLang
                        match decodedLang with
                        | Some (JObject dict) ->
                            let map =
                                dict |> Map.map (fun _ (value: Json) ->
                                    match value with
                                    | JNumber num -> num |> int
                                    | _ -> 0
                                )
                            printfn $"{map}"
                            map |> Map.map (fun key1 value1 ->
                                match repoMap.TryFind key1 with
                                | Some value2 ->
                                    repoMap <- (repoMap.Remove key1)
                                    repoMap <- repoMap.Add (key1, value1 + value2)
                                | None -> repoMap <- repoMap.Add (key1, value1) )
                            |> ignore
                        | _ -> ()
                        ()
                    | _ -> ()
            } |> Async.Start
            
            match repoMap.IsEmpty with
            | true -> setState None
            | false -> setState (Some (Ok repoMap))
        )
        
    let getData (user: string) (setState: Option<Result<User * List<Repository>, Error>> -> unit) =
        let url = $"https://api.github.com/users/{user}"
        
        async {
            let! userResponse =
                Http.request url 
                |> Http.method GET
                |> Http.send
                
            let! repoResponse =
                Http.request (url + "/repos?per_page=100")
                |> Http.method GET
                |> Http.send
                
            match userResponse.content, repoResponse.content with
            | ResponseContent.Text rawUser, ResponseContent.Text rawRepo ->
                let decodedUser = Decode.Auto.fromString<User> (rawUser, caseStrategy = SnakeCase)
                let decodedRepo = Decode.Auto.fromString<List<Repository>> (rawRepo, caseStrategy = SnakeCase)
                
                match decodedUser, decodedRepo with
                | Ok user, Ok repo ->
                    setState (Some (Ok (user, repo)))
                | Error errUser, _ ->
                    setState (Some (Error { Message = errUser; User = user }))
                | _, Error errRepo ->
                    setState (Some (Error { Message = errRepo; User = user }))
            | _ ->
                setState None
        } |> Async.Start
