@Echo On
Pushd "%~dp0"

docker build --no-cache --file SampleService01.dockerfile --pull --rm --tag simpleservice01:latest . 

pause
popd