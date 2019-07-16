FROM mcr.microsoft.com/dotnet/core/sdk:2.2
WORKDIR /app
COPY /app /app
ENTRYPOINT ["dotnet", "ksp-portal.dll"]