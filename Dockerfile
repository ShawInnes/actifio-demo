FROM microsoft/dotnet:2.2-sdk as build
WORKDIR /usr/src/app
COPY ./DemoApp.Web /usr/src/app
RUN dotnet restore
RUN dotnet publish -c Release -o out DemoApp.Web.csproj

FROM microsoft/dotnet:2.2-aspnetcore-runtime
WORKDIR /app
COPY --from=build /usr/src/app/out .
ENTRYPOINT ["dotnet", "DemoApp.Web.dll"]

