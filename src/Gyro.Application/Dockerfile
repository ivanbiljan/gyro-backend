FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Gyro.Application/Gyro.Application.csproj", "Gyro.Application/"]
RUN dotnet restore "Gyro.Application/Gyro.Application.csproj"
COPY . .
WORKDIR "/src/Gyro.Application"
RUN dotnet build "Gyro.Application.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Gyro.Application.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Gyro.Application.dll"]
