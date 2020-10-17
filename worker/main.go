package main

import (
	"log"
	"github.com/aronlebani/genre-explorer/crawler"
)

func main() {
	domain := "en.wikipedia.org"
	glob := "en.wikipedia.org/wiki/*"

    root:= crawler.Genre{
        Name: "Alternative rock",
        Url: "/wiki/Alternative_rock",
        Parent: nil,
        Children: nil,
    }

	crawler.Crawl(domain, glob, &root)
	for _, genre := range root.Children[2].Children {
		log.Println(genre.Name)
	}
}
