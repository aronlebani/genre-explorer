open System
open Crawler

let initialLink: Crawler.Link = {
    Url = "/wiki/Alternative_rock"
    Name = "Alternative rock"
}

[<EntryPoint>]
let main argv =
    Crawler.crawlDerivativeForms initialLink
    0
