#######################################################################################
### Explanation of Microsoft '-alpine' Docker Containers: 
### https://hub.docker.com/_/microsoft-dotnet-runtime/
#######################################################################################
FROM mcr.microsoft.com/dotnet/sdk:7.0-alpine AS source
#ARG PAT

COPY ./ /app/source/


WORKDIR /app/source/

######################################################################################
###
### Publish and run Unit Tests. 
######################################################################################
FROM source AS release

LABEL unittest=true

RUN dotnet test  \ 
    --configuration release \
    --results-directory /testresults \
    --verbosity detailed \
    --logger "trx;LogFileName=unittest_results.trx" \
    --logger "console;verbosity=diagnostic" \
    --collect:"XPlat Code Coverage" \
    "SampleService01/SampleService01.UnitTests/SampleService01.UnitTests.csproj" 


######################################################################################
###
### Publish the Docker Container. 
### Docker Container should be between 100-200 MB. closer to 100 MB if trimmed.
###
### --self-contained true|false      
###     :: Determines if the app is self-contained or framework-dependent.
###
### -p:PublishSingleFile=true|false   
###     :: Packages the app into a platform-specific single-file executable.
###
### -p:PublishTrimmed=true|false
###     :: Trims unused libraries to reduce the deployment size of an app when publishing a self-contained executable.
###
### -p:DebugType=None -p:DebugSymbols=false 
###     :: Disables *.pdb file output. Exclude both for normal output from publish.
###
######################################################################################
WORKDIR /app/source/

RUN dotnet restore "SampleService01/SampleService01/SampleService01.csproj" \
    --verbosity detailed --disable-parallel

RUN dotnet publish "SampleService01/SampleService01/SampleService01.csproj" \ 
    --verbosity detailed --configuration release --output /app/publish --self-contained true -p:PublishSingleFile=true -p:PublishTrimmed=false 


FROM mcr.microsoft.com/dotnet/runtime:7.0-alpine AS container
COPY --from=release /app/publish/  /app

WORKDIR /app
CMD ["./SampleService01"]
