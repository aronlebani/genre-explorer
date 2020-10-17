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
    r.HandleFunc("/genre", controllers.GetGenres)

    // Run
    http.ListenAndServe(":" + os.Getenv("PORT"), r)
}
