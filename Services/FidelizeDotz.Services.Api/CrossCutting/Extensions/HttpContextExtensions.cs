using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using FidelizeDotz.Services.Api.Domain.Application.Dtos;
using Microsoft.AspNetCore.Http;

namespace FidelizeDotz.Services.Api.CrossCutting.Extensions
{
    public static class HttpContextExtensions
    {
        public static UserLogged GetUserLogged(
            this HttpContext httpContext)
        {
            string forwarded;
            if (httpContext == null)
                forwarded = null;
            else
                forwarded = httpContext.Request?.Headers["X-Forwarded-For"].FirstOrDefault();

            var userId = httpContext?.User?.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value ?? "";
            Guid? id = null;
            if (!userId.IsNullOrWhiteSpace()) id = new Guid(userId);
            var name = httpContext?.User?.FindFirst("name")?.Value;
            var email = httpContext?.User?.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress")?.Value;
            var isAuthenticated = httpContext?.User?.Identity?.IsAuthenticated;
            var remoteIpAddress = forwarded ?? httpContext?.Connection?.RemoteIpAddress?.MapToIPv4()?.ToString();
            var username = httpContext?.User?.FindFirst("preferred_username")?.Value;
            return new UserLogged(id, name, email, isAuthenticated ?? false, remoteIpAddress, username);
        }
    }
}