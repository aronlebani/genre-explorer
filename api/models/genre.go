package models

type Genre struct {
    Id uint `json:"id" gorm:"primary_key"`
    Name string `json:"name"`
    Url string `json:"url"`
}
