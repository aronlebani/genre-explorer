module Api

open FSharp.Data
open FSharp.Data.HttpRequestHeaders
open Genre

let postGenre (genre: Genre): int =
    let url = "http://localhost:5000/api/genres"
    let headers = [ ContentType HttpContentTypes.Json ]
    let payload =
        genre
        |> Json.serialize
        |> TextRequest

    let response = Http.Request (url, headers = headers, body = payload)

    match response.Body with
    | Text text -> text |> int
    | _ -> 0 

let postDerivative (id: int) (genre: Genre): int =
    let url = "http://localhost:5000/api/genres/" + sprintf "%i" id + "/derivative"
    let headers = [ ContentType HttpContentTypes.Json ]
    let payload =
        genre
        |> Json.serialize
        |> TextRequest

    let response = Http.Request (url, headers = headers, body = payload)

    match response.Body with
    | Text text -> text |> int
    | _ -> 0
