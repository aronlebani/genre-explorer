namespace Crawler

module Api =

    open FSharp.Data

    let postGenre (url: string, name: string) =
        let genre: Genre = {
            Name = name
            Url = url
        }

        Http.RequestString (
            "http://localhost:5000/api/genres",
            headers = [ ContentType HttpContentTypes.Json ],
            body =
                genre
                |> Json.serialize
                |> TextRequest
        )

    let postDerivative (id: int) (url: string, name: string) =
        let genre: Genre = {
            Name = name
            Url = url
        }

        Http.RequestString (
            "http://localhost:5000/api/genres" + id
            headers = [ ContentType HttpContentTypes.Json ],
            body =
                genre
                |> Json.serialize
                |> TextRequest
        )
