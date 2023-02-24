import debug from 'debug';
import express from 'express';
import { Portfolio } from '../models/portfolio';
import portfolioService from '../services/portfolio.service';

const log: debug.IDebugger = debug(`app:portfolio-controller`);
class PortfolioController {
    
    async createPortfolio(req: express.Request, res:express.Response) {
        const portfolio: Portfolio = await portfolioService.createPortfolio(Number(req.params.userId));
        res.status(201).json(portfolio);
    }
}

export default new PortfolioController();