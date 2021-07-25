namespace genre_explorer

module Repositories =

    open Npgsql.FSharp
    open genre_explorer.Models

    let connectionString : string =
        Sql.host "localhost"
        |> Sql.database "genre-explorer"
        |> Sql.username "admin"
        |> Sql.password "admin123"
        |> Sql.port 5432
        |> Sql.formatConnectionString

    let getMessage =
        { Text = "Hello world, from Giraffe!" }

    let createGenre =
        ()

    let readGenre id =
        ()

    let readGenres (): Genre list =
        connectionString
        |> Sql.connect
        |> Sql.query "SELECT * FROM genres"
        |> Sql.execute (fun read ->
            {
                Id = read.int "id"
                Name = read.text "name"
                Url = read.text "url"
            })

    let updateGenre id =
        ()

    let deleteGenre id =
        ()
