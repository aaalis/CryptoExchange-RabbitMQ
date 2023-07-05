FROM node:18-alpine

RUN npm i -g pnpm

WORKDIR /app

COPY /Services/Portfolio/package.json /Services/Portfolio/pnpm-lock.yaml ./

RUN pnpm install

# RUN npm install

# FROM base as build

# WORKDIR /app
COPY /Services/Portfolio/. .
# COPY --from=dependencies /app/node_modules ./node_modules
# RUN pnpm start
# RUN pnpm prune --prod

# FROM base as deploy

# WORKDIR /app
# COPY --from=build /app/dist/ ./dist/
# COPY --from=build /app/node_modules ./node_modules

EXPOSE 8083 5672

# CMD ["node", "dist/app.js"]

CMD npm start