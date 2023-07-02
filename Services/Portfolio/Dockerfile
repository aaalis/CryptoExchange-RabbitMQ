FROM node:18.14 As base

RUN npm i -g pnpm

FROM base AS dependencies
WORKDIR /app
COPY package.json ./
RUN pnpm install

FROM base as build
WORKDIR /app
COPY . .
COPY --from=dependencies /app/node_modules ./node_modules
RUN pnpm build
RUN pnpm prune --prod

FROM base as deploy

WORKDIR /app
COPY --from=build /app/dist/ ./dist/
COPY --from=build /app/node_modules ./node_modules

EXPOSE 8083

CMD ["npm", "start"]