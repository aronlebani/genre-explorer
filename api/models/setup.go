package models

import (
    "os"
    "fmt"
    "log"
    "gorm.io/gorm"
    "gorm.io/driver/postgres"
)

var Db *gorm.DB

func InitDatabase() {
    host := os.Getenv("DB_HOST")
    user := os.Getenv("DB_USER")
    password := os.Getenv("DB_PASSWORD")
    name := os.Getenv("DB_NAME")
    port := os.Getenv("DB_PORT")

    dsn := fmt.Sprintf("host=%s user=%s password=%s dbname=%s port=%s sslmode=disable", host, user, password, name, port)
    db, err := gorm.Open(postgres.Open(dsn), &gorm.Config{})

    if err != nil {
        log.Fatalln(err) 
    }

    db.AutoMigrate(&Genre{})

    Db = db
}
