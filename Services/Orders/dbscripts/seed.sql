\connect orderDB

CREATE TYPE OrdersKind AS ENUM ('Buy', 'Sell');

CREATE TYPE OrdersCurrency AS ENUM ('BTC', 'ETH', 'DASH');

CREATE TABLE orders
(
    ID serial PRIMARY KEY,
    OwnerGuid VARCHAR(50) NOT NULL,
    Kind OrdersKind,
    Count INTEGER,
    Price DECIMAL,
    BaseCurrency OrdersCurrency,
    QuoteCurrency OrdersCurrency
);

INSERT INTO orders(OwnerGuid, Kind, Count, Price, BaseCurrency, QuoteCurrency) VALUES ('test1', 'Buy', 55, 200.0, 'BTC', 'DASH');
INSERT INTO orders(OwnerGuid, Kind, Count, Price, BaseCurrency, QuoteCurrency) VALUES ('test1', 'Buy', 66, 300.0, 'BTC', 'DASH');
INSERT INTO orders(OwnerGuid, Kind, Count, Price, BaseCurrency, QuoteCurrency) VALUES ('test1', 'Sell', 33, 100.0, 'BTC', 'DASH');
INSERT INTO orders(OwnerGuid, Kind, Count, Price, BaseCurrency, QuoteCurrency) VALUES ('test1', 'Sell', 11, 1200.0, 'BTC', 'DASH');
INSERT INTO orders(OwnerGuid, Kind, Count, Price, BaseCurrency, QuoteCurrency) VALUES ('test2', 'Buy', 22, 222.0, 'BTC', 'ETH');
INSERT INTO orders(OwnerGuid, Kind, Count, Price, BaseCurrency, QuoteCurrency) VALUES ('test2', 'Buy', 44, 444.0, 'BTC', 'ETH');
INSERT INTO orders(OwnerGuid, Kind, Count, Price, BaseCurrency, QuoteCurrency) VALUES ('test2', 'Sell', 77, 777.0, 'BTC', 'ETH');
INSERT INTO orders(OwnerGuid, Kind, Count, Price, BaseCurrency, QuoteCurrency) VALUES ('test2', 'Sell', 88, 888.0, 'BTC', 'ETH');
INSERT INTO orders(OwnerGuid, Kind, Count, Price, BaseCurrency, QuoteCurrency) VALUES ('test3', 'Buy', 1, 11.0, 'DASH', 'ETH');
INSERT INTO orders(OwnerGuid, Kind, Count, Price, BaseCurrency, QuoteCurrency) VALUES ('test3', 'Buy', 2, 22.0, 'DASH', 'ETH');
INSERT INTO orders(OwnerGuid, Kind, Count, Price, BaseCurrency, QuoteCurrency) VALUES ('test3', 'Sell', 3, 33.0, 'DASH', 'ETH');
INSERT INTO orders(OwnerGuid, Kind, Count, Price, BaseCurrency, QuoteCurrency) VALUES ('test3', 'Sell', 4, 44.0, 'DASH', 'ETH');