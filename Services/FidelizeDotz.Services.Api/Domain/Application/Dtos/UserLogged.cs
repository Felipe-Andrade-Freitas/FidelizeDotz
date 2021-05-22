using System;

namespace FidelizeDotz.Services.Api.Domain.Application.Dtos
{
    public class UserLogged
    {
        public UserLogged(
            Guid? id,
            string name,
            string email,
            bool isAuthenticated,
            string remoteIpAddress,
            string username)
        {
            if(id != null) Id = id.Value;
            Name = name;
            Email = email;
            IsAuthenticated = isAuthenticated;
            RemoteIpAddress = remoteIpAddress;
            Username = username;
        }

        public Guid Id { get; }

        public string Name { get; }

        public string Username { get; }

        public string Email { get; }

        public bool IsAuthenticated { get; }

        public string RemoteIpAddress { get; }
    }
}