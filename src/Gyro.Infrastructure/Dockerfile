FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Gyro.Infrastructure/Gyro.Infrastructure.csproj", "Gyro.Infrastructure/"]
RUN dotnet restore "Gyro.Infrastructure/Gyro.Infrastructure.csproj"
COPY . .
WORKDIR "/src/Gyro.Infrastructure"
RUN dotnet build "Gyro.Infrastructure.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Gyro.Infrastructure.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Gyro.Infrastructure.dll"]
