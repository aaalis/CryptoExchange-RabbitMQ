CREATE DATABASE "DB";

\c DB

CREATE TABLE users
(
    id serial PRIMARY KEY,
    name VARCHAR(50),
    login VARCHAR(50) NOT NULL,
    password VARCHAR(50) NOT NULL,
    creationDate TIMESTAMP,
    isDeleted BOOLEAN DEFAULT FALSE NOT NULL
);

INSERT INTO users(name, login, password, creationDate) VALUES ('TestName1', 'TestLogin1', 'TestPassword1', '2022-01-01 01:01:01');
INSERT INTO users(name, login, password, creationDate) VALUES ('TestName2', 'TestLogin2', 'TestPassword2', '2022-01-01 01:01:01');
INSERT INTO users(name, login, password, creationDate) VALUES ('TestName3', 'TestLogin3', 'TestPassword3', '2022-01-01 01:01:01');

CREATE TABLE currencies
(
    id serial PRIMARY KEY,
    "name" VARCHAR(50) NOT NULL
);

INSERT INTO currencies("name") VALUES ('BTC');
INSERT INTO currencies("name") VALUES ('ETH');
INSERT INTO currencies("name") VALUES ('DASH');

-- CREATE TABLE OrdersKind
-- (
--     id serial PRIMARY KEY,
--     kind VARCHAR(50) NOT NULL
-- );

-- INSERT INTO OrdersKind(currency) VALUES ('DASH');
-- INSERT INTO OrdersKind(currency) VALUES ('DASH');

-- CREATE TABLE ActionType
-- (
--     id serial PRIMARY KEY,
--     actype VARCHAR(50) NOT NULL
-- );

-- INSERT INTO ActionType(currency) VALUES ('UP');
-- INSERT INTO ActionType(currency) VALUES ('DOWN');

CREATE TYPE orderskind AS ENUM ('Buy', 'Sell');

-- CREATE TYPE OrdersCurrency AS ENUM ('BTC', 'ETH', 'DASH');

-- CREATE TYPE actiontype AS ENUM ('UP', 'DOWN');

CREATE TABLE orders
(
    id serial PRIMARY KEY,
    userid INTEGER,
    kind orderskind,
    count INTEGER,
    price DECIMAL,
    basecurrencyid INTEGER,
    quotecurrencyid INTEGER,
    isdeleted BOOLEAN DEFAULT FALSE NOT NULL,
    CONSTRAINT "basecurrencyid_id" FOREIGN KEY ("basecurrencyid")
        REFERENCES public.currencies (id) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION,
    CONSTRAINT "quotecurrencyid_id" FOREIGN KEY ("quotecurrencyid")
        REFERENCES public.currencies (id) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION,
    CONSTRAINT "userid_id" FOREIGN KEY ("userid")
        REFERENCES public.users (id) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
);

INSERT INTO orders(userid, kind, count, price, basecurrencyid, quotecurrencyid) VALUES (1, 'Buy', 55, 200.0, 1, 2);
INSERT INTO orders(userid, kind, count, price, basecurrencyid, quotecurrencyid) VALUES (1, 'Buy', 66, 300.0, 1, 3);
INSERT INTO orders(userid, kind, count, price, basecurrencyid, quotecurrencyid) VALUES (1, 'Sell', 33, 100.0, 2, 1);
INSERT INTO orders(userid, kind, count, price, basecurrencyid, quotecurrencyid) VALUES (1, 'Sell', 11, 1200.0, 2, 3);
INSERT INTO orders(userid, kind, count, price, basecurrencyid, quotecurrencyid) VALUES (2, 'Buy', 22, 222.0, 1, 2);
INSERT INTO orders(userid, kind, count, price, basecurrencyid, quotecurrencyid) VALUES (2, 'Buy', 44, 444.0, 1, 3);
INSERT INTO orders(userid, kind, count, price, basecurrencyid, quotecurrencyid) VALUES (2, 'Sell', 77, 777.0, 3, 2);
INSERT INTO orders(userid, kind, count, price, basecurrencyid, quotecurrencyid) VALUES (2, 'Sell', 88, 888.0, 3, 1);
INSERT INTO orders(userid, kind, count, price, basecurrencyid, quotecurrencyid) VALUES (3, 'Buy', 1, 11.0, 3, 2);
INSERT INTO orders(userid, kind, count, price, basecurrencyid, quotecurrencyid) VALUES (3, 'Buy', 2, 22.0, 3, 2);
INSERT INTO orders(userid, kind, count, price, basecurrencyid, quotecurrencyid) VALUES (3, 'Sell', 3, 33.0, 2, 3);
INSERT INTO orders(userid, kind, count, price, basecurrencyid, quotecurrencyid) VALUES (3, 'Sell', 4, 44.0, 2, 1);

-- CREATE TABLE currancy_rate
-- (
--     id serial PRIMARY KEY,
--     back_ref_action ActionType,
--     date_of_change TIMESTAMP,
--     currency OrdersCurrency,
--     price DECIMAL
-- );

-- INSERT INTO currancy_rate(back_ref_action, date_of_change, currency, price) VALUES ('UP', '1999-01-08 04:05:06', 'BTC', 2000.0);
-- INSERT INTO currancy_rate(back_ref_action, date_of_change, currency, price) VALUES ('DOWN', '1999-01-09 04:05:06', 'BTC', 1900.0);
-- INSERT INTO currancy_rate(back_ref_action, date_of_change, currency, price) VALUES ('UP', '1999-01-10 04:05:06', 'BTC', 1950.0);

-- INSERT INTO currancy_rate(back_ref_action, date_of_change, currency, price) VALUES ('UP', '1999-01-08 04:05:06', 'ETH', 500.0);
-- INSERT INTO currancy_rate(back_ref_action, date_of_change, currency, price) VALUES ('DOWN', '1999-01-09 04:05:06', 'ETH', 450.0);
-- INSERT INTO currancy_rate(back_ref_action, date_of_change, currency, price) VALUES ('UP', '1999-01-10 04:05:06', 'BTC', 490.0);

-- INSERT INTO currancy_rate(back_ref_action, date_of_change, currency, price) VALUES ('UP', '1999-01-08 04:05:06', 'DASH', 100.0);
-- INSERT INTO currancy_rate(back_ref_action, date_of_change, currency, price) VALUES ('DOWN', '1999-01-09 04:05:06', 'DASH', 90.0);
-- INSERT INTO currancy_rate(back_ref_action, date_of_change, currency, price) VALUES ('UP', '1999-01-10 04:05:06', 'DASH', 95.0);

CREATE TABLE portfolio
(
    id serial PRIMARY KEY,
    "userId" INTEGER NOT NULL,
    CONSTRAINT "userId_id" FOREIGN KEY ("userId")
        REFERENCES public.users (id) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
);

INSERT INTO portfolio("userId") VALUES (1);
INSERT INTO portfolio("userId") VALUES (2);
INSERT INTO portfolio("userId") VALUES (3);

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
        REFERENCES public.currencies (id) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
);

INSERT INTO assets("portfolioId", amount, "currencyId") VALUES(1, 1200, 1);
INSERT INTO assets("portfolioId", amount, "currencyId") VALUES(1, 1200, 2);
INSERT INTO assets("portfolioId", amount, "currencyId") VALUES(1, 1200, 3);
INSERT INTO assets("portfolioId", amount, "currencyId") VALUES(2, 800, 1);
INSERT INTO assets("portfolioId", amount, "currencyId") VALUES(2, 900, 2);
INSERT INTO assets("portfolioId", amount, "currencyId") VALUES(2, 1000, 3);
INSERT INTO assets("portfolioId", amount, "currencyId") VALUES(3, 1300, 1);
INSERT INTO assets("portfolioId", amount, "currencyId") VALUES(3, 1400, 2);
INSERT INTO assets("portfolioId", amount, "currencyId") VALUES(3, 1500, 3);