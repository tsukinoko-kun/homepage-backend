namespace homepage_backend.Controllers

open System
open Microsoft.AspNetCore.Mvc
open homepage_backend.Services
open System.Threading.Tasks

[<ApiController>]
[<Route("[controller]")>]
type MessageController () =
    inherit ControllerBase()

    [<HttpGet>]
    member _.Get(mail: string, message: string) =
        try
            if String.IsNullOrWhiteSpace(mail) then
                failwith "Mail is not specified"
            elif String.IsNullOrWhiteSpace(message) then
                failwith "Message is not specified"
            else
                MessageService.send (mail, message)
        with exn ->
            Task.FromResult exn.Message
