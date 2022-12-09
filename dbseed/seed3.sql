CREATE DATABASE "DB";

\connect DB

CREATE TYPE OrdersKind AS ENUM ('Buy', 'Sell');

CREATE TYPE OrdersCurrency AS ENUM ('BTC', 'ETH', 'DASH');

CREATE TYPE ActionType AS ENUM ('UP', 'DOWN');

CREATE TABLE orders
(
    id serial PRIMARY KEY,
    ownerguid VARCHAR(50) NOT NULL,
    kind OrdersKind,
    count INTEGER,
    price DECIMAL,
    basecurrency OrdersCurrency,
    quotecurrency OrdersCurrency
);

INSERT INTO orders(ownerguid, kind, count, price, basecurrency, quotecurrency) VALUES ('test1', 'Buy', 55, 200.0, 'BTC', 'DASH');
INSERT INTO orders(ownerguid, kind, count, price, basecurrency, quotecurrency) VALUES ('test1', 'Buy', 66, 300.0, 'BTC', 'DASH');
INSERT INTO orders(ownerguid, kind, count, price, basecurrency, quotecurrency) VALUES ('test1', 'Sell', 33, 100.0, 'BTC', 'DASH');
INSERT INTO orders(ownerguid, kind, count, price, basecurrency, quotecurrency) VALUES ('test1', 'Sell', 11, 1200.0, 'BTC', 'DASH');
INSERT INTO orders(ownerguid, kind, count, price, basecurrency, quotecurrency) VALUES ('test2', 'Buy', 22, 222.0, 'BTC', 'ETH');
INSERT INTO orders(ownerguid, kind, count, price, basecurrency, quotecurrency) VALUES ('test2', 'Buy', 44, 444.0, 'BTC', 'ETH');
INSERT INTO orders(ownerguid, kind, count, price, basecurrency, quotecurrency) VALUES ('test2', 'Sell', 77, 777.0, 'BTC', 'ETH');
INSERT INTO orders(ownerguid, kind, count, price, basecurrency, quotecurrency) VALUES ('test2', 'Sell', 88, 888.0, 'BTC', 'ETH');
INSERT INTO orders(ownerguid, kind, count, price, basecurrency, quotecurrency) VALUES ('test3', 'Buy', 1, 11.0, 'DASH', 'ETH');
INSERT INTO orders(ownerguid, kind, count, price, basecurrency, quotecurrency) VALUES ('test3', 'Buy', 2, 22.0, 'DASH', 'ETH');
INSERT INTO orders(ownerguid, kind, count, price, basecurrency, quotecurrency) VALUES ('test3', 'Sell', 3, 33.0, 'DASH', 'ETH');
INSERT INTO orders(ownerguid, kind, count, price, basecurrency, quotecurrency) VALUES ('test3', 'Sell', 4, 44.0, 'DASH', 'ETH');

CREATE TABLE currancy_rate
(
    id serial PRIMARY KEY,
    back_ref_action ActionType,
    date_of_change TIMESTAMP,
    currency OrdersCurrency,
    price DECIMAL
);

INSERT INTO currancy_rate(back_ref_action, date_of_change, currency, price) VALUES ('UP', '1999-01-08 04:05:06', 'BTC', 2000.0);
INSERT INTO currancy_rate(back_ref_action, date_of_change, currency, price) VALUES ('DOWN', '1999-01-09 04:05:06', 'BTC', 1900.0);
INSERT INTO currancy_rate(back_ref_action, date_of_change, currency, price) VALUES ('UP', '1999-01-10 04:05:06', 'BTC', 1950.0);

INSERT INTO currancy_rate(back_ref_action, date_of_change, currency, price) VALUES ('UP', '1999-01-08 04:05:06', 'ETH', 500.0);
INSERT INTO currancy_rate(back_ref_action, date_of_change, currency, price) VALUES ('DOWN', '1999-01-09 04:05:06', 'ETH', 450.0);
INSERT INTO currancy_rate(back_ref_action, date_of_change, currency, price) VALUES ('UP', '1999-01-10 04:05:06', 'BTC', 490.0);

INSERT INTO currancy_rate(back_ref_action, date_of_change, currency, price) VALUES ('UP', '1999-01-08 04:05:06', 'DASH', 100.0);
INSERT INTO currancy_rate(back_ref_action, date_of_change, currency, price) VALUES ('DOWN', '1999-01-09 04:05:06', 'DASH', 90.0);
INSERT INTO currancy_rate(back_ref_action, date_of_change, currency, price) VALUES ('UP', '1999-01-10 04:05:06', 'DASH', 95.0);