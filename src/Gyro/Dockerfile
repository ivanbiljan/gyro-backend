FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Gyro/Gyro.csproj", "Gyro/"]
RUN dotnet restore "Gyro/Gyro.csproj"
COPY . .
WORKDIR "/src/Gyro"
RUN dotnet build "Gyro.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Gyro.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Gyro.dll"]
