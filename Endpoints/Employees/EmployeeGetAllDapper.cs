using IWantApp.infra.Data;

namespace IWantApp.Endpoints.Employees;

public class EmployeeGetAllDapper
{
    public static string Template => "/employeegetalldapper";

    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };

    public static Delegate Handle => Action;

    public static async Task<IResult> Action(int? page, int? rows, QueryAllUsersWithClaimName query)
    {
        var result = await query.Execute(page.Value, rows.Value);
        return Results.Ok(result);
    }
}
