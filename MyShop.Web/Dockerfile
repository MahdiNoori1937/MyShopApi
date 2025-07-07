FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["MyShop.Web/MyShop.Web.csproj", "MyShop.Web/"]
COPY ["MyShop.Application/MyShop.Application.csproj", "MyShop.Application/"]
COPY ["MyShop.Data/MyShop.Data.csproj", "MyShop.Data/"]
COPY ["MyShop.IOC/MyShop.IOC.csproj", "MyShop.IOC/"]
COPY ["MyShop.Domain/MyShop.Domain.csproj", "MyShop.Domain/"]
COPY ["MyShop.Test/MyShop.Test.csproj", "MyShop.Test/"]
RUN dotnet restore "MyShop.Web/MyShop.Web.csproj"
COPY . .
WORKDIR "/src/MyShop.Web"
RUN dotnet build "MyShop.Web.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "MyShop.Web.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MyShop.Web.dll"]
