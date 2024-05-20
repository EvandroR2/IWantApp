using IWantApp.Endpoints.Employees;
using IWantApp.Endpoints.Token;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace IWantApp.Endpoints.Security;

public class TokenPost
{
    public static string Template => "/token";

    public static string[] Methods => new string[] { HttpMethod.Post.ToString() };

    public static Delegate Handle => Action;


    [AllowAnonymous]
    public static IResult Action(LoginRequest loginRequest, IConfiguration configuration ,UserManager<IdentityUser> userManager)
    {
        var user = userManager.FindByEmailAsync(loginRequest.Email).Result;
        if (user == null)
            Results.BadRequest();
        if (!userManager.CheckPasswordAsync(user , loginRequest.Password).Result)
            Results.BadRequest();

        var claims = userManager.GetClaimsAsync(user).Result;
        var subjet = new ClaimsIdentity(new Claim[]
        {
            new Claim(ClaimTypes.Email, loginRequest.Email),
            new Claim(ClaimTypes.NameIdentifier, user.Id),
        });
        subjet.AddClaims(claims);

        var key = Encoding.ASCII.GetBytes(configuration["jwtBeareTokenSettings:SecretKey"]);
        //var key = Encoding.ASCII.GetBytes("A@fderwFQQSDXCCer34A@fderwFQQSDXCCer34");

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = subjet,
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            Audience = configuration["jwtBeareTokenSettings:Audience"], 
            Issuer = configuration["jwtBeareTokenSettings:Issuer"],
            Expires = DateTime.UtcNow.AddSeconds(30)
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return Results.Ok(new
        {
            token = tokenHandler.WriteToken(token)
        });
    }
}


