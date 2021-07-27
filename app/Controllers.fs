namespace genre_explorer

module Controllers =

    open Microsoft.AspNetCore.Http
    open FSharp.Control.Tasks
    open Giraffe
    open genre_explorer.Repositories
    open genre_explorer.Models

    let handleGetGenres: HttpHandler =
        fun (next: HttpFunc) (ctx: HttpContext) ->
            task {
                let response: Genre list = readGenres
                return! json response next ctx
            }

    let handleGetGenre (id: int): HttpHandler =
        fun (next: HttpFunc) (ctx: HttpContext) ->
            task {
                let response: Genre = readGenre id
                return! json response next ctx
            }

    let handlePostGenre: HttpHandler =
        fun (next: HttpFunc) (ctx: HttpContext) ->
            task {
                let! payload = ctx.BindJsonAsync<Genre>()
                let response: int = createGenre payload
                return! json response next ctx
            }

    let handleDeleteGenre (id: int): HttpHandler =
        fun (next: HttpFunc) (ctx: HttpContext) ->
            task {
                let response: int = deleteGenre id
                return! json response next ctx
            }