package main

import (
	"log"
	"github.com/aronlebani/genre-explorer/crawler"
)

func main() {
	domain := "en.wikipedia.org"
	slug := "/wiki/Alternative_rock"
	name := "Alternative rock"
	glob := "en.wikipedia.org/wiki/*"

	root := crawler.Crawl(domain, slug, name, glob)	// TODO - pass in root node instead of individual params
	for _, genre := range root.Children[2].Children {
		log.Println(genre.Name)
	}
}