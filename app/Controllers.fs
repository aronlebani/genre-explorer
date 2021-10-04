module Controllers

open Microsoft.AspNetCore.Http
open FSharp.Control.Tasks
open Giraffe
open Repositories
open Models

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

let handleGetGenreDerivatives (id: int): HttpHandler =
    fun (next: HttpFunc) (ctx: HttpContext) ->
        task {
            let response: Genre list = readGenreDerivatives id
            return! json response next ctx
        }

let handleGetGenreOrigins (id: int): HttpHandler =
    fun (next: HttpFunc) (ctx: HttpContext) ->
        task {
            let response: Genre list = readGenreOrigins id
            return! json response next ctx
        }

let handleGetGenreSubgenres (id: int): HttpHandler =
    fun (next: HttpFunc) (ctx: HttpContext) ->
        task {
            let response: Genre list = readGenreSubgenres id
            return! json response next ctx
        }

let handleGetGenreFusions (id: int): HttpHandler =
    fun (next: HttpFunc) (ctx: HttpContext) ->
        task {
            let response: Genre list = readGenreFusions id
            return! json response next ctx
        }

let handlePostGenre: HttpHandler =
    fun (next: HttpFunc) (ctx: HttpContext) ->
        task {
            let! payload = ctx.BindJsonAsync<Genre>()
            let response: int = createGenre payload
            return! json response next ctx
        }

let handlePostDerivative (id: int): HttpHandler =
    fun (next: HttpFunc) (ctx: HttpContext) ->
        task {
            let! payload = ctx.BindJsonAsync<Genre>()
            let response: int = createDerivative id payload
            return! json response next ctx
        }

let handlePostOrigin (id: int): HttpHandler =
    fun (next: HttpFunc) (ctx: HttpContext) ->
        task {
            let! payload = ctx.BindJsonAsync<Genre>()
            let response: int = createOrigin id payload
            return! json response next ctx
        }

let handlePostSubgenre (id: int): HttpHandler =
    fun (next: HttpFunc) (ctx: HttpContext) ->
        task {
            let! payload = ctx.BindJsonAsync<Genre>()
            let response: int = createSubgenre id payload
            return! json response next ctx
        }

let handlePostFusion (id: int): HttpHandler =
    fun (next: HttpFunc) (ctx: HttpContext) ->
        task {
            let! payload = ctx.BindJsonAsync<Genre>()
            let response: int = createFusion id payload
            return! json response next ctx
        }

let handleDeleteGenre (id: int): HttpHandler =
    fun (next: HttpFunc) (ctx: HttpContext) ->
        task {
            let response: int = deleteGenre id
            return! json response next ctx
        }
