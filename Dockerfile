FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY publish .
RUN apt update && apt install -y curl
RUN addgroup --system --gid 1000 rumblegroup && adduser --system --uid 1000 --ingroup rumblegroup --shell /bin/sh rumbleuser
RUN chown -R rumbleuser:rumblegroup /app
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080
USER 1000
ENTRYPOINT ["dotnet", "calendar-service.dll"]
