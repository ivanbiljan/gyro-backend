FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Gyro.HangfireWorker/Gyro.HangfireWorker.csproj", "Gyro.HangfireWorker/"]
RUN dotnet restore "src/Gyro.HangfireWorker/Gyro.HangfireWorker.csproj"
COPY . .
WORKDIR "/src/Gyro.HangfireWorker"
RUN dotnet build "Gyro.HangfireWorker.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Gyro.HangfireWorker.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Gyro.HangfireWorker.dll"]
