import express from 'express';
import { CommonRoutesConfig } from "../common/common.routes.config";

export class PortfolioRoutes extends CommonRoutesConfig {
    constructor(app: express.Application) {
        super(app, `PortfolioRoutes`);
    }

    configureRoutes(): express.Application {
        this.app.route(`/portfolio`)
            .get((req: express.Request, res: express.Response) => {
                res.status(200).send(`List of `);
            })
            .post((req: express.Request, res: express.Response) => {
                res.status(200).send(`Post`);
            });

        this.app.route(`/portfolio/:portfolioId`)
            .all((req: express.Request, res: express.Response, next: express.NextFunction) => {
                next();
            })
            .get((req: express.Request, res: express.Response) => {
                res.status(200).send(`GET requested for id ${req.params.portfolioId}`);
            })
            .put((req: express.Request, res: express.Response) => {
                res.status(200).send(`PUT requested for id ${req.params.portfolioId}`);
            })
            .patch((req: express.Request, res: express.Response) => {
                res.status(200).send(`PATCH requested for id ${req.params.portfolioId}`);
            })
            .delete((req: express.Request, res: express.Response) => {
                res.status(200).send(`DELETE requested for id ${req.params.portfolioId}`);
            });
        
        return this.app;
    }
}