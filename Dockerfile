FROM microsoft/dotnet:2.0.3-sdk AS build-env
WORKDIR /app

ENV http_proxy http://172.24.203.241:3128
ENV https_proxy http://172.24.203.241:3128

# copy csproj and restore as distinct layers
COPY *.csproj ./
RUN dotnet restore -v d --disable-parallel

# copy everything else and build
COPY . ./
RUN dotnet publish -c Release -r linux-x64 -o out

# build runtime image
FROM microsoft/dotnet:2.0.3-runtime-deps
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["./snakeAiDnc"]