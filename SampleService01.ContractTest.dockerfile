#######################################################################################
### Explanation of Microsoft '-alpine' Docker Containers: 
### https://hub.docker.com/_/microsoft-dotnet-runtime/
#######################################################################################
FROM mcr.microsoft.com/dotnet/sdk:7.0-alpine AS source

COPY ./ /app/source/

WORKDIR /app/source/

FROM source AS release

######################################################################################
###
### Run Contract Tests. 
######################################################################################

LABEL contracttest=true  

ENTRYPOINT [  \
    "dotnet", \
    "test",   \
    "SampleService01.ContractTests.csproj" \
]
