using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace WebApi_ApiKeyAuthentication.Attributes
{
    //This sets the Attribute usage to Classes and Methods
    [AttributeUsage( AttributeTargets.Class | AttributeTargets.Method)]
    
    //Extends Attribute and an async filter that is detected upon usage (I think)
    public class ApiKeyAuth : Attribute, IAsyncActionFilter
    {
        //API header name
        private const string ApiKeyAuthHeaderName = "API_KEY";
        
        //On Action
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            //Checks if the header exists if so outs the user key.
            if (!context.HttpContext.Request.Headers.TryGetValue(ApiKeyAuthHeaderName, out var userKey))
            {
                //Return Unauthorized Result if theres no header in the request
                context.Result = new UnauthorizedResult();
                return;
            }

            //Gets the configuration
            var configuration = context.HttpContext.RequestServices.GetRequiredService<IConfiguration>();
            //Gets the value of the key in the header
            var apiKey = configuration.GetValue<string>(ApiKeyAuthHeaderName);

            //Checks if the key matches.
            //You can check if it exists in the DB.
            //After that you can check who is logging in.
            if (!userKey.Equals("API_KEY"))
            {
                //If the key doesnt exist. Return Unauthorized.
                context.Result = new UnauthorizedResult();
                return;
            }

            await next();
        }
    }
}