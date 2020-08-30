using System;
using Microsoft.AspNetCore.Http;

namespace AzWebPlayGround.Domain
{
    public static class NamingValues
    {
        public const string SYMM_KEY = "06037033-dc3f-4f1e-aa6e-3e15f2ad1f4a";
        public const string JWT_BEARER_COOKIE_NAME = "jwt-bearer";
        public const string XSRF_PUBLIC_KEY_COOKIE_NAME = "xsrf-k";
        public const string XSRF_PRIVATE_KEY_COOKIE_NAME = "xsrf-pk";

        public static readonly TimeSpan tokenExpiryTime = TimeSpan.FromMinutes(2);
    }
}