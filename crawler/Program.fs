open System
open Crawler

[<EntryPoint>]
let main argv =
    Crawler.crawlDerivativeForms("/wiki/Alternative_rock", "Alternative rock")
    0
