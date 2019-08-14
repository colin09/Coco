FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /app
COPY . ./
RUN ls
RUN dotnet restore -s http://127.0.0.1:9400 -s https://www.nuget.org/api/v2/
RUN dotnet publish -o out
#RUN dotnet publish -c Release -o out

FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
EXPOSE 80
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT ["dotnet", "com.d.site.dll"]
