import * as Sequelize from 'sequelize';
import { DataTypes, Model, Optional } from 'sequelize';
import type { Portfolio, PortfolioId } from './portfolio';
import type { Tempcurrency, TempcurrencyId } from './tempcurrency';

export interface AssetAttributes {
  id: number;
  portfolioId?: number;
  amount?: number;
  currencyId?: number;
}

export type AssetPk = "id";
export type AssetId = Asset[AssetPk];
export type AssetOptionalAttributes = "id" | "portfolioId" | "amount" | "currencyId";
export type AssetCreationAttributes = Optional<AssetAttributes, AssetOptionalAttributes>;

export class Asset extends Model<AssetAttributes, AssetCreationAttributes> implements AssetAttributes {
  id!: number;
  portfolioId?: number;
  amount?: number;
  currencyId?: number;

  // Asset belongsTo Portfolio via portfolioId
  portfolio!: Portfolio;
  getPortfolio!: Sequelize.BelongsToGetAssociationMixin<Portfolio>;
  setPortfolio!: Sequelize.BelongsToSetAssociationMixin<Portfolio, PortfolioId>;
  createPortfolio!: Sequelize.BelongsToCreateAssociationMixin<Portfolio>;
  // Asset belongsTo Tempcurrency via currencyId
  currency!: Tempcurrency;
  getCurrency!: Sequelize.BelongsToGetAssociationMixin<Tempcurrency>;
  setCurrency!: Sequelize.BelongsToSetAssociationMixin<Tempcurrency, TempcurrencyId>;
  createCurrency!: Sequelize.BelongsToCreateAssociationMixin<Tempcurrency>;

  static initModel(sequelize: Sequelize.Sequelize): typeof Asset {
    return Asset.init({
    id: {
      autoIncrement: true,
      type: DataTypes.INTEGER,
      allowNull: false,
      primaryKey: true
    },
    portfolioId: {
      type: DataTypes.INTEGER,
      allowNull: true,
      references: {
        model: 'portfolio',
        key: 'id'
      }
    },
    amount: {
      type: DataTypes.DECIMAL,
      allowNull: true
    },
    currencyId: {
      type: DataTypes.INTEGER,
      allowNull: true,
      references: {
        model: 'tempcurrency',
        key: 'id'
      }
    }
  }, {
    sequelize,
    tableName: 'assets',
    schema: 'public',
    timestamps: false,
    indexes: [
      {
        name: "assets_pkey",
        unique: true,
        fields: [
          { name: "id" },
        ]
      },
    ]
  });
  }
}
