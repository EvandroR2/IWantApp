using IWantApp.Domain.Produtcs;
using IWantApp.infra.Data;
using Microsoft.AspNetCore.Mvc;

namespace IWantApp.Endpoints.Categories;

public class CategoryDelete
{
    public static string Template => "/categories/{id}";

    public static string[] Methods => new string[] { HttpMethod.Delete.ToString() };

    public static Delegate Handle => Action;

    public static IResult Action([FromRoute] Guid Id, ApplicationDbContext context)
    {
        var categories = context.Categories.FirstOrDefault();
        
        if(categories != null)
        {
            context.Remove(categories);
            context.SaveChanges();
            return Results.Ok(categories);
        }
        
        return Results.NotFound();
    }
}
