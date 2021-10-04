module Api

open FSharp.Data
open FSharp.Data.HttpRequestHeaders
open Genre

let postGenre (genre: Genre): string =
    let url = "http://localhost:5000/api/genres"
    let headers = [ ContentType HttpContentTypes.Json ]
    let payload =
        genre
        |> Json.serialize
        |> TextRequest

    Http.RequestString (url, headers = headers, body = payload)

let postDerivative (id: int) (genre: Genre): string =
    let url = "http://localhost:5000/api/genres" + sprintf "%i" id
    let headers = [ ContentType HttpContentTypes.Json ]
    let payload =
        genre
        |> Json.serialize
        |> TextRequest

    Http.RequestString (url, headers = headers, body = payload)
