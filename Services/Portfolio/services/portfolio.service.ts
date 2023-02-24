import { Portfolio } from "../models/portfolio";
import { User } from "../models/user";

class PortfolioService {

    async getUserByID(userId: number) {
        return await User.findByPk(userId);
    }

    async createPortfolio(userId: number) {
        return await Portfolio.create({userId});
    }
}

export default new PortfolioService();