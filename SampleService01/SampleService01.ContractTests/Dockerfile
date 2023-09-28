#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

#FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
#WORKDIR /src
#COPY ["SampleService01.ContractTests/SampleService01.ContractTests.csproj", "SampleService01.ContractTests/"]
#COPY ["SampleService01/SampleService01.csproj", "SampleService01/"]
#RUN dotnet restore "SampleService01.ContractTests/SampleService01.ContractTests.csproj"
#COPY . .
#WORKDIR "/src/SampleService01.ContractTests"
#RUN dotnet build "SampleService01.ContractTests.csproj" -c Release -o /app/build
#
#FROM build AS publish
#RUN dotnet publish "SampleService01.ContractTests.csproj" -c Release -o /app/publish /p:UseAppHost=false
#
#FROM base AS final
#WORKDIR /app
#COPY --from=publish /app/publish .
#ENTRYPOINT ["dotnet", "SampleService01.ContractTests.dll"]