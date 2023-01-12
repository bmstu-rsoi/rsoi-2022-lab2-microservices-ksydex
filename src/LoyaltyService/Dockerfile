FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["LoyaltyService/LoyaltyService.csproj", "LoyaltyService/"]
RUN dotnet restore "LoyaltyService/LoyaltyService.csproj"
COPY . .
WORKDIR "/src/LoyaltyService"
RUN dotnet build "LoyaltyService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "LoyaltyService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LoyaltyService.dll"]
