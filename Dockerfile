#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["SmartOne.Services.Core.Api/SmartOne.Services.Core.Api.csproj", "SmartOne.Services.Core.Api/"]
COPY ["SmartOne.Common.Core/SmartOne.Common.Core.csproj", "SmartOne.Common.Core/"]
COPY ["SmartOne.Common.AspNetCore/SmartOne.Common.AspNetCore.csproj", "SmartOne.Common.AspNetCore/"]
COPY ["SmartOne.Common.ThingsBoardRestApis/SmartOne.Common.ThingsBoardRestApis.csproj", "SmartOne.Common.ThingsBoardRestApis/"]
RUN dotnet restore "SmartOne.Services.Core.Api/SmartOne.Services.Core.Api.csproj"
COPY . .
WORKDIR "/src/SmartOne.Services.Core.Api"
RUN dotnet build "SmartOne.Services.Core.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SmartOne.Services.Core.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SmartOne.Services.Core.Api.dll"]
