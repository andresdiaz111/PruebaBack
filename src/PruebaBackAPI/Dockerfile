FROM mcr.microsoft.com/dotnet/sdk:6.0-focal AS build
WORKDIR /source
COPY . .
RUN dotnet restore "./src/PruebaBackAPI/PruebaBackAPI.csproj" --disable-parallel
RUN dotnet publish "./src/PruebaBackAPI/PruebaBackAPI.csproj" -c release -o /app --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:6.0-focal
WORKDIR /app
COPY --from=build /app ./

EXPOSE 7300

ENTRYPOINT [ "dotnet", "PruebaBackAPI.dll" ]
