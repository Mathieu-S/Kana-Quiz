#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["src/KanaQuiz.Web.Server/KanaQuiz.Web.Server.csproj", "src/KanaQuiz.Web.Server/"]
COPY ["src/KanaQuiz.Core/KanaQuiz.Core.csproj", "src/KanaQuiz.Core/"]
COPY ["src/KanaQuiz.Web.Client/KanaQuiz.Web.Client.csproj", "src/KanaQuiz.Web.Client/"]
RUN dotnet restore "src/KanaQuiz.Web.Server/KanaQuiz.Web.Server.csproj"
COPY . .
WORKDIR "/src/src/KanaQuiz.Web.Server"
RUN dotnet build "KanaQuiz.Web.Server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "KanaQuiz.Web.Server.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "KanaQuiz.Web.Server.dll"]