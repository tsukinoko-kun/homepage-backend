namespace homepage_backend.Controllers

open Microsoft.AspNetCore.Mvc
open homepage_backend.Services

[<ApiController>]
[<Route("[controller]")>]
type MessageController () =
    inherit ControllerBase()

    [<HttpGet>]
    member _.Get(mail: string, message: string) =
        (mail, message) |> MessageService.send
