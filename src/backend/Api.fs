namespace App

open Fable.SimpleHttp
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
          Language: Option<string>
          ForksCount: int
          Archived: bool
          OpenIssuesCount: int
          Topics: List<string>
          Forks: int
          StargazersCount: int }
      
    type Error =
        { Message: string
          User: string }
    
    let getData (user: string) (func: Option<Result<User * List<Repository>, Error>> -> unit) =
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
                
            match userResponse.content, repoResponse.content with
            | ResponseContent.Text rawUser, ResponseContent.Text rawRepo ->
                let decodedUser = Decode.Auto.fromString<User> (rawUser, caseStrategy = SnakeCase)
                let decodedRepo = Decode.Auto.fromString<List<Repository>> (rawRepo, caseStrategy = SnakeCase)
                
                match decodedUser, decodedRepo with
                | Ok user, Ok repo ->
                    func (Some (Ok (user, repo)))
                    printfn $"{user}"
                | Error errUser, _ ->
                    func (Some (Error { Message = errUser; User = user }))
                | _, Error errRepo ->
                    func (Some (Error { Message = errRepo; User = user }))
            | _ ->
                func None
        } |> Async.Start
