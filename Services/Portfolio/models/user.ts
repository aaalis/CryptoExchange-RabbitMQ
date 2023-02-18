import * as Sequelize from 'sequelize';
import { DataTypes, Model, Optional } from 'sequelize';
import type { Portfolio, PortfolioId } from './portfolio';

export interface UserAttributes {
  id: number;
  name?: string;
  login: string;
  password: string;
  creationdate?: Date;
  isdeleted: boolean;
}

export type UserPk = "id";
export type UserId = User[UserPk];
export type UserOptionalAttributes = "id" | "name" | "creationdate";
export type UserCreationAttributes = Optional<UserAttributes, UserOptionalAttributes>;

export class User extends Model<UserAttributes, UserCreationAttributes> implements UserAttributes {
  id!: number;
  name?: string;
  login!: string;
  password!: string;
  creationdate?: Date;
  isdeleted!: boolean;

  // User hasMany Portfolio via userId
  portfolios!: Portfolio[];
  getPortfolios!: Sequelize.HasManyGetAssociationsMixin<Portfolio>;
  setPortfolios!: Sequelize.HasManySetAssociationsMixin<Portfolio, PortfolioId>;
  addPortfolio!: Sequelize.HasManyAddAssociationMixin<Portfolio, PortfolioId>;
  addPortfolios!: Sequelize.HasManyAddAssociationsMixin<Portfolio, PortfolioId>;
  createPortfolio!: Sequelize.HasManyCreateAssociationMixin<Portfolio>;
  removePortfolio!: Sequelize.HasManyRemoveAssociationMixin<Portfolio, PortfolioId>;
  removePortfolios!: Sequelize.HasManyRemoveAssociationsMixin<Portfolio, PortfolioId>;
  hasPortfolio!: Sequelize.HasManyHasAssociationMixin<Portfolio, PortfolioId>;
  hasPortfolios!: Sequelize.HasManyHasAssociationsMixin<Portfolio, PortfolioId>;
  countPortfolios!: Sequelize.HasManyCountAssociationsMixin;

  static initModel(sequelize: Sequelize.Sequelize): typeof User {
    return User.init({
    id: {
      autoIncrement: true,
      type: DataTypes.INTEGER,
      allowNull: false,
      primaryKey: true
    },
    name: {
      type: DataTypes.STRING(50),
      allowNull: true
    },
    login: {
      type: DataTypes.STRING(50),
      allowNull: false
    },
    password: {
      type: DataTypes.STRING(50),
      allowNull: false
    },
    creationdate: {
      type: DataTypes.DATE,
      allowNull: true
    },
    isdeleted: {
      type: DataTypes.BOOLEAN,
      allowNull: false,
      defaultValue: false
    }
  }, {
    sequelize,
    tableName: 'users',
    schema: 'public',
    timestamps: false,
    indexes: [
      {
        name: "users_pkey",
        unique: true,
        fields: [
          { name: "id" },
        ]
      },
    ]
  });
  }
}
