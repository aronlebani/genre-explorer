module Crawler

open FSharp.Data
open Genre

type Link = {
    Url: string;
    Name: string;
}

let genreFromLink (link: Link): Genre =
    {
        Name =  link.Name
        Url = link.Url
    } 

let is (category: string) (tr: HtmlNode): bool =
    match tr.CssSelect("th") |> Seq.tryHead with
    | Some th -> th.InnerText() = category
    | None -> false

let isDerivativeForm = is "Derivative forms"

let isStylisticOrigin = is "Stylistic origins"

let isSubgenre = is "Subgenres"

let isFusion = is "Fusion genres"

let extractLink (a: HtmlNode): Link =
    match a.TryGetAttribute("href") with
    | None -> { Url = ""; Name = "" }
    | Some href -> { Url = href.Value(); Name = a.InnerText() }

let isValidLink (link: Link): bool =
    match link.Url with
    | x when x.Contains("#") -> false
    | x when x.Contains("/wiki") -> true
    | _ -> false

let rec crawl (filter: HtmlNode -> bool) (link: Link) =
    printfn "{ name: %s, url: %s }" link.Name link.Url

    let genre: Genre = genreFromLink link
    Api.postGenre genre

    match HtmlDocument.Load("https://en.wikipedia.org" + link.Url).CssSelect(".infobox tbody") |> Seq.tryHead with
    | None -> ()
    | Some infobox ->
        match infobox.CssSelect("tr") |> Seq.tryFind filter with
        | None -> ()
        | Some derivativeForm ->
            derivativeForm.Descendants("a")
            |> Seq.map extractLink
            |> Seq.filter isValidLink
            |> Seq.iter (crawl filter)

let crawlDerivativeForms = crawl isDerivativeForm

let crawlStylisticOrigins = crawl isStylisticOrigin
