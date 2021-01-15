#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["CorreiosWs2WebAPICore/CorreiosWs2WebAPICore.csproj", "CorreiosWs2WebAPICore/"]
RUN dotnet restore "CorreiosWs2WebAPICore/CorreiosWs2WebAPICore.csproj"
COPY . .
WORKDIR "/src/CorreiosWs2WebAPICore"
RUN dotnet build "CorreiosWs2WebAPICore.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CorreiosWs2WebAPICore.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CorreiosWs2WebAPICore.dll"]