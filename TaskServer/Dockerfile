FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["TaskServer/TaskServer.csproj", "TaskServer/"]
RUN dotnet restore "TaskServer/TaskServer.csproj"
COPY . .
WORKDIR "/src/TaskServer"
RUN dotnet build "TaskServer.csproj" -c Debug -o /app/build

FROM build AS publish
RUN dotnet publish "TaskServer.csproj" -c Debug -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_ENVIRONMENT=Development
ENTRYPOINT ["dotnet", "TaskServer.dll"]
