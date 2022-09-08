#Stage 1
FROM node:latest as build-step


RUN mkdir -p /app

WORKDIR /app

COPY package.json /app

RUN npm install

RUN npx browserslist --update-db

COPY . /app

RUN npm run build --omit=dev
# Stage 2
FROM nginx:latest

COPY --from=build-step /app/dist/client /usr/share/nginx/html

EXPOSE 80
