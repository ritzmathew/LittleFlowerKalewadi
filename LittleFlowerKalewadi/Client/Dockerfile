FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY ["Client/LittleFlowerKalewadi.Client.csproj", "Client/"]
RUN dotnet restore "Client/LittleFlowerKalewadi.Client.csproj"
COPY . .
WORKDIR "/src/Client"
RUN dotnet build "LittleFlowerKalewadi.Client.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "LittleFlowerKalewadi.Client.csproj" -c Release -o /app/publish

FROM nginx:alpine
WORKDIR /var/www/web
COPY --from=publish /app/publish/wwwroot .
COPY Client/nginx.conf /etc/nginx/nginx.conf
EXPOSE 80
EXPOSE 443
