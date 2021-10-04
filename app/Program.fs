open System
open Microsoft.AspNetCore.Builder
open Microsoft.AspNetCore.Cors.Infrastructure
open Microsoft.AspNetCore.Hosting
open Microsoft.Extensions.Hosting
open Microsoft.Extensions.Logging
open Microsoft.Extensions.DependencyInjection
open Giraffe
open Controllers

// ---------------------------------
// Web app
// ---------------------------------

let webApp =
    choose [
        subRoute "/api/genres"
            (choose [
                GET >=> choose [
                    route "" >=> handleGetGenres
                    routef "/%i" handleGetGenre
                    routef "/%i/derivatives" handleGetGenreDerivatives
                    routef "/%i/origins" handleGetGenreOrigins
                    routef "/%i/subgenres" handleGetGenreSubgenres
                    routef "/%i/fusion" handleGetGenreFusions
                ]
                POST >=> choose [
                    route "" >=> handlePostGenre
                    routef "/%i/derivative" handlePostDerivative
                    routef "/%i/origin" handlePostOrigin
                    routef "/%i/subgenre" handlePostSubgenre
                    routef "/%i/fusion" handlePostFusion
                ]
                DELETE >=> routef "/%i" handleDeleteGenre
            ])
        setStatusCode 404 >=> text "Not Found"
    ]

// ---------------------------------
// Error handler
// ---------------------------------

let errorHandler (ex : Exception) (logger : ILogger) =
    logger.LogError(ex, "An unhandled exception has occurred while executing the request.")
    clearResponse >=> setStatusCode 500 >=> text ex.Message

// ---------------------------------
// Config and Main
// ---------------------------------

let configureCors (builder : CorsPolicyBuilder) =
    builder
        .WithOrigins(
            "http://localhost:5000",
            "https://localhost:5001")
       .AllowAnyMethod()
       .AllowAnyHeader()
       |> ignore

let configureApp (app : IApplicationBuilder) =
    let env = app.ApplicationServices.GetService<IWebHostEnvironment>()
    (match env.IsDevelopment() with
    | true  ->
        app.UseDeveloperExceptionPage()
    | false ->
        app .UseGiraffeErrorHandler(errorHandler)
            .UseHttpsRedirection())
        .UseCors(configureCors)
        .UseGiraffe(webApp)

let configureServices (services : IServiceCollection) =
    services.AddCors()    |> ignore
    services.AddGiraffe() |> ignore

let configureLogging (builder : ILoggingBuilder) =
    builder.AddConsole()
           .AddDebug() |> ignore

[<EntryPoint>]
let main args =
    Host.CreateDefaultBuilder(args)
        .ConfigureWebHostDefaults(
            fun webHostBuilder ->
                webHostBuilder
                    .Configure(Action<IApplicationBuilder> configureApp)
                    .ConfigureServices(configureServices)
                    .ConfigureLogging(configureLogging)
                    |> ignore)
        .Build()
        .Run()
    0
