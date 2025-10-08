FROM mcr.microsoft.com/dotnet/sdk:9.0-alpine AS build

COPY . .

# Setup migration
RUN dotnet tool install --global dotnet-ef
ENV PATH="/root/.dotnet/tools:$PATH"