using IdentityServer4.Models;
using IdentityServer4.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SmileBoy.Server.Infrastructure
{
    public class ResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            try
            {
                if (context.UserName == "Admin")
                {
                    //check if password match - remember to hash password if stored as hash in db

                    //set the result
                    context.Result = new GrantValidationResult(
                        subject: context.UserName,
                        authenticationMethod: "custom",
                        claims: GetUserClaims(context.UserName));

                    return;
                }

                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "User does not exist.");
                return;
            }
            catch
            {
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "Invalid username or password.");
            }
        }

        //create claims array from user data
        private static Claim[] GetUserClaims(string userName)
        {
            return new Claim[]
            {
                new Claim("sub", userName)
            };
        }
    }
}
