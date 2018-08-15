FROM microsoft/aspnetcore-build:2.0 
ENV ASPNETCORE_URLS="http://+:8000"
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY . ./
WORKDIR /app/FLS.ServerSide/FLSAPI
RUN dotnet restore

RUN dotnet publish -c Release -o out

ENTRYPOINT ["dotnet", "out/FLS.ServerSide.API.dll"]