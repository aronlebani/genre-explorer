namespace genre_explorer

module Repositories =

    open Npgsql.FSharp
    open genre_explorer.Models

    let connectionString: string =
        Sql.host "db"
        |> Sql.database "postgres"
        |> Sql.username "postgres"
        |> Sql.password "secret"
        |> Sql.port 5432
        |> Sql.formatConnectionString

    let createGenre (payload: Genre): int =
        connectionString
        |> Sql.connect
        |> Sql.query "INSERT INTO genres (name, url) VALUES (@name, @url);"
        |> Sql.parameters [ ("name", Sql.string payload.Name); ("url", Sql.string payload.Url)]
        |> Sql.executeNonQuery

    let readGenre (id: int): Genre =
        connectionString
        |> Sql.connect
        |> Sql.query "SELECT * FROM genres WHERE id = @id;"
        |> Sql.parameters [ "id", Sql.int id ]
        |> Sql.executeRow (fun read ->
            {
                Id = read.int "id"
                Name = read.text "name"
                Url = read.text "url"
            })

    let readGenres: Genre list =
        connectionString
        |> Sql.connect
        |> Sql.query "SELECT * FROM genres;"
        |> Sql.execute (fun read ->
            {
                Id = read.int "id"
                Name = read.text "name"
                Url = read.text "url"
            })

    let updateGenre id =
        ()

    let deleteGenre (id: int): int =
        connectionString
        |> Sql.connect
        |> Sql.query "DELETE FROM genres WHERE id = @id;"
        |> Sql.parameters [ "id", Sql.int id ]
        |> Sql.executeNonQuery
