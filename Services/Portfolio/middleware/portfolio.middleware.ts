import express from 'express';
import portfolioService from '../services/portfolio.service';

class PortfolioMiddleware {
    
    async validateRequiredField(req: express.Request, res: express.Response, next: express.NextFunction) {
        if (req.body) {
            next();
        }
        else {
            res.status(400).send({error: `Missing required field: userId`});
        }
    }
    
    async validateUserExist(req: express.Request, res: express.Response, next: express.NextFunction) {
        const id: number = req.body.id;
        const user = await portfolioService.getUserByID(id);
        
        if (user) {
            next();            
        }
        else {
            res.status(400).send({error: `User does not exist with id:${id}`});
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