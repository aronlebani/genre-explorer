# syntax=docker/dockerfile:1
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build-env
WORKDIR /var/app

# Copy csproj and restore as distinct layers
COPY ./app/genre-explorer.fsproj ./
RUN dotnet restore

# Copy everything else and build
COPY ./app ./
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /var/app/out
COPY --from=build-env /var/app/out .
ENTRYPOINT ["./genre-explorer.App"]
