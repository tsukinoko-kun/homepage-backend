namespace homepage_backend.Controllers

open System
open Microsoft.AspNetCore.Mvc
open homepage_backend.Services
open Microsoft.AspNetCore.Http
open System.Threading.Tasks

[<ApiController>]
[<Route("[controller]")>]
type MessageController () =
    inherit ControllerBase()

    [<HttpGet>]
    member _.Get(mail: string, message: string, from: string): Task<IActionResult> =
        task {
            try
                if String.IsNullOrWhiteSpace(mail) then
                    return BadRequestObjectResult "Parameter 'mail' is required"
                elif String.IsNullOrWhiteSpace(message) then
                    return BadRequestObjectResult "Parameter 'message' is required"
                elif String.IsNullOrWhiteSpace(from) then
                    return BadRequestObjectResult "Parameter 'from' is required"
                elif message.Length > 512 then
                    return StatusCodeResult 414
                elif not (mail.Contains '@') then
                    return BadRequestObjectResult "Parameter 'mail' must be a valid email address"
                else
                    MessageService.send (mail, message)
                        |> Async.AwaitTask
                        |> ignore
                    return RedirectResult "https://frank-mayer.io"
            with exn ->
                return BadRequestObjectResult $"{exn.Message}\n{exn.StackTrace}"
        }
