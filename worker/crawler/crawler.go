package crawler

import (
	"log"
	"time"
	"strings"
    "regexp"
	"github.com/gocolly/colly"
	"github.com/PuerkitoBio/goquery"
)

type Genre struct {
	Name string
	Url string
	Parent *Genre
	Children []*Genre
}

func findDerivativeForms(genre *Genre, c *colly.Collector, domain string) {
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
			if !ok || !strings.Contains(href, "/wiki") {	// TODO - parameterise me!
				return	// Link has no url or incorrect url format
			}

			derivativeGenre := Genre{
				Name: s.Text(),
				Url: href,
				Parent: genre,
				Children: nil,
			}

			genre.Children = append(genre.Children, &derivativeGenre)
			findDerivativeForms(&derivativeGenre, c.Clone(), domain)
		})
	})

	c.OnRequest(func(r *colly.Request) {
		log.Println("Visiting", r.URL.String())
	})

	c.Visit("https://" + domain + genre.Url)
	c.Wait()
}

func Crawl(domain string, glob string, root *Genre) {
	c := colly.NewCollector(
		colly.AllowedDomains(domain),
		colly.URLFilters(regexp.MustCompile(glob)),
		colly.Async(true),
	)

	c.Limit(&colly.LimitRule{
		DomainGlob: glob,
		Delay: 1 * time.Second,
		RandomDelay: 1 * time.Second,
		Parallelism: 2,
	})

	findDerivativeForms(root, c.Clone(), domain)
}
