# snake-ai-dotnetcore

containerized [snake-ai](https://github.com/murugaratham/snake-ai)


`git clone https://github.com/murugaratham/snake-ai-dotnetcore.git`

## Without Docker
assuming you have .net core ^2.0.3 installed
### Run one of the following, depending on your platform

```
dotnet publish -c Release -r linux-x64 -o out
dotnet publish -c Release -r osx-x64 -o out
dotnet publish -c Release -r win-x64 -o out

dotnet snakeAiDnc.dll
```

```
Hosting environment: Production
Content root path: D:\git\snake-ai-dotnetcore\bin\Debug\netcoreapp2.0/snake-ai
Now listening on: http://0.0.0.0:5000
Application started. Press Ctrl+C to shut down.
```

## With Docker

```
docker build -t mysmartsnake .
docker run -d -p 80:5000 --name "some name" mysmartsnake
```

I am using linux containers on windows 10.

The dockerfile is created to use `microsoft/dotnet:2.0.3-sdk` image as a build environment, then copies all the binaries to another image that is only loaded with runtime dependencies `microsoft/dotnet:2.0.3-runtime-deps`. 
Essentially a [self-contained application](https://docs.microsoft.com/en-us/dotnet/core/deploying/#self-contained-deployments-scd)

All public microsoft docker images can be found [here](https://hub.docker.com/r/microsoft/dotnet/)

```
FROM microsoft/dotnet:2.0.3-sdk AS build-env
WORKDIR /app

# copy csproj and restore as distinct layers
COPY *.csproj ./
RUN dotnet restore -v d --disable-parallel <-- my network is choppy, remove this switch to make it faster

# copy everything else and build
COPY . ./
RUN dotnet publish -c Release -r linux-x64 -o out

# build runtime image
FROM microsoft/dotnet:2.0.3-runtime-deps
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["./snakeAiDnc"]
```

## I'm using [cntlm](http://cntlm.sourceforge.net)

update or remote it before trying out the docker

ENV http_proxy http://172.24.203.241:3128
ENV https_proxy http://172.24.203.241:3128

## SSL Connect error during dotnet restore

If you faced SSL Connect error during dotnet restore like i did, very likely it's due to MTU size of container is different from host [issue](https://github.com/moby/moby/issues/22297)

add this to your daemon.json
```
"mtu": 1400
```


