#!/bin/bash
dotnet user-secrets set client_secret 881cd158-adcd-4499-9366-93b55209cfa7 --project ./src/PruebaBackAPI/PruebaBackAPI.csproj
dotnet user-secrets set client_id 0640000b-ec0b-4207-aab3-61e0481c405d --project ./src/PruebaBackAPI/PruebaBackAPI.csproj
dotnet user-secrets set UserID dbuser --project ./src/PruebaBackAPI/PruebaBackAPI.csproj
dotnet user-secrets set Password Inicio.123 --project ./src/PruebaBackAPI/PruebaBackAPI.csproj
dotnet restore "./src/PruebaBackAPI/PruebaBackAPI.csproj"
dotnet build "./src/PruebaBackAPI/PruebaBackAPI.csproj"
dotnet-ef database update --project ./src/PruebaBackAPI/PruebaBackAPI.csproj
