#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM node:12.19-buster AS assets
WORKDIR /assets
COPY ["src/KanaQuiz.Web.Client/Assets", "src"]
WORKDIR /assets/src
RUN npm i
RUN npm run build

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["src/KanaQuiz.Web.Server/", "KanaQuiz.Web.Server/"]
COPY ["src/KanaQuiz.Web.Client/", "KanaQuiz.Web.Client/"]
COPY ["src/KanaQuiz.Core/", "KanaQuiz.Core/"]
COPY --from=assets /assets/wwwroot KanaQuiz.Web.Client/wwwroot
RUN dotnet restore "KanaQuiz.Web.Server/KanaQuiz.Web.Server.csproj"
WORKDIR /src/KanaQuiz.Web.Server
RUN dotnet build "KanaQuiz.Web.Server.csproj" -c Release -o /app/build

FROM build AS publish
WORKDIR /src/KanaQuiz.Web.Server
RUN dotnet publish "KanaQuiz.Web.Server.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "KanaQuiz.Web.Server.dll"]