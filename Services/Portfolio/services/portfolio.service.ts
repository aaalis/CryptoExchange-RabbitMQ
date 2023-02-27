import { Portfolio } from "../models/portfolio";
import { User } from "../models/user";
import { Asset } from '../models/asset';

class PortfolioService {

    async getUserByID(userId: number) {
        return await User.findByPk(userId);
    }

    async createPortfolio(userId: number) {
        return await Portfolio.create({userId});
    }

    async addAssets(asset: Asset) {
        return await Asset.create({ ...asset });
    }
}

export default new PortfolioService();