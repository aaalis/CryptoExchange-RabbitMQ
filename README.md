# CryptoExchange

## Для запуска необходимо:
### git clone https://github.com/aaalis/CryptoExchange
### cd CryptoExchange
### docker-compose up

Концепция(./Concept) - Концептия проекта

REST(/Services/Orders) - Сервис для работы с ордерами
http://localhost:8080/swagger/index.html

gRPC(./Services/Rate) - Сервис для работой с котировками валюты
http://localhost:8081
Для тестирования нужно прописать dotnet run, находясь в /Services/Rate/gRPC_Client

Adminer - http://localhost:8088

Брокер сообщений(./MessageBroker) - Доклад Kafka
