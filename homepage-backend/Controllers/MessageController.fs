namespace homepage_backend.Controllers

open System
open Microsoft.AspNetCore.Mvc
open homepage_backend.Services

[<ApiController>]
[<Route("[controller]")>]
type MessageController () =
    inherit ControllerBase()

    [<HttpGet>]
    member _.Get(mail: string, message: string) =
        if String.IsNullOrWhiteSpace(mail) then
            failwith "Mail is not specified"
        elif String.IsNullOrWhiteSpace(message) then
            failwith "Message is not specified"
        else
            (mail, message) |> MessageService.send
