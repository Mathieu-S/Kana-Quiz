#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM node:14.15-alpine AS assets
WORKDIR /assets
COPY ["src/KanaQuiz.Web.Client/Assets", "src"]
WORKDIR /assets/src
RUN npm i
RUN npm run build

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-alpine AS build
WORKDIR /src
COPY ["src/KanaQuiz.Web.Server/", "KanaQuiz.Web.Server/"]
COPY ["src/KanaQuiz.Web.Client/", "KanaQuiz.Web.Client/"]
COPY ["src/KanaQuiz.Core/", "KanaQuiz.Core/"]
COPY ["src/KanaQuiz.Infrastructure/", "KanaQuiz.Infrastructure/"]
COPY --from=assets /assets/wwwroot KanaQuiz.Web.Client/wwwroot
RUN dotnet restore "KanaQuiz.Web.Server/KanaQuiz.Web.Server.csproj"
WORKDIR /src/KanaQuiz.Web.Server
RUN dotnet publish "KanaQuiz.Web.Server.csproj" -c Release -o /app/build

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-alpine AS final
WORKDIR /app
EXPOSE 80
EXPOSE 443
COPY --from=build /app/build .
ENTRYPOINT ["dotnet", "KanaQuiz.Web.Server.dll"]