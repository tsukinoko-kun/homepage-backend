namespace homepage_backend

open System
open Microsoft.AspNetCore.Builder
open Microsoft.Extensions.Hosting

module Program =
    open System.Threading.Tasks

    [<EntryPoint>]
    let main args =
        let builder = WebApplication.CreateBuilder(args)
        let app = builder.Build()

        app.MapGet("/", Func<Task<string>>(fun () -> message.send("mail@frank-mayer.io","Hello World"))) |> ignore

        app.Run()

        0 // Exit code
