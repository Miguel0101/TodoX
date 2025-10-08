# Build stage
FROM mcr.microsoft.com/dotnet/sdk:9.0-alpine AS build

# Copy project to container
COPY src/TodoX.API/TodoX.API.csproj src/TodoX.API/
COPY src/TodoX.Application/TodoX.Application.csproj src/TodoX.Application/
COPY src/TodoX.Domain/TodoX.Domain.csproj src/TodoX.Domain/
COPY src/TodoX.Infrastructure/TodoX.Infrastructure.csproj src/TodoX.Infrastructure/

RUN dotnet restore src/TodoX.API/TodoX.API.csproj

COPY . .

# Dependencies
RUN apk add --no-cache gcompat

# Build project
WORKDIR /src
RUN dotnet publish TodoX.API/TodoX.API.csproj -c Release --os linux -o /app

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:9.0-alpine

# Copy output from build
WORKDIR /app
COPY --from=build /app .

EXPOSE 6789

ENTRYPOINT [ "dotnet", "TodoX.API.dll" ]