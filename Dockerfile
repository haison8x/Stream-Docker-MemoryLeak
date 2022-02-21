FROM mcr.microsoft.com/dotnet/sdk:6.0 AS builder

WORKDIR /src

COPY ./StreamDockerTestApi/. ./StreamDockerTestApi

WORKDIR /src/StreamDockerTestApi
RUN dotnet restore -v diag

COPY ./StreamDockerTestApi .

RUN dotnet publish -r linux-x64 --self-contained false -c Release -o /app 

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base

WORKDIR /app
COPY --from=builder /app .

ENTRYPOINT ["dotnet", "StreamDockerTestApi.dll"]