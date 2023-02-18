import * as Sequelize from 'sequelize';
import { DataTypes, Model, Optional } from 'sequelize';
import type { Asset, AssetId } from './asset';
import type { User, UserId } from './user';

export interface PortfolioAttributes {
  id: number;
  userId?: number;
}

export type PortfolioPk = "id";
export type PortfolioId = Portfolio[PortfolioPk];
export type PortfolioOptionalAttributes = "id" | "userId";
export type PortfolioCreationAttributes = Optional<PortfolioAttributes, PortfolioOptionalAttributes>;

export class Portfolio extends Model<PortfolioAttributes, PortfolioCreationAttributes> implements PortfolioAttributes {
  id!: number;
  userId?: number;

  // Portfolio hasMany Asset via portfolioId
  assets!: Asset[];
  getAssets!: Sequelize.HasManyGetAssociationsMixin<Asset>;
  setAssets!: Sequelize.HasManySetAssociationsMixin<Asset, AssetId>;
  addAsset!: Sequelize.HasManyAddAssociationMixin<Asset, AssetId>;
  addAssets!: Sequelize.HasManyAddAssociationsMixin<Asset, AssetId>;
  createAsset!: Sequelize.HasManyCreateAssociationMixin<Asset>;
  removeAsset!: Sequelize.HasManyRemoveAssociationMixin<Asset, AssetId>;
  removeAssets!: Sequelize.HasManyRemoveAssociationsMixin<Asset, AssetId>;
  hasAsset!: Sequelize.HasManyHasAssociationMixin<Asset, AssetId>;
  hasAssets!: Sequelize.HasManyHasAssociationsMixin<Asset, AssetId>;
  countAssets!: Sequelize.HasManyCountAssociationsMixin;
  // Portfolio belongsTo User via userId
  user!: User;
  getUser!: Sequelize.BelongsToGetAssociationMixin<User>;
  setUser!: Sequelize.BelongsToSetAssociationMixin<User, UserId>;
  createUser!: Sequelize.BelongsToCreateAssociationMixin<User>;

  static initModel(sequelize: Sequelize.Sequelize): typeof Portfolio {
    return Portfolio.init({
    id: {
      autoIncrement: true,
      type: DataTypes.INTEGER,
      allowNull: false,
      primaryKey: true
    },
    userId: {
      type: DataTypes.INTEGER,
      allowNull: true,
      references: {
        model: 'users',
        key: 'id'
      }
    }
  }, {
    sequelize,
    tableName: 'portfolio',
    schema: 'public',
    timestamps: false,
    indexes: [
      {
        name: "portfolio_pkey",
        unique: true,
        fields: [
          { name: "id" },
        ]
      },
    ]
  });
  }
}
