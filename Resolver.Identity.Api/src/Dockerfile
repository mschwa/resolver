ARG ASPNETCORE_ENVIRONMENT

FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["Resolver.Identity.Api.csproj", "./"]
RUN dotnet restore "./Resolver.Identity.Api.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "Resolver.Identity.Api.csproj" -c #{environment}# -o /app

FROM build AS publish
RUN dotnet publish "Resolver.Identity.Api.csproj" -c #{environment}# -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Resolver.Identity.Api.dll"]

ENV ASPNETCORE_ENVIRONMENT=#{environment}#

