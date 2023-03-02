import express from 'express';
import * as http from 'http';

import * as winston from 'winston';
import * as expressWinston from 'express-winston';
import cors from 'cors';
import {CommonRoutesConfig} from './common/common.routes.config';
import {PortfolioRoutes} from './routes/portfolio.routes';
import debug from 'debug';
import connection from './connection';
import { initModels } from './models/init-models';
import ApplicationOptions from './appOptions';


const swaggerJsdoc = require("swagger-jsdoc");
const swaggerUi = require("swagger-ui-express");

const app: express.Application = express();
const server: http.Server = http.createServer(app);
const routes: Array<CommonRoutesConfig> = [];
const debugLog: debug.IDebugger = debug('app');

app.use(express.json());

app.use(cors());

const loggerOptions: expressWinston.LoggerOptions = {
    transports: [new winston.transports.Console()],
    format: winston.format.combine(
        winston.format.json(),
        winston.format.prettyPrint(),
        winston.format.colorize({ all: true })
    ),
};

if (!process.env.DEBUG) {
    loggerOptions.meta = false;
}

app.use(expressWinston.logger(loggerOptions));

routes.push(new PortfolioRoutes(app));

const specs = swaggerJsdoc(ApplicationOptions.options);
app.use(
    "/swagger",
    swaggerUi.serve,
    swaggerUi.setup(specs)
  );

const runningMessage = `Server running at http://localhost:${ApplicationOptions.port}`;
app.get('/', (req: express.Request, res: express.Response) => {
    res.status(200).send(runningMessage)
});

initModels(connection);

const start = async () => {
    try {
        server.listen(ApplicationOptions.port, () => {
            routes.forEach((route: CommonRoutesConfig) => {
                debugLog(`Routes configured for ${route.getName()}`);
            });
            console.log(runningMessage);
        });
    } catch (error) {
        console.error(error);
    }
};

void start();