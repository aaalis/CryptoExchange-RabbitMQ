import { Asset } from "./asset";

export class Portfolio {
    id: number;
    userId: number;
    assets: Array<Asset>;

    constructor(id: number, userId: number, assets: Array<Asset>) {
        this.id = id;
        this.userId = userId;
        this.assets = assets;
    }
}