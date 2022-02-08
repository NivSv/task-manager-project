FROM mcr.microsoft.com/mssql/server

USER root

RUN apt-get -y update && apt-get install -y dos2unix

RUN mkdir -p /usr/src/app
WORKDIR /usr/src/app

COPY . /usr/src/app

RUN dos2unix *

RUN chmod +x /usr/src/app/init_db.sh

USER mssql
ENTRYPOINT /bin/bash ./entrypoint.sh