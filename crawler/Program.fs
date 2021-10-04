open System
open Crawler

let initialLink: Link = {
    Url = "/wiki/Alternative_rock"
    Name = "Alternative rock"
}

[<EntryPoint>]
let main argv =
    crawlDerivativeForms initialLink
    0
