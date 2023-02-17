export class Asset {
    id: number;
    portfolioId: number;
    currencyId: number;
    amount: number;

    constructor(id: number, portfolioId: number, amount: number, currencyId: number) {
        this.id = id;
        this.portfolioId = portfolioId;
        this.amount = amount
        this.currencyId = currencyId;
    }
}