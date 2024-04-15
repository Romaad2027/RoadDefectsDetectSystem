FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

COPY Data/accelerometer.csv .
COPY Data/gps.csv .

EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Agent.csproj", "./"]
RUN dotnet restore "Agent.csproj"
COPY . .

RUN dotnet build "Agent.csproj" -c Debug -o /app/build

FROM build AS publish
RUN dotnet publish "Agent.csproj" -c Debug -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Agent.dll"]
