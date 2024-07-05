//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc;
//using ScreenSound.API.Requests;
//using ScreenSound.API.Responses;
//using ScreenSound.Data;
//using ScreenSound.Models;
//using ScreenSound.Shared.Data.Models;

//namespace ScreenSound.API.Endpoints
//{
//    public static class AuthExtensions
//    {
//        public static void AddEndpointsAuth(this WebApplication app)
//        {
//            app.MapGroup("auth").MapIdentityApi<PessoaAcesso>().WithTags("Autorização");

//            var groupBuilder = app.MapGroup("auth")
//                .RequireAuthorization()
//                .WithTags("Autorização");

//            groupBuilder.MapPost("logout", async ([FromServices] SignInManager<PessoaAcesso> signInManager) =>
//            {
//                await signInManager.SignOutAsync();
//                return Results.Ok();
//            }).RequireAuthorization().WithTags("Autorização");

//        }
//    }
//}
