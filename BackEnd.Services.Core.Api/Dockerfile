FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["BackEnd.Services.Core.Api/BackEnd.Services.Core.Api.csproj", "BackEnd.Services.Core.Api/"]
RUN dotnet restore "BackEnd.Services.Core.Api/BackEnd.Services.Core.Api.csproj"
COPY . .
WORKDIR "/src/BackEnd.Services.Core.Api"
RUN dotnet build "BackEnd.Core.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BackEnd.Services.Core.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BackEnd.Services.Core.Api.dll"]
