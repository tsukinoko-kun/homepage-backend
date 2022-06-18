namespace homepage_backend

open System
open System.IO
open System.Net.Http
open System.Net.Http.Json

module message =

    let env key =
        match Environment.GetEnvironmentVariable key with
        | null -> failwith $"Environment variable '{key}' not set"
        | value -> value

    type Post (from: string, text: string) =
        let from = from
        let ``to`` = "mail@frank-mayer.io"
        let subject = "New message from homepage"
        let text = text

    let send (mail: string, message: string) =
        task {
        let apiKey = env "MAILGUN_API_KEY"
        let domainName = env "MAILGUN_DOMAIN_NAME"

        // new Post
        let post = new Post(mail, message)

        use client = new HttpClient()
        let byteArray = UTF8.bytes $"api:{apiKey}"
        let b64EAuthString = Convert.ToBase64String byteArray
        client.DefaultRequestHeaders.Authorization = new Headers.AuthenticationHeaderValue("Basic", b64EAuthString)
            |> ignore
            
        let! resp = client.PostAsJsonAsync($"https://api.mailgun.net/v3/{domainName}/messages", post)
        return resp.Content.ReadAsStringAsync()
        }
        |> Async.AwaitTask
        |> Async.RunSynchronously

        
