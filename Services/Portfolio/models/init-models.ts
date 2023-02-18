import type { Sequelize } from "sequelize";
import { Asset as _Asset } from "./asset";
import type { AssetAttributes, AssetCreationAttributes } from "./asset";
import { CurrancyRate as _CurrancyRate } from "./currancy_rate";
import type { CurrancyRateAttributes, CurrancyRateCreationAttributes } from "./currancy_rate";
import { Order as _Order } from "./order";
import type { OrderAttributes, OrderCreationAttributes } from "./order";
import { Portfolio as _Portfolio } from "./portfolio";
import type { PortfolioAttributes, PortfolioCreationAttributes } from "./portfolio";
import { Tempcurrency as _Tempcurrency } from "./tempcurrency";
import type { TempcurrencyAttributes, TempcurrencyCreationAttributes } from "./tempcurrency";
import { User as _User } from "./user";
import type { UserAttributes, UserCreationAttributes } from "./user";

export {
  _Asset as Asset,
  _CurrancyRate as CurrancyRate,
  _Order as Order,
  _Portfolio as Portfolio,
  _Tempcurrency as Tempcurrency,
  _User as User,
};

export type {
  AssetAttributes,
  AssetCreationAttributes,
  CurrancyRateAttributes,
  CurrancyRateCreationAttributes,
  OrderAttributes,
  OrderCreationAttributes,
  PortfolioAttributes,
  PortfolioCreationAttributes,
  TempcurrencyAttributes,
  TempcurrencyCreationAttributes,
  UserAttributes,
  UserCreationAttributes,
};

export function initModels(sequelize: Sequelize) {
  const Asset = _Asset.initModel(sequelize);
  const CurrancyRate = _CurrancyRate.initModel(sequelize);
  const Order = _Order.initModel(sequelize);
  const Portfolio = _Portfolio.initModel(sequelize);
  const Tempcurrency = _Tempcurrency.initModel(sequelize);
  const User = _User.initModel(sequelize);

  Asset.belongsTo(Portfolio, { as: "portfolio", foreignKey: "portfolioId"});
  Portfolio.hasMany(Asset, { as: "assets", foreignKey: "portfolioId"});
  Asset.belongsTo(Tempcurrency, { as: "currency", foreignKey: "currencyId"});
  Tempcurrency.hasMany(Asset, { as: "assets", foreignKey: "currencyId"});
  Portfolio.belongsTo(User, { as: "user", foreignKey: "userId"});
  User.hasMany(Portfolio, { as: "portfolios", foreignKey: "userId"});

  return {
    Asset: Asset,
    CurrancyRate: CurrancyRate,
    Order: Order,
    Portfolio: Portfolio,
    Tempcurrency: Tempcurrency,
    User: User,
  };
}
