package main

import (
	"log"
	"time"
	"regexp"
	"github.com/gocolly/colly"
	"github.com/PuerkitoBio/goquery"
)

func handleError(err error) {
	if err != nil {
		log.Fatalln(err)
	}
}

type Genre struct {
	Name string
	Url string
	Parent *Genre
	Children []*Genre
}

func FindDerivativeForms(genre Genre, c *colly.Collector) {
	c.OnHTML(".infobox tbody", func(e *colly.HTMLElement) {
		if e == nil {
			return	// Page has no infobox
		}

		var derivativeForms *goquery.Selection
		e.DOM.Children().Each(func(i int, s *goquery.Selection) {
			if s.Find("th").Text() == "Derivative forms" {
				derivativeForms = s
			}
		})

		if derivativeForms == nil {
			return	// Genre has no derivative genres
		}

		derivativeForms.Find("a[href]").Each(func(i int, s *goquery.Selection) {
			href, ok := s.Attr("href")
			if !ok {
				return	// Link has no url
			}

			derivativeGenre := Genre{
				Name: s.Text(),
				Url: href,
				Parent: &genre,
				Children: nil,
			}

			genre.Children = append(genre.Children, &derivativeGenre)
			FindDerivativeForms(derivativeGenre, c.Clone())
		})
	})

	c.OnRequest(func(r *colly.Request) {
		log.Println("Visiting", r.URL.String())
	})

	baseUrl := "https://en.wikipedia.org"
	c.Visit(baseUrl + genre.Url)
	c.Wait()
}

func main() {
	root := Genre{
		Name: "Post-punk",
		Url: "/wiki/Post-punk",
		Parent: nil,
		Children: nil,
	}

	c := colly.NewCollector(
		colly.AllowedDomains("en.wikipedia.org"),
		colly.URLFilters(regexp.MustCompile("en.wikipedia.org/wiki/*")),
		colly.Async(true),
	)

	c.Limit(&colly.LimitRule{
		DomainGlob: "en.wikipedia.org/wiki/*",
		Delay: 1 * time.Second,
		RandomDelay: 1 * time.Second,
		Parallelism: 2,
	})

	FindDerivativeForms(root, c.Clone())
}