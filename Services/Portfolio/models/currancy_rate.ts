import * as Sequelize from 'sequelize';
import { DataTypes, Model, Optional } from 'sequelize';

export interface CurrancyRateAttributes {
  id: number;
  back_ref_action?: "UP" | "DOWN";
  date_of_change?: Date;
  currency?: "BTC" | "ETH" | "DASH";
  price?: number;
}

export type CurrancyRatePk = "id";
export type CurrancyRateId = CurrancyRate[CurrancyRatePk];
export type CurrancyRateOptionalAttributes = "id" | "back_ref_action" | "date_of_change" | "currency" | "price";
export type CurrancyRateCreationAttributes = Optional<CurrancyRateAttributes, CurrancyRateOptionalAttributes>;

export class CurrancyRate extends Model<CurrancyRateAttributes, CurrancyRateCreationAttributes> implements CurrancyRateAttributes {
  id!: number;
  back_ref_action?: "UP" | "DOWN";
  date_of_change?: Date;
  currency?: "BTC" | "ETH" | "DASH";
  price?: number;


  static initModel(sequelize: Sequelize.Sequelize): typeof CurrancyRate {
    return CurrancyRate.init({
    id: {
      autoIncrement: true,
      type: DataTypes.INTEGER,
      allowNull: false,
      primaryKey: true
    },
    back_ref_action: {
      type: DataTypes.ENUM("UP","DOWN"),
      allowNull: true
    },
    date_of_change: {
      type: DataTypes.DATE,
      allowNull: true
    },
    currency: {
      type: DataTypes.ENUM("BTC","ETH","DASH"),
      allowNull: true
    },
    price: {
      type: DataTypes.DECIMAL,
      allowNull: true
    }
  }, {
    sequelize,
    tableName: 'currancy_rate',
    schema: 'public',
    timestamps: false,
    indexes: [
      {
        name: "currancy_rate_pkey",
        unique: true,
        fields: [
          { name: "id" },
        ]
      },
    ]
  });
  }
}
