FROM node:alpine
ARG configuration=production
WORKDIR /usr/src/app
COPY . /usr/src/app
RUN apk add chromium
ENV CHROME_BIN=/usr/bin/chromium-browser
RUN npm install -g @angular/cli
RUN npm install
# RUN ng test --watch=false
RUN ng build -c $configuration

FROM node:alpine
RUN npm install -g http-server
WORKDIR /usr/app
COPY --from=0 /usr/src/app/dist/pwpa-call-logging-frontend/browser/* /usr/app
COPY --from=0 /usr/src/app/certificates/* /usr/app
CMD ["npx", "http-server", ".", "-S", "-C", "cert.pem"]
