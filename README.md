# CryptoExchange
## Для запуска необходимо:
git clone https://github.com/aaalis/CryptoExchange

cd CryptoExchange

docker-compose up

## Описание
Концепция(./Concept) - Концептия проекта

Orders(./Services/Orders) - REST Сервис для работы с ордерами
http://localhost:8080/swagger/index.html

Rate(./Services/Rate) - gRPC Сервис для работой с котировками валюты
http://localhost:8081
Для тестирования нужно прописать dotnet run, находясь в /Services/Rate/gRPC_Client

Adminer - http://localhost:8088

Брокер сообщений(./MessageBroker) - Доклад Kafka
