#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY DeepLearning.Server/DeepLearning.Server.csproj DeepLearning.Server/
COPY DeepLearning.Predication/DeepLearning.Predication.csproj DeepLearning.Predication/
COPY DeepLearning.Shared/DeepLearning.Shared.csproj DeepLearning.Shared/
RUN dotnet restore "DeepLearning.Server/DeepLearning.Server.csproj"
COPY . .
WORKDIR "/src/DeepLearning.Server"
RUN dotnet build "DeepLearning.Server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DeepLearning.Server.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DeepLearning.Server.dll"]
