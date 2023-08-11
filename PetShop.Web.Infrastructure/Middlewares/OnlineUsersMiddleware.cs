namespace PetShop.Web.Infrastructure.Middlewares
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Caching.Memory;
    using PetShop.Web.Infrastructure.Extensions;
    using System.Collections.Concurrent;
    using static PetShop.Common.GeneralApplicationConstants;
    public class OnlineUsersMiddleware
    {
        private readonly RequestDelegate next;
        private readonly string cookieName;
        private readonly int lastActivityMinutes;

        private static readonly ConcurrentDictionary<string, bool> AllKeys = new ConcurrentDictionary<string, bool>();

        public OnlineUsersMiddleware(RequestDelegate next, string cookieName=OnlineUsersCookieName, int lastActivityMinutes=LastActivityBeforeOfflineMinutes)
        {
            this.next = next;
            this.cookieName = cookieName;
            this.lastActivityMinutes = lastActivityMinutes;
        }


        public Task InvokeAsync(HttpContext context, IMemoryCache memoryCache)
        {
            if(context.User.Identity?.IsAuthenticated ?? false)
            {
                if(!context.Request.Cookies.TryGetValue(this.cookieName, out string userId))
                {
                    userId = context.User.GetId()!;

                    context.Response.Cookies.Append(this.cookieName, userId,
                       new CookieOptions()
                       {
                           HttpOnly = true,
                           MaxAge = TimeSpan.FromDays(30)
                       });

                }

                memoryCache.GetOrCreate(userId, cacheEnty =>
                {
                    if (!AllKeys.TryAdd(userId, true))
                    {
                        cacheEnty.AbsoluteExpiration = DateTimeOffset.MinValue;
                    }
                    else
                    {
                        cacheEnty.SlidingExpiration = TimeSpan.FromMinutes(this.lastActivityMinutes);
                        cacheEnty.RegisterPostEvictionCallback(this.RemoveKeyWhenExpired);
                    }

                    return string.Empty;
                });
            }
            else
            {
                if(context.Request.Cookies.TryGetValue(this.cookieName, out string userId))
                {
                    if(!AllKeys.TryRemove(userId, out _))
                    {
                        AllKeys.TryUpdate(userId, false, true);
                    }

                    context.Response.Cookies.Delete(this.cookieName);
                }
            }

            return this.next(context);
        }
        public static bool CheckIfUserIsOnline(string userId)
        {
            bool valueTaken = AllKeys.TryGetValue(userId.ToLower(), out bool success);

            return success && valueTaken;
        }
        private void RemoveKeyWhenExpired(object key, object value, EvictionReason reason, object state)
        {
            string keyStr = (string)key;

            if(!AllKeys.TryRemove(keyStr, out _))
            {
                AllKeys.TryUpdate(keyStr, false, true);
            }
        }

    }
}
