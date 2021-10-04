module Repositories

open System
open Npgsql.FSharp
open Models

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

let createGenreOrigin (genreId: int) (originId: int): int =
    connectionString
    |> Sql.connect
    |> Sql.query "INSERT INTO genre_origin (genre_id, origin_id) VALUES (@genreId, @originId);"
    |> Sql.parameters [ ("genreId", Sql.int genreId); ("originId", Sql.int originId) ]
    |> Sql.executeNonQuery

let createOrigin (genreId: int) (originGenre: Genre): int =
    createGenre originGenre
    |> createGenreOrigin genreId

let createGenreSubgenre (genreId: int) (subgenreId: int): int =
    connectionString
    |> Sql.connect
    |> Sql.query "INSERT INTO genre_subgenre (genre_id, subgenre_id) VALUES (@genreId, @subgenreId);"
    |> Sql.parameters [ ("genreId", Sql.int genreId); ("subgenreId", Sql.int subgenreId) ]
    |> Sql.executeNonQuery

let createSubgenre (genreId: int) (subgenreGenre: Genre): int =
    createGenre subgenreGenre
    |> createGenreSubgenre genreId

let createGenreFusion (genreId: int) (fusionId: int): int =
    connectionString
    |> Sql.connect
    |> Sql.query "INSERT INTO genre_fusion (genre_id, fusion_id) VALUES (@genreId, @fusionId);"
    |> Sql.parameters [ ("genreId", Sql.int genreId); ("fusionId", Sql.int fusionId) ]
    |> Sql.executeNonQuery

let createFusion (genreId: int) (fusionGenre: Genre): int =
    createGenre fusionGenre
    |> createGenreOrigin genreId

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

let readGenreOrigins (id: int): Genre list =
    connectionString
    |> Sql.connect
    |> Sql.query "SELECT g.genre_id AS genre_id, g.name AS name, g.url AS url, g.created_date AS created_date, g.updated_date AS updated_date
                  FROM genre_origin go
                  JOIN genres g ON (go.origin_id=g.genre_id)
                  WHERE go.genre_id = @id;"
    |> Sql.parameters [ "id", Sql.int id ]
    |> Sql.execute (fun read ->
        {
            Id = read.int "genre_id"
            Name = read.text "name"
            Url = read.text "url"
            CreatedDate = read.dateTime "created_date"
            UpdatedDate = read.dateTime "updated_date"
        })

let readGenreSubgenres (id: int): Genre list =
    connectionString
    |> Sql.connect
    |> Sql.query "SELECT g.genre_id AS genre_id, g.name AS name, g.url AS url, g.created_date AS created_date, g.updated_date AS updated_date
                  FROM genre_subgenre gs
                  JOIN genres g ON (gs.origin_id=g.genre_id)
                  WHERE gs.genre_id = @id;"
    |> Sql.parameters [ "id", Sql.int id ]
    |> Sql.execute (fun read ->
        {
            Id = read.int "genre_id"
            Name = read.text "name"
            Url = read.text "url"
            CreatedDate = read.dateTime "created_date"
            UpdatedDate = read.dateTime "updated_date"
        })

let readGenreFusions (id: int): Genre list =
    connectionString
    |> Sql.connect
    |> Sql.query "SELECT g.genre_id AS genre_id, g.name AS name, g.url AS url, g.created_date AS created_date, g.updated_date AS updated_date
                  FROM genre_fusion gf
                  JOIN genres g ON (gf.origin_id=g.genre_id)
                  WHERE gf.genre_id = @id;"
    |> Sql.parameters [ "id", Sql.int id ]
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
