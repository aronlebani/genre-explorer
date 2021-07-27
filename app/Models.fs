namespace genre_explorer.Models

open System

[<CLIMutable>]
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
