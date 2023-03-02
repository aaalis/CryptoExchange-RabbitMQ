import { Portfolio } from "../models/portfolio";
import { User } from "../models/user";
import { Asset } from '../models/asset';
import { strict } from "assert";
import { Tempcurrency } from "../models/tempcurrency";

class PortfolioService {

    public async getUserById(userId: number) {
        return await User.findByPk(userId);
    }

    public async getCurrencyById(currencyId: number) {
        return await Tempcurrency.findByPk(currencyId);
    }

    public async getPortfolioById(portfolioId: number) {
        const portfolio: Portfolio | null =  await Portfolio.findByPk(portfolioId, {
            include: [`assets`],
        });
        return portfolio; 
    }

    public async getPortfolioByUserId(userID: number) {
        const portfolio: Portfolio | null = await Portfolio.findOne({
            where: {
                userId: userID
            },
            include: [`assets`],
        });
        return portfolio;
    }

    public async createPortfolio(userId: number) {
        return await Portfolio.create({userId});
    }

    public async addAssets(asset: Asset) {
        const finded: Asset | null = await this.findAssetByCurrencyId(asset);

        if (finded) {
            const totalAmount = Number(finded.dataValues.amount) + Number(asset.amount);
            
            finded.set({
                amount: totalAmount
            });

            await finded.save();
            return finded;
        }
        else {
            return await Asset.create({...asset});
        }
    }

    public async removeAssets(asset: Asset) {
        const finded: Asset | null = await this.findAssetByCurrencyId(asset);

        if (finded) {
            const totalAmount = Number(finded.dataValues.amount) - Number(asset.amount);

            finded.set({
                amount: totalAmount
            });

            await finded.save();
        }
        return finded;
    }

    public async findAsset(portfolioID: number, currencyID: number) {
        return await Asset.findOne({
            where: {
                portfolioId: portfolioID,
                currencyId: currencyID
            }
        });
    }

    private async findAssetByCurrencyId(asset: Asset) {
        return await Asset.findOne({
            where: {
                currencyId: asset.currencyId
            }
        });
    }
}

export default new PortfolioService();