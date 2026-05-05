FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY ["eCommerceProducts.Api/eCommerceProducts.Api.csproj", "eCommerceProducts.Api/"]
COPY ["BusinessLogicLayer/BusinessLogicLayer.csproj", "BusinessLogicLayer/"]
COPY ["DataAccessLayer/DataAccessLayer.csproj", "DataAccessLayer/"]

RUN dotnet restore "eCommerceProducts.Api/eCommerceProducts.Api.csproj"

COPY . .

WORKDIR "/src/eCommerceProducts.Api"
RUN dotnet build "eCommerceProducts.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "eCommerceProducts.Api.csproj" -c Release -o /app/publish --no-restore

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "eCommerceProducts.Api.dll"]
