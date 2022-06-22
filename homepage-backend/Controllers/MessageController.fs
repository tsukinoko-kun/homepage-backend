namespace homepage_backend.Controllers

open System
open Microsoft.AspNetCore.Mvc
open homepage_backend.Services
open Microsoft.AspNetCore.Http

[<ApiController>]
[<Route("[controller]")>]
type MessageController (ctx: IHttpContextAccessor) =
    inherit ControllerBase()

    [<HttpGet>]
    member _.Get(mail: string, message: string) =
        let resp = ctx.HttpContext.Response
        task {
            try
                if String.IsNullOrWhiteSpace(mail) then
                    resp.StatusCode <- 422
                    do! resp.WriteAsync "Parameter 'mail' is not specified"
                elif String.IsNullOrWhiteSpace(message) then
                    resp.StatusCode <- 422
                    do! resp.WriteAsync "Parameter 'message' is not specified"
                elif message.Length > 512 then
                    resp.StatusCode <- 414
                    do! resp.WriteAsync "Parameter 'message' is too long"
                elif not (mail.Contains '@') then
                    resp.StatusCode <- 422
                    do! resp.WriteAsync "Parameter 'mail' is not a valid email address"
                else
                    do! MessageService.send (mail, message)
                        |> resp.WriteAsync
                    resp.StatusCode <- 302
                    resp.Headers.Add ("Location", "/")
            with exn ->
                resp.StatusCode <- 500
                do! resp.WriteAsync $"{exn.Message}\n{exn.StackTrace}"
        }
