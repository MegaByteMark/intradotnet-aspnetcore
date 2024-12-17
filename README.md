# IntraDotNet.Windows.Server

ASP.NET Core polyfill for enabling Windows features when targeting a Windows intranet environment.

## Summary

This library provides middleware and authorization handlers to enable Windows-specific features in an ASP.NET Core application. It includes support for Windows impersonation and Windows group membership authorization, making it easier to integrate with existing Windows-based AD DS infrastructure in an intranet environment.

## Features

- Windows Impersonation Middleware
- Windows Group Membership Authorization

## Installation

To install the library, add the following NuGet package to your project:

```bash
dotnet add package IntraDotNet.Windows.Server
```

## Usage

### Windows Impersonation Middleware

To use the Windows Impersonation Middleware, add it to the middleware pipeline in the Program.cs file:
```csharp
using IntraDotNet.Windows.Server.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddAuthentication(NegotiateDefaults.AuthenticationScheme).AddNegotiate();
builder.Services.AddAuthorization();

var app = builder.Build();

app.UseAuthentication();

// Add after UseAuthentication, you must have already added Negotiate authentication before calling UseAuthentication.
app.UseMiddleware<WindowsImpersonationMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();
```

### Windows Group Membership Authorization

To use Windows Group Membership Authorization, configure the authorization policies in the appsettings.json file and Startup.cs file:

#### appsettings.json
```json
{
  "Authorization": {
    "Policies": [
      {
        "Name": "RequireWindowsGroup",
        "AllowedGroups": [ "DomainName\\GroupName" ]
      }
    ]
  }
}
```

#### Program.cs
```csharp
using IntraDotNet.Windows.Server.Authorization.WindowsGroupMembership.DependencyInjection;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

//You must add the configuration here before calling AddWindowsGroupMembershipAuthorization
builder.Services.Configure<WindowsGroupMembershipAuthorizationOptions>(
    builder.Configuration.GetSection("Authorization:WindowsGroupMembershipAuthorization"));

builder.Services.AddWindowsGroupMembershipAuthorization();

var app = builder.Build();

app.UseRouting();
app.UseAuthorization();
app.MapControllers();

app.Run();
```

In your controller, you can then use the policy to protect actions:

```csharp
[Authorize(Policy = "RequireWindowsGroup")]
public class SecureController : ControllerBase
{
    public IActionResult Get()
    {
        return Ok("This is a secure endpoint.");
    }
}
```

## Contributing

Contributions are welcome! Please open an issue or submit a pull request.

## License

This project is licensed under the MIT License.
