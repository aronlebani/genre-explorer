namespace Crawler

module Crawler =

    open FSharp.Data

    type Genre = {
        Name: string;
        Url: string;
        StylisticOrigins: List<Genre>;
        DerivativeForms: List<Genre>;
        SubGenres: List<Genre>;
        FusionGenres: List<Genre>;
    }

    let is (category: string) (tr: HtmlNode): bool =
        match tr.CssSelect("th") |> Seq.tryHead with
        | Some th -> th.InnerText() = category
        | None -> false

    let isDerivativeForm = is "Derivative forms"

    let isStylisticOrigin = is "Stylistic origins"

    let isSubgenre = is "Subgenres"

    let isFusion = is "Fusion genres"

    let extractLink (a: HtmlNode): string * string =
        match a.TryGetAttribute("href") with
        | Some href -> href.Value(), a.InnerText()
        | None -> "", ""

    let isValidLink (url: string, name: string): bool =
        match url with
        | x when x.Contains("#") -> false
        | x when x.Contains("/wiki") -> true
        | _ -> false

    let rec crawl (filter: HtmlNode -> bool) (url: string, name: string) =
        printfn "{ name: %s, url: %s }" name url

        match HtmlDocument.Load("https://en.wikipedia.org" + url).CssSelect(".infobox tbody") |> Seq.tryHead with
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
