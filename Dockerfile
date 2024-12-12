FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

COPY ./BeatStore.sln ./
COPY ./BeatStore_SoftUni/ ./BeatStore_SoftUni/
COPY ./BeatStore_SoftUni.Data/ ./BeatStore_SoftUni.Data/
COPY ./BeatStore_SoftUni.Data.Models/ ./BeatStore_SoftUni.Data.Models/
COPY ./BeatStore_SoftUni.Services.Data/ ./BeatStore_SoftUni.Services.Data/
COPY ./BeatStore_SoftUni.ViewModels/ ./BeatStore_SoftUni.ViewModels/
COPY ./BeatStore_SoftUni.Common/ ./BeatStore_SoftUni.Common/

RUN dotnet restore

RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build-env /app/out ./

EXPOSE 5000
EXPOSE 5001

ENV ASPNETCORE_ENVIRONMENT=Production

ENTRYPOINT ["dotnet", "BeatStore_SoftUni.dll"]