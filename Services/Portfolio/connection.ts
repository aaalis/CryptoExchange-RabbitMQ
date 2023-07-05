import { Sequelize } from "sequelize";

const connection = new Sequelize({
    dialect:"postgres",
    host:"db", //CONTAINER_NAME database
    username: "postgres",
    password: "postgres",
    database: "DB",
    port: 5432,
    logging: false
});

export default connection;