DROP TABLE IF EXISTS genres;
DROP TABLE IF EXISTS genre_derivative;
DROP TABLE IF EXISTS genre_origin;
DROP TABLE IF EXISTS genre_subgenre;
DROP TABLE IF EXISTS genre_fusion;

CREATE TABLE genres (
    genre_id SERIAL PRIMARY KEY,
    name VARCHAR(255),
    url VARCHAR(255),
    created_date TIMESTAMP,
    updated_date TIMESTAMP
);

CREATE TABLE genre_derivative (
    genre_id INT REFERENCES genres (genre_id) ON UPDATE CASCADE,
    derivative_id INT REFERENCES genres (genre_id) ON UPDATE CASCADE,
    CONSTRAINT genre_derivative_pkey PRIMARY KEY (genre_id, derivative_id)
);

CREATE TABLE genre_origin (
    genre_id INT REFERENCES genres (genre_id) ON UPDATE CASCADE,
    origin_id INT REFERENCES genres (genre_id) ON UPDATE CASCADE,
    CONSTRAINT genre_origin_pkey PRIMARY KEY (genre_id, origin_id)
);

CREATE TABLE genre_subgenre (
    genre_id INT REFERENCES genres (genre_id) ON UPDATE CASCADE,
    subgenre_id INT REFERENCES genres (genre_id) ON UPDATE CASCADE,
    CONSTRAINT genre_subgenre_pkey PRIMARY KEY (genre_id, subgenre_id)
);

CREATE TABLE genre_fusion (
    genre_id INT REFERENCES genres (genre_id) ON UPDATE CASCADE,
    fusion_id INT REFERENCES genres (genre_id) ON UPDATE CASCADE,
    CONSTRAINT genre_fusion_pkey PRIMARY KEY (genre_id, fusion_id)
);
