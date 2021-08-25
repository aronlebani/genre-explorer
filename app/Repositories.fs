namespace genre_explorer

module Repositories =

    open System
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
        |> Sql.query "INSERT INTO genres (name, url, created_date, updated_date) VALUES (@name, @url, @createdDate, @updatedDate) RETURNING genre_id;"
        |> Sql.parameters [ ("name", Sql.string payload.Name); ("url", Sql.string payload.Url); ("createdDate", Sql.timestamp DateTime.UtcNow); ("updatedDate", Sql.timestamp DateTime.UtcNow)]
        |> Sql.executeRow (fun read -> read.int "genre_id")

    let createGenreDerivative (genreId: int) (derivativeId: int): int =
        connectionString
        |> Sql.connect
        |> Sql.query "INSERT INTO genre_derivative (genre_id, derivative_id) VALUES (@genreId, @derivativeId);"
        |> Sql.parameters [ ("genreId", Sql.int genreId); ("derivativeId", Sql.int derivativeId) ]
        |> Sql.executeNonQuery

    let createDerivative (genreId: int) (derivativeGenre: Genre): int =
        createGenre derivativeGenre
        |> createGenreDerivative genreId

    let readGenre (id: int): Genre =
        connectionString
        |> Sql.connect
        |> Sql.query "SELECT * FROM genres g WHERE g.genre_id = @id;"
        |> Sql.parameters [ "id", Sql.int id ]
        |> Sql.executeRow (fun read ->
            {
                Id = read.int "genre_id"
                Name = read.text "name"
                Url = read.text "url"
                CreatedDate = read.dateTime "created_date"
                UpdatedDate = read.dateTime "updated_date"
            })

    let readGenreDerivatives (id: int): Genre list =
        connectionString
        |> Sql.connect
        |> Sql.query "SELECT g.genre_id AS genre_id, g.name AS name, g.url AS url, g.created_date AS created_date, g.updated_date AS updated_date
                      FROM genre_derivative gd
                      JOIN genres g ON (gd.derivative_id=g.genre_id)
                      WHERE gd.genre_id = @id;"
        |> Sql.parameters [ "id", Sql.int id ]
        |> Sql.execute (fun read ->
            {
                Id = read.int "genre_id"
                Name = read.text "name"
                Url = read.text "url"
                CreatedDate = read.dateTime "created_date"
                UpdatedDate = read.dateTime "updated_date"
            })

    let readGenres: Genre list =
        connectionString
        |> Sql.connect
        |> Sql.query "SELECT * FROM genres;"
        |> Sql.execute (fun read ->
            {
                Id = read.int "genre_id"
                Name = read.text "name"
                Url = read.text "url"
                CreatedDate = read.dateTime "created_date"
                UpdatedDate = read.dateTime "updated_date"
            })

    let updateGenre id =
        ()

    let deleteGenre (id: int): int =
        connectionString
        |> Sql.connect
        |> Sql.query "DELETE FROM genres WHERE genre_id = @id;"
        |> Sql.parameters [ "id", Sql.int id ]
        |> Sql.executeNonQuery
