FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env

# Build Api

WORKDIR /app

# Copy everything else and build
COPY ./src/Tlis.Cms.ShowManagement/ ./

WORKDIR /app/Api/src

RUN dotnet restore
RUN dotnet publish -c Release -o /app/out

# Build Cli

WORKDIR /cli

COPY ./src/Tlis.Cms.ShowManagement.Cli/ ./

RUN dotnet restore
RUN dotnet publish -c Release -o /cli/out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0

# Copy Api

WORKDIR /app
COPY --from=build-env /app/out .

# Copy Cli

WORKDIR /cli
COPY --from=build-env /cli/out .

ENTRYPOINT [ "dotnet", "Tlis.Cms.ShowManagement.Api.dll" ]
