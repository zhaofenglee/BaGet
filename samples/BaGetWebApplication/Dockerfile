FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["samples/BaGetWebApplication/BaGetWebApplication.csproj", "samples/BaGetWebApplication/"]
COPY ["src/BaGet.Database.Sqlite/BaGet.Database.Sqlite.csproj", "src/BaGet.Database.Sqlite/"]
COPY ["src/BaGet.Core/BaGet.Core.csproj", "src/BaGet.Core/"]
COPY ["src/BaGet.Protocol/BaGet.Protocol.csproj", "src/BaGet.Protocol/"]
COPY ["src/BaGet.Web/BaGet.Web.csproj", "src/BaGet.Web/"]
RUN dotnet restore "samples/BaGetWebApplication/BaGetWebApplication.csproj"
COPY . .
WORKDIR "/src/samples/BaGetWebApplication"
RUN dotnet build "BaGetWebApplication.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "BaGetWebApplication.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BaGetWebApplication.dll"]
