#Stage 1: Build Stage (Build the application)
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["ApiAggregator/ApiAggregator.csproj", "ApiAggregator/"]
RUN dotnet restore "ApiAggregator/ApiAggregator.csproj"
COPY . .
WORKDIR "/src/ApiAggregator"
RUN dotnet build "ApiAggregator.csproj" -c Release -o /app/build

#Stage 2: Publish Stage (Publish the application)
FROM build AS publish
RUN dotnet publish "ApiAggregator.csproj" -c Release -o /app/publish

#Stage 3: Run Stage (Run the application)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
ENV ASPNETCORE_ENVIRONMENT=Development
ENV ASPNETCORE_URLS=http://*:7249
EXPOSE 7001
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ApiAggregator.dll"]
