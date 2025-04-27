FROM mcr.microsoft.com/mssql/server:2022-latest

USER root

RUN apt-get update && \
    apt-get install -y curl gnupg && \
    curl https://packages.microsoft.com/keys/microsoft.asc | apt-key add - && \
    curl https://packages.microsoft.com/config/debian/10/prod.list > /etc/apt/sources.list.d/mssql-release.list && \
    apt-get update && \
    ACCEPT_EULA=Y apt-get install -y mssql-tools unixodbc-dev \
        -o Dpkg::Options::="--force-overwrite" \
        --allow-downgrades --allow-remove-essential --allow-change-held-packages && \
    ln -sfn /opt/mssql-tools/bin/sqlcmd /usr/bin/sqlcmd

ENV PATH="$PATH:/opt/mssql-tools/bin"
ENV ACCEPT_EULA=Y
