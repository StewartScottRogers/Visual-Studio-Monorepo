@Echo On
Pushd "%~dp0"

docker build --no-cache --file Dockerfile --pull --rm --tag simpleservice01:latest . 

pause
popd