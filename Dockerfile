FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env

WORKDIR /app

COPY ./src ./

# Build Api

WORKDIR /app/Tlis.Cms.ShowManagement/Api/src

RUN dotnet restore
RUN dotnet publish -c Release -o /out/api

# Build Cli

WORKDIR /app/Tlis.Cms.ShowManagement.Cli

RUN dotnet restore
RUN dotnet publish -c Release -o /out/cli

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0

# Copy Cli

WORKDIR /cli
COPY --from=build-env /out/cli .

# Copy Api

WORKDIR /app
COPY --from=build-env /out/api .

ENTRYPOINT [ "dotnet", "Tlis.Cms.ShowManagement.Api.dll" ]
