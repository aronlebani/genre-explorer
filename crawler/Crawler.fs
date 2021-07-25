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

    let isDerivativeForm (tr: HtmlNode): bool =
        match tr.CssSelect("th") |> Seq.tryHead with
        | Some th -> th.InnerText() = "Derivative forms"
        | None -> false

    let extractLink (a: HtmlNode): string * string =
        match a.TryGetAttribute("href") with
        | Some href -> href.Value(), a.InnerText()
        | None -> "", ""

    let isValidLink (url: string, name: string): bool =
        match url with
        | x when x.Contains("/wiki") -> true
        | _ -> false

    let rec crawl (url: string, name: string) =
        printfn "{ name: %s, url: %s }" name url

        let infoBox: HtmlNode =
            HtmlDocument.Load("https://en.wikipedia.org" + url).CssSelect(".infobox tbody")
            |> Seq.head

        let infoRows: seq<HtmlNode> = infoBox.Descendants("tr")

        let derivativeFormsRow: HtmlNode option =
            infoRows
            |> Seq.tryFind isDerivativeForm

        match derivativeFormsRow with
        | Some derivativeForm -> derivativeForm.Descendants("a") |> Seq.map extractLink |> Seq.filter isValidLink |> Seq.iter crawl
        | None -> ()

        ()
