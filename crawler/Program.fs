// Learn more about F# at http://docs.microsoft.com/dotnet/fsharp

open System
open Crawler

// Define a function to construct a message to print
let from whom =
    sprintf "from %s" whom

[<EntryPoint>]
let main argv =
    let message = from "F#"
    printfn "Hello world %s" message
    Crawler.crawl("/wiki/Blues", "Blues")
    0