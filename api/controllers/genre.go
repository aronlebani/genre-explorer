package controllers

import (
    "log"
    "net/http"
    "api/models" 
    "encoding/json"
    "github.com/gorilla/mux"
)

func HealthCheck(w http.ResponseWriter, r *http.Request) {
    log.Println("OK")
}

func ReadGenres(w http.ResponseWriter, r *http.Request) {
    var genres []models.Genre
    models.Db.Find(&genres)

    w.Header().Set("Content-Type", "application/json")
    json.NewEncoder(w).Encode(genres)
}

func ReadGenre(w http.ResponseWriter, r *http.Request) {
    id := mux.Vars(r)["id"]
    var genre models.Genre
    models.Db.First(&genre, id)

    w.Header().Set("Content-Type", "application/json")
    json.NewEncoder(w).Encode(genre)
}

func CreateGenre(w http.ResponseWriter, r *http.Request) {
    var genre models.Genre
    json.NewDecoder(r.Body).Decode(&genre)
    models.Db.Create(&genre)

    w.Header().Set("Content-Type", "application/json")
    json.NewEncoder(w).Encode(genre)
}

func UpdateGenre(w http.ResponseWriter, r *http.Request) {
    id := mux.Vars(r)["id"]
    var genre models.Genre
    var newGenre models.Genre
    json.NewDecoder(r.Body).Decode(&newGenre) 
    models.Db.First(&genre, id).Updates(newGenre)

    w.Header().Set("Content-Type", "application/json")
    json.NewEncoder(w).Encode(genre)
}

func DeleteGenre(w http.ResponseWriter, r *http.Request) {
    id := mux.Vars(r)["id"]
    models.Db.Delete(&models.Genre{}, id)
}
