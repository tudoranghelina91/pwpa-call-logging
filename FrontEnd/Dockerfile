FROM node:alpine
WORKDIR /usr/src/app
COPY . /usr/src/app
RUN npm install -g @angular/cli
RUN npm install
RUN ng build -c production

FROM node:alpine
RUN npm install -g http-server
WORKDIR /usr/app
COPY --from=0 /usr/src/app/dist/pwpa-call-logging-frontend/browser/* /usr/app
COPY --from=0 /usr/src/app/certificates/* /usr/app
CMD ["npx", "http-server", ".", "-S", "-C", "cert.pem"]
