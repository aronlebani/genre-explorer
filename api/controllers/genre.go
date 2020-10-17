package controllers

import (
    "log"
    "net/http"
    "api/models" 
    "encoding/json"
)

func HealthCheck(w http.ResponseWriter, r *http.Request) {
    log.Println("OK")
}

func GetGenres(w http.ResponseWriter, r *http.Request) {
    var genres []models.Genre
    models.Db.Find(&genres)

    w.Header().Set("Content-Type", "application/json")
    json.NewEncoder(w).Encode(genres)
}
