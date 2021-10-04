module Models

open System

type Genre = {
    Id: int;
    Name: string;
    Url: string;
    CreatedDate: DateTime;
    UpdatedDate: DateTime;
}
