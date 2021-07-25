namespace genre_explorer.Models

[<CLIMutable>]
type Message = {
    Text : string
}

[<CLIMutable>]
type Genre = {
    Name: string;
    Url: string;
    StylisticOrigins: List<Genre>;
    DerivativeForms: List<Genre>;
    SubGenres: List<Genre>;
    FusionGenres: List<Genre>;
}