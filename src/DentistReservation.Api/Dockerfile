FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/DentistReservation.Api/DentistReservation.Api.csproj", "src/DentistReservation.Api/"]
COPY ["src/DentistReservation.Application/DentistReservation.Application.csproj", "src/DentistReservation.Application/"]
COPY ["src/DentistReservation.Domain/DentistReservation.Domain.csproj", "src/DentistReservation.Domain/"]
COPY ["src/DentistReservation.Infrastructure/DentistReservation.Infrastructure.csproj", "src/DentistReservation.Infrastructure/"]
RUN dotnet restore "src/DentistReservation.Api/DentistReservation.Api.csproj"
COPY . .
WORKDIR "/src/src/DentistReservation.Api"
RUN dotnet build "DentistReservation.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "DentistReservation.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DentistReservation.Api.dll"]
