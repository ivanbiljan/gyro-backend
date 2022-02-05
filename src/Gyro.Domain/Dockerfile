FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Gyro.Domain/Gyro.Domain.csproj", "Gyro.Domain/"]
RUN dotnet restore "Gyro.Domain/Gyro.Domain.csproj"
COPY . .
WORKDIR "/src/Gyro.Domain"
RUN dotnet build "Gyro.Domain.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Gyro.Domain.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Gyro.Domain.dll"]
