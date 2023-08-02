import * as Sequelize from 'sequelize';
import { DataTypes, Model, Optional } from 'sequelize';
import type { Asset, AssetId } from './asset';

export interface TempcurrencyAttributes {
  id: number;
  name: string;
}

export type TempcurrencyPk = "id";
export type TempcurrencyId = Tempcurrency[TempcurrencyPk];
export type TempcurrencyOptionalAttributes = "id";
export type TempcurrencyCreationAttributes = Optional<TempcurrencyAttributes, TempcurrencyOptionalAttributes>;

export class Tempcurrency extends Model<TempcurrencyAttributes, TempcurrencyCreationAttributes> implements TempcurrencyAttributes {
  id!: number;
  name!: string;

  // Tempcurrency hasMany Asset via currencyId
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

  static initModel(sequelize: Sequelize.Sequelize): typeof Tempcurrency {
    return Tempcurrency.init({
    id: {
      autoIncrement: true,
      type: DataTypes.INTEGER,
      allowNull: false,
      primaryKey: true
    },
    name: {
      type: DataTypes.STRING(50),
      allowNull: false
    }
  }, {
    sequelize,
    tableName: 'currencies',
    schema: 'public',
    timestamps: false,
    indexes: [
      {
        name: "currencies_pkey",
        unique: true,
        fields: [
          { name: "id" },
        ]
      },
    ]
  });
  }
}
