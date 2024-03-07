FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["ApiDate-2.0/ApiDate-2.0.csproj", "ApiDate-2.0/"]
RUN dotnet restore "ApiDate-2.0/ApiDate-2.0.csproj"
COPY . .
WORKDIR "/src/ApiDate-2.0"
RUN dotnet build "ApiDate-2.0.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "ApiDate-2.0.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ApiDate-2.0.dll"]
