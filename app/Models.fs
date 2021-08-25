namespace genre_explorer.Models

open System

type Genre = {
    Id: int;
    Name: string;
    Url: string;
    CreatedDate: DateTime;
    UpdatedDate: DateTime;
    // StylisticOrigins: List<Genre>;
    // DerivativeForms: List<Genre>;
    // SubGenres: List<Genre>;
    // FusionGenres: List<Genre>;
}

type Derivative = {
    GenreId: int;
    derivativeGenre: Genre;
}
