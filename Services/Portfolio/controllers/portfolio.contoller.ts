import debug from 'debug';
import express from 'express';
import { Asset } from '../models/asset';
import { Portfolio } from '../models/portfolio';
import portfolioService from '../services/portfolio.service';

class PortfolioController {
    
    public async createPortfolio(req: express.Request, res:express.Response) {
        const portfolio: Portfolio = await portfolioService.createPortfolio(Number(req.params.userId));
        res.status(201).json(portfolio);
    }

    public async addAssets(req: express.Request, res:express.Response) {
        const asset: Asset | null = await portfolioService.addAssets(req.body);
        res.status(202).json(asset);
    }

    public async removeAssets(req: express.Request, res: express.Response) {
        const asset: Asset | null = await portfolioService.removeAssets(req.body);
        res.status(202).json(asset);
    }

    public async getPortfolioById(req: express.Request, res: express.Response) {
        const portfolio: Portfolio | null = await portfolioService.getPortfolioById(Number(req.params.portfolioId));
        res.status(200).json(portfolio);
    }

    public async getPortfolioByUserId(req: express.Request, res: express.Response) {
        const portfolio: Portfolio | null = await portfolioService.getPortfolioByUserId(Number(req.params.userId));
        res.status(200).json(portfolio);
    }

    public async deletePortfolioById(req: express.Request, res: express.Response) {
        const portfolio: Portfolio | null = await portfolioService.deletePortfolio(Number(req.params.portfolioId));
        res.status(200).json(portfolio);
    }
}

export default new PortfolioController();