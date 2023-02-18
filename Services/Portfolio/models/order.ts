import * as Sequelize from 'sequelize';
import { DataTypes, Model, Optional } from 'sequelize';

export interface OrderAttributes {
  id: number;
  ownerguid: string;
  kind?: "Buy" | "Sell";
  count?: number;
  price?: number;
  basecurrency?: "BTC" | "ETH" | "DASH";
  quotecurrency?: "BTC" | "ETH" | "DASH";
}

export type OrderPk = "id";
export type OrderId = Order[OrderPk];
export type OrderOptionalAttributes = "id" | "kind" | "count" | "price" | "basecurrency" | "quotecurrency";
export type OrderCreationAttributes = Optional<OrderAttributes, OrderOptionalAttributes>;

export class Order extends Model<OrderAttributes, OrderCreationAttributes> implements OrderAttributes {
  id!: number;
  ownerguid!: string;
  kind?: "Buy" | "Sell";
  count?: number;
  price?: number;
  basecurrency?: "BTC" | "ETH" | "DASH";
  quotecurrency?: "BTC" | "ETH" | "DASH";


  static initModel(sequelize: Sequelize.Sequelize): typeof Order {
    return Order.init({
    id: {
      autoIncrement: true,
      type: DataTypes.INTEGER,
      allowNull: false,
      primaryKey: true
    },
    ownerguid: {
      type: DataTypes.STRING(50),
      allowNull: false
    },
    kind: {
      type: DataTypes.ENUM("Buy","Sell"),
      allowNull: true
    },
    count: {
      type: DataTypes.INTEGER,
      allowNull: true
    },
    price: {
      type: DataTypes.DECIMAL,
      allowNull: true
    },
    basecurrency: {
      type: DataTypes.ENUM("BTC","ETH","DASH"),
      allowNull: true
    },
    quotecurrency: {
      type: DataTypes.ENUM("BTC","ETH","DASH"),
      allowNull: true
    }
  }, {
    sequelize,
    tableName: 'orders',
    schema: 'public',
    timestamps: false,
    indexes: [
      {
        name: "orders_pkey",
        unique: true,
        fields: [
          { name: "id" },
        ]
      },
    ]
  });
  }
}
