FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["Services/FidelizeDotz.Services.Api/FidelizeDotz.Services.Api.csproj", "Services/FidelizeDotz.Services.Api/"]
RUN dotnet restore "Services/FidelizeDotz.Services.Api/FidelizeDotz.Services.Api.csproj"
COPY . .
WORKDIR "/src/Services/FidelizeDotz.Services.Api"
RUN dotnet build "FidelizeDotz.Services.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FidelizeDotz.Services.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FidelizeDotz.Services.Api.dll"]