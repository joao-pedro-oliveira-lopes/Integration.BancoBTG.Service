FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Integration.BancoBTG.Service/Integration.BancoBTG.Service.csproj", "Integration.BancoBTG.Service/"]
RUN dotnet restore "Integration.BancoBTG.Service/Integration.BancoBTG.Service.csproj"
COPY . .
WORKDIR "/src/Integration.BancoBTG.Service"
RUN dotnet publish "Integration.BancoBTG.Service.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Integration.BancoBTG.Service.dll"]docker build -t btgapplication .
