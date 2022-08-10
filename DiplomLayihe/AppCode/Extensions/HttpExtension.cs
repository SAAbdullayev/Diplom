using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DiplomLayihe.AppCodee.Extensions
{
    public static partial class Extension
    {
        
        public static string GetAppLink(this IActionContextAccessor ctx)
        {
            string scheme = ctx.ActionContext.HttpContext.Request.Scheme;
            string host = ctx.ActionContext.HttpContext.Request.Host.ToString();

            return $"{scheme}://{host}";
        }


        public static string GetCurrentCulture(this HttpContext ctx)
        {

            var match = Regex.Match(ctx.Request.Path, @"\/(?<lang>az|en|ru)\/?.*");

            if (match.Success)
            {
                return match.Groups["lang"].Value;
            }

            if (ctx.Request.Cookies.TryGetValue("lang", out string lang))
            {
                return lang;
            }

            return "az";
        }
    }
}
