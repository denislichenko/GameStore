FROM node:12-alpine as build

ENV PROJECT_DIR /app

RUN mkdir -p ${PROJECT_DIR}

WORKDIR ${PROJECT_DIR}

COPY package*.json ./

RUN npm install

COPY . ${PROJECT_DIR}

RUN rm -f .npmrc
RUN npm run build

FROM nginx:stable-alpine

COPY --from=build /app/build /usr/share/nginx/html
COPY nginx/nginx.conf /etc/nginx/conf.d/default.conf

EXPOSE 80

CMD ["nginx", "-g", "daemon off;"]
