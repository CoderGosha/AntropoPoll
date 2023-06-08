FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /app

# copy csproj and restore as distinct layers
COPY *.sln .
COPY AntropoPollWebApi/*.csproj ./AntropoPollWebApi/
COPY AntropoPollWorker/*.csproj ./AntropoPollWorker/
COPY AntropoPollWebApi.Core/*.csproj ./AntropoPollWebApi.Core/

RUN dotnet restore

# copy everything else and build app
COPY AntropoPollWebApi/. ./AntropoPollWebApi/
COPY AntropoPollWorker/. ./AntropoPollWorker/
COPY AntropoPollWebApi.Core/. ./AntropoPollWebApi.Core/

WORKDIR /app/AntropoPollWebApi
RUN dotnet publish -c Release -o out /p:Version=1.0.0.9-ab


FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS runtime
WORKDIR /app
COPY --from=build /app/AntropoPollWebApi/out ./
ENTRYPOINT ["dotnet", "AntropoPollWebApi.dll"]
