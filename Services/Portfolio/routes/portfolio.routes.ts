import express from 'express';
import { CommonRoutesConfig } from "../common/common.routes.config";
import portfolioContoller from '../controllers/portfolio.contoller';
import portfolioMiddleware from '../middleware/portfolio.middleware';

export class PortfolioRoutes extends CommonRoutesConfig {
    
    constructor(app: express.Application) {
        super(app, `PortfolioRoutes`);
    }

    configureRoutes(): express.Application {

        this.app.route(`/portfolio/addAssets`)
                .put(
                    //middleware
                    portfolioContoller.addAssets
                );
        
        this.app.param(`userId`, portfolioMiddleware.extractUserId)
        this.app.route(`/portfolio/:userId`)
                .post(
                    portfolioMiddleware.validateRequiredField,
                    portfolioMiddleware.validateUserExist,
                    portfolioContoller.createPortfolio
                );
        
        return this.app;
    }
}