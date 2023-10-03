@Echo On
Pushd "%~dp0"

docker compose --file docker-compose.yml down --rmi all

popd