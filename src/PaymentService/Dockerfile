FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

COPY ["src/PaymentService/PaymentService.csproj", "PaymentService/"]
COPY ["src/SharedKernel/SharedKernel.csproj", "SharedKernel/"]


RUN dotnet restore "/src/PaymentService/PaymentService.csproj"
COPY /src .
WORKDIR "/src/PaymentService"
RUN dotnet build -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PaymentService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PaymentService.dll"]
