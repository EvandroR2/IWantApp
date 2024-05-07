using IWantApp.Domain.Produtcs;
using IWantApp.infra.Data;
using Microsoft.AspNetCore.Mvc;

namespace IWantApp.Endpoints.Categories;

public class CategoryGet
{
    public static string Template => "/categories/{id:guid}";

    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };

    public static Delegate Handle => Action;

    public static IResult Action([FromRoute] Guid Id, ApplicationDbContext context)
    {
        var categories = context.Categories.FirstOrDefault();
        var response = categories;
        if(response != null)
        {
            return Results.Ok(response);
        }

        return Results.NotFound();
    }
}
