namespace homepage_backend

open Microsoft.AspNetCore.Builder
open Microsoft.Extensions.DependencyInjection
open Microsoft.Extensions.Hosting
open dotenv.net

module Program =
    let exitCode = 0
        

    [<EntryPoint>]
    let main args =
        DotEnv
            .Fluent()
            .WithoutExceptions()
            .WithTrimValues()
            .WithOverwriteExistingVars()
            .Load()

        let builder = WebApplication.CreateBuilder(args)

        builder.Services.AddControllers() |> ignore

        let app = builder.Build()

        app.UseHttpsRedirection() |> ignore

        app.UseAuthorization() |> ignore
        app.MapControllers() |> ignore

        app.Run()

        exitCode
