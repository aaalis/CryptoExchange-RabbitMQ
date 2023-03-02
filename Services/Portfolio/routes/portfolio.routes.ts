import express from 'express';
import { CommonRoutesConfig } from "../common/common.routes.config";
import portfolioContoller from '../controllers/portfolio.contoller';
import portfolioMiddleware from '../middleware/portfolio.middleware';

export class PortfolioRoutes extends CommonRoutesConfig {
    
    constructor(app: express.Application) {
        super(app, `PortfolioRoutes`);
    }

    configureRoutes(): express.Application {

        /**
         * @swagger
         * /PortfolioAPI/addAssets:
         *   patch:
         *     summary: Add assets to portfolio.
         *     requestBody:
         *       required: true
         *       content:
         *         application/json:
         *           schema:
         *             type: object
         *             properties:
         *               portfolioId:
         *                 type: integer
         *                 description: Portfolio Id/Primary key
         *                 example: 1
         *               amount:
         *                 type: integer
         *                 description: Amount of new assets
         *                 example: 100
         *               currencyId:
         *                 type: integer
         *                 description: Currency Id/Foreign key
         *                 example: 2
         *     responses:
         *       202:
         *         description: Assets added
         *         content:
         *           application/json:
         *             schema:
         *               type: object
         */
        this.app.route(`/PortfolioAPI/addAssets`)
                .patch(
                    portfolioMiddleware.validateAssetFields,
                    portfolioMiddleware.validateCurrencyExist,
                    portfolioMiddleware.validateAssetAmountPositive,
                    portfolioContoller.addAssets
                );
        /**
         * @swagger
         * /PortfolioAPI/removeAssets:
         *   patch:
         *     summary: Remove assets to portfolio.
         *     requestBody:
         *       required: true
         *       content:
         *         application/json:
         *           schema:
         *             type: object
         *             properties:
         *               portfolioId:
         *                 type: integer
         *                 description: Portfolio Id/Primary key
         *                 example: 1
         *               amount:
         *                 type: integer
         *                 description: Amount assets
         *                 example: 100
         *               currencyId:
         *                 type: integer
         *                 description: Currency Id/Foreign key
         *                 example: 2
         *     responses:
         *       202:
         *         description: Assets removed
         *         content:
         *           application/json:
         *             schema:
         *               type: object
         */
        this.app.route(`/PortfolioAPI/removeAssets`)
                .patch(
                    portfolioMiddleware.validateAssetFields,
                    portfolioMiddleware.validateCurrencyExist,
                    portfolioMiddleware.validateAssetAmountPositive,
                    portfolioMiddleware.checkAmountAssets,
                    portfolioContoller.removeAssets
                );

        /**
         * @swagger
         * /PortfolioAPI/getById/{portfolioId}:
         *   get:
         *     summary: Get portfolio by id.
         *     parameters:
         *       - in: path
         *         name: portfolioId
         *         required: true
         *         description: Numeric ID of the portfolio.
         *         schema:
         *           type: integer
         *     responses:
         *       200:
         *         description: A single portfolio.
         *         content:
         *           application/json:
         *             schema:
         *               type: object
         */
        this.app.route(`/PortfolioAPI/getById/:portfolioId`)
                .get(
                    portfolioContoller.getPortfolioById
                );

        this.app.route(`/PortfolioAPI/getByUserId/:userId`)
                .get(
                    portfolioContoller.getPortfolioByUserId
                )
        
        this.app.route(`/PortfolioAPI/delete/:portfolioId`)
                .delete(

                );

        this.app.param(`userId`, portfolioMiddleware.extractUserId)
        /**
         * @swagger
         * /PortfolioAPI/create/{userId}:
         *   post:
         *     summary: Create portfolio.
         *     parameters:
         *       - in: path
         *         name: userId
         *         required: true
         *         description: user id.
         *         schema:
         *           type: integer
         *     responses:
         *       201:
         *         description: New portfolio.
         *         content:
         *           application/json:
         *             schema:
         *               type: object
         */
        this.app.route(`/PortfolioAPI/create/:userId`)
                .post(
                    //portfolioMiddleware.validateRequiredField,
                    portfolioMiddleware.validateUserExist,
                    portfolioContoller.createPortfolio
                );
        
        return this.app;
    }
}