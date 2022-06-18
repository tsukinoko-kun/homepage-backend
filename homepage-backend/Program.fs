namespace homepage_backend

open System
open System.IO
open System.Threading.Tasks
open Microsoft.AspNetCore.Builder
open Microsoft.Extensions.Hosting
open Microsoft.AspNetCore.Http
open Newtonsoft.Json

module Program =
    open homepage_backend

    [<EntryPoint>]
    let main args =
        let builder = WebApplication.CreateBuilder(args)
        let app = builder.Build()

        app.MapPost(
            "/message",
            Func<HttpContext, Task<string>>(fun (context) ->
                task {
                    let bodyReader = new StreamReader(context.Request.Body)
                    let! bodyStr = bodyReader.ReadToEndAsync()
                    let body = JsonConvert.DeserializeObject<MessageInput> bodyStr
                    if String.IsNullOrWhiteSpace body.Mail then
                        context.Response.StatusCode <- 400
                        return "Mail is required"
                    elif String.IsNullOrWhiteSpace body.Message then
                        context.Response.StatusCode <- 400
                        return "Message is required"
                    else
                        return! message.send(body.Mail, body.Message)
                }
            )
        )
        |> ignore

        app.Run()

        0 // Exit code
