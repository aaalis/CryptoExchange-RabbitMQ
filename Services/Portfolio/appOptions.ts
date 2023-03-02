class ApplicationOptions {
    
    public port: number = 8083;
    public options: object = {
        definition: {
          openapi: "3.0.0",
          info: {
            title: "Express API with Swagger",
            version: "0.1.0",
            description:
              "This is CRUD API application made with Express and documented with Swagger",
          },
          servers: [
            {
              url: `http://localhost:${this.port}`,
            },
          ],
        },
        apis: [`./dist/routes/portfolio.routes.js`],
    };   
}

export default new ApplicationOptions();