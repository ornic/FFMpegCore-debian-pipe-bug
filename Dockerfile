FROM debian:12 AS base
WORKDIR /app

# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["iOS.audio.conversion.test.csproj", "."]
RUN dotnet restore "./iOS.audio.conversion.test.csproj"
COPY . .
WORKDIR "/src"
RUN dotnet build "./iOS.audio.conversion.test.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./iOS.audio.conversion.test.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final

RUN apt update -y
RUN apt upgrade -y

RUN apt install wget -y

RUN wget https://packages.microsoft.com/config/debian/12/packages-microsoft-prod.deb -O packages-microsoft-prod.deb
RUN dpkg -i packages-microsoft-prod.deb

RUN apt update -y

RUN apt install ffmpeg -y
RUN apt install dotnet-runtime-8.0 -y

WORKDIR /app
COPY --from=publish /app/publish .
COPY BAD.mp4 .

ENTRYPOINT ["dotnet", "iOS.audio.conversion.test.dll"]