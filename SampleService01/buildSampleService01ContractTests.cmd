@Echo On
Pushd "%~dp0"

docker build --no-cache --file SampleService01.ContractTest.dockerfile --pull --rm --tag simpleservice01contracttests:latest . 

pause
popd