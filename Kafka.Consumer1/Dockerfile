FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Kafka.Consumer1/Kafka.Consumer1.csproj", "Kafka.Consumer1/"]
COPY ["Kafka.Common/Kafka.Common.csproj", "Kafka.Common/"]
RUN dotnet restore "Kafka.Consumer1/Kafka.Consumer1.csproj"
COPY . .
WORKDIR "/src/Kafka.Consumer1"
RUN dotnet build "Kafka.Consumer1.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Kafka.Consumer1.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Kafka.Consumer1.dll"]