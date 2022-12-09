FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR .
COPY ["Services/Rate/gRPC_Server/gRPC_Server.csproj", "Services/Rate/gRPC_Server/"]
RUN dotnet restore "Services/Rate/gRPC_Server/gRPC_Server.csproj"
COPY . .
WORKDIR "/Services/Rate/gRPC_Server"
RUN dotnet build "gRPC_Server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "gRPC_Server.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "gRPC_Server.dll"]