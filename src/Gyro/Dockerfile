FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
ENV ASPNETCORE_URLS=https://+:5001;http://+:5000
WORKDIR /app
EXPOSE 5000
EXPOSE 5001

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Gyro/Gyro.csproj", "src/Gyro/"]
COPY ["src/Gyro.Core/Gyro.Core.csproj", "src/Gyro.Core/"]
COPY ["src/Gyro.Infrastructure/Gyro.Infrastructure.csproj", "src/Gyro.Infrastructure/"]
COPY ["tests/Gyro.Core.Tests/Gyro.Core.Tests.csproj", "tests/Gyro.Core.Tests/"]
RUN dotnet restore "src/Gyro/Gyro.csproj"
COPY . .
RUN dotnet test
WORKDIR "/src/src/Gyro"
RUN ls
RUN dotnet build "Gyro.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Gyro.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Gyro.dll"]