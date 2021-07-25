namespace genre_explorer

module Controllers =

    open Microsoft.AspNetCore.Http
    open FSharp.Control.Tasks
    open Giraffe
    open genre_explorer.Repositories

    let handleGetHello =
        fun (next : HttpFunc) (ctx : HttpContext) ->
            task {
                let response = getMessage
                return! json response next ctx
            }