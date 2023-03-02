import express from 'express';
import portfolioService from '../services/portfolio.service';
import debug from 'debug';
import { Asset } from '../models/asset';

const log: debug.IDebugger = debug('app:users-controller');
class PortfolioMiddleware {
    
    async validateRequiredField(req: express.Request, res: express.Response, next: express.NextFunction) {
        if (req.body) {
            next();
        }
        else {
            res.status(400).send({error: `Missing required field: userId`});
        }
    }

    async validateAssetFields(req: express.Request, res: express.Response, next:express.NextFunction) {
        if (req.body.portfolioId && req.body.amount && req.body.currencyId) {
            next();            
        }
        else {
            res.status(400).send({error:`Missing required asset fields`});
        }
    }

    async validateAssetAmountPositive(req: express.Request, res: express.Response, next:express.NextFunction) {
        const amount = req.body.amount
        if (amount > 0) {
            next();
        }
        else {
            res.status(400).send({error: `Amount of assets is not positive`});
        }
    }
    
    async validateUserExist(req: express.Request, res: express.Response, next: express.NextFunction) {
        const id: number = req.body.id;
        const user = await portfolioService.getUserById(id);
        
        if (user) {
            next();            
        }
        else {
            res.status(400).send({error: `User does not exist with id:${id}`});
        }
    }

    async validateCurrencyExist(req: express.Request, res: express.Response, next: express.NextFunction) {
        const currId: number = req.body.currencyId;
        const currency = await portfolioService.getCurrencyById(currId);

        if (currency) {
            next();
        }
        else {
            res.status(400).send({error: `Currency does not exist with id: ${currId}`});
        }
    }

    async checkAmountAssets(req: express.Request, res: express.Response, next: express.NextFunction) {
        const amountRemove: number = req.body.amount;
        const asset: Asset | null = await portfolioService.findAsset(req.body.portfolioId, req.body.currencyId);
        
        if (asset && asset.dataValues.amount != undefined) {
            if (amountRemove > asset.dataValues.amount ) {
                res.status(400).send({error: `Deletion amount exceeds available`});
            }
            else {
                next();
            }
        }
    }

    async extractUserId(req: express.Request, res: express.Response, next: express.NextFunction) {
        req.body.id = req.params.userId;
        next();
    }

    async extractPortfolioId(req: express.Request, res: express.Response, next: express.NextFunction) {
        req.body.id = req.params.portfolioId;
        next();
    }
}

export default new PortfolioMiddleware();