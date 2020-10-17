package main

import (
    "os"
    "log"
    "net/http"
    "github.com/gorilla/mux"
    "github.com/joho/godotenv"
    "api/models"
    "api/controllers"
)

func handleError(err error) {
    if err != nil {
        log.Fatalln(err)
    }
}

func main() {
    // Load environment variables
    err := godotenv.Load(".env")
    handleError(err)

    // Initialise database
    models.InitDatabase()

    // Set up router
    r := mux.NewRouter()
    r.HandleFunc("/", controllers.HealthCheck)
    
    r.HandleFunc("/genre", controllers.ReadGenres).Methods("GET")
    r.HandleFunc("/genre", controllers.CreateGenre).Methods("POST")
    
    r.HandleFunc("/genre/{id}", controllers.ReadGenre).Methods("GET")
    r.HandleFunc("/genre/{id}", controllers.UpdateGenre).Methods("PUT")
    r.HandleFunc("/genre/{id}", controllers.DeleteGenre).Methods("DELETE")

    // Run
    http.ListenAndServe(":" + os.Getenv("PORT"), r)
}
