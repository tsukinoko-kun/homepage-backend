namespace homepage_backend

open System
open System.Text;
open System.Net.Http
open System.Net.Http.Json

module message =

    let env key =
        match Environment.GetEnvironmentVariable key with
        | null -> failwith $"Environment variable '{key}' not set"
        | value -> value

    let apiKey = env "MAILGUN_API_KEY"
    let domainName = env "MAILGUN_DOMAIN_NAME"

    let send (mail: string, message: string) =
        use httpClient = new HttpClient()
        use request = new HttpRequestMessage(new HttpMethod("POST"), $"https://api.mailgun.net/v3/{domainName}/messages")
        let mutable base64authorization = Convert.ToBase64String (Encoding.ASCII.GetBytes ($"api:{apiKey}"))
        request.Headers.TryAddWithoutValidation ("Authorization", $"Basic {base64authorization}") |> ignore
        let mutable multipartContent = new MultipartFormDataContent()
        multipartContent.Add (new StringContent(mail), "from")
        multipartContent.Add (new StringContent("mail@frank-mayer.io"), "to")
        multipartContent.Add (new StringContent("New message from homepage"), "subject")
        multipartContent.Add (new StringContent(message), "text")
        request.Content <- multipartContent
        task {
            let! resp = httpClient.SendAsync request
            return resp.Content.ReadAsStringAsync()
        }
        |> Async.AwaitTask
        |> Async.RunSynchronously

        
