FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Services/OrdersWorkerService/OrdersWorkerService.csproj", "Services/OrdersWorkerService/"]
RUN dotnet restore "Services/OrdersWorkerService/OrdersWorkerService.csproj"
COPY . .
WORKDIR "/src/Services/OrdersWorkerService"
RUN dotnet build "OrdersWorkerService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "OrdersWorkerService.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OrdersWorkerService.dll"]
