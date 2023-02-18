CREATE DATABASE "DB";

\c DB

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

CREATE TABLE users
(
    id serial PRIMARY KEY,
    name VARCHAR(50),
    login VARCHAR(50) NOT NULL,
    password VARCHAR(50) NOT NULL,
    creationDate TIMESTAMP,
    isDeleted BOOLEAN DEFAULT FALSE NOT NULL
);

INSERT INTO users(name, login, password, creationDate, isDeleted) VALUES ('TestName1', 'TestLogin1', 'TestPassword1', '2022-01-01 01:01:01', false);

CREATE TABLE portfolio
(
    id serial PRIMARY KEY,
    "userId" INTEGER,
    CONSTRAINT "userId_id" FOREIGN KEY ("userId")
        REFERENCES public.users (id) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
);

INSERT INTO portfolio("userId") VALUES (1);

CREATE TABLE tempcurrency
(
    id serial PRIMARY KEY,
    currency VARCHAR(50) NOT NULL
);

INSERT INTO tempcurrency(currency) VALUES ('BTC');
INSERT INTO tempcurrency(currency) VALUES ('ETH');
INSERT INTO tempcurrency(currency) VALUES ('DASH');

CREATE TABLE assets
(
    id serial PRIMARY KEY,
    "portfolioId" INTEGER,
    amount DECIMAL,
    "currencyId" INTEGER,
    CONSTRAINT "portfolioId_id" FOREIGN KEY ("portfolioId")
        REFERENCES public.portfolio (id) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION,
    CONSTRAINT "currencyId_id" FOREIGN KEY ("currencyId")
        REFERENCES public.tempcurrency (id) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
);

INSERT INTO assets("portfolioId", amount, "currencyId") VALUES(1, 20, 2);