# Build Stage
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app

WORKDIR /app
COPY . ./
RUN dotnet publish "MatchingGame.csproj" -c Release -o output

# Nginx Stage
FROM nginx:1.23.0-alpine
WORKDIR /app
EXPOSE 80
COPY ./nginx.conf /etc/nginx/nginx.conf
COPY --from=build /app/output/wwwroot /usr/share/nginx/html