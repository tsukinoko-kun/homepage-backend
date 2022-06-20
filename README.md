## Required Software

.NET 6 SDK or Visual Studio (.NET for Web, F#)

## Build

```
dotnet publish ./homepage-backend.sln --arch x64 --configuration Release --output ./bin/
```

Output directory is `./bin`

## Environment Variables

- `MAILGUN_API_KEY` Mailgun API Key
- `MAILGUN_DOMAIN_NAME` Mailgun API Domain
