FROM mcr.microsoft.com/dotnet/sdk:6.0 as build

#Create and Set the working Directory 
WORKDIR /src

COPY . .

RUN dotnet publish -c Release -o /published ./server.sln

FROM mcr.microsoft.com/dotnet/aspnet:6.0 as runtime

# Uncomment the line below if running with HTTPS
ENV ASPNETCORE_URLS=https://+:443

WORKDIR /app

COPY --from=build /published .

ENTRYPOINT [ "dotnet", "server.dll" ]

EXPOSE 80
