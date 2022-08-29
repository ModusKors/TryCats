#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["TryCatsGrpcService/TryCatsGrpcService.csproj", "TryCatsGrpcService/"]
COPY ["TryCats6/TryCats6.csproj", "TryCats6/"]
COPY ["BusinessLogicLayer/BusinessLogicLayer.csproj", "BusinessLogicLayer/"]
COPY ["DataAccessLayer/DataAccessLayer.csproj", "DataAccessLayer/"]
RUN dotnet restore "TryCatsGrpcService/TryCatsGrpcService.csproj"
COPY . .
WORKDIR "/src/TryCatsGrpcService"
RUN dotnet build "TryCatsGrpcService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TryCatsGrpcService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TryCatsGrpcService.dll"]