#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-nanoserver-1903 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-nanoserver-1903 AS build
WORKDIR /src
COPY ["App.Web/App.Web.csproj", "App.Web/"]
COPY ["App.Infrastructure/App.Infrastructure.csproj", "App.Infrastructure/"]
COPY ["App.Core/App.Core.csproj", "App.Core/"]
RUN dotnet restore "App.Web/App.Web.csproj"
COPY . .
WORKDIR "/src/App.Web"
RUN dotnet build "App.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "App.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "App.Web.dll"]