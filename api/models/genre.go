package models

import (
    "time"
)

type Genre struct {
    Id uint `json:"id" gorm:"primary_key"`
    Name string `json:"name"`
    Url string `json:"url"`
    CreatedAt time.Time
    UpdatedAt time.Time
}
