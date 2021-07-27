CREATE TABLE [IF NOT EXISTS] genres (
    id serial PRIMARY KEY,
    name VARCHAR(100),
    url VARCHAR(100),
    created_date TIMESTAMP,
    updated_date TIMESTAMP,
);