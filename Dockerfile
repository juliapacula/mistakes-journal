FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /server

FROM node:lts-alpine AS clientBuild
WORKDIR /client-src
COPY client/package*.json ./
RUN npm install
COPY client .
RUN npm run build

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY ["server/Mistakes.Journal.Api/Mistakes.Journal.Api.csproj", "Mistakes.Journal.Api/"]
RUN dotnet restore "Mistakes.Journal.Api/Mistakes.Journal.Api.csproj"
COPY server .
WORKDIR "/src/Mistakes.Journal.Api"
RUN dotnet build "Mistakes.Journal.Api.csproj" -c Release -o /server/build

FROM build AS publish
RUN dotnet publish "Mistakes.Journal.Api.csproj" -c Release -o /server/publish

FROM base AS final
WORKDIR /server
COPY --from=publish /server/publish .
COPY --from=clientBuild /client-src/dist client
ENTRYPOINT ["dotnet", "Mistakes.Journal.Api.dll"]

