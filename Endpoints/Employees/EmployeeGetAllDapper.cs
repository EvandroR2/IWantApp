﻿using Dapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;

namespace IWantApp.Endpoints.Employees;

public class EmployeeGetAllDapper
{
    public static string Template => "/employeegetalldapper";

    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };

    public static Delegate Handle => Action;

    public static IResult Action(int? page, int? rows, IConfiguration configuration)
    {
        var db = new SqlConnection(configuration["ConnectionStrings:IWantDb"]);
        
        var query = @"select Email, ClaimValue as Name from AspNetUsers u 
              inner join AspNetUserClaims c on u.id = c.UserId and claimtype = 'Name'
              Order by name 
              OFFSET (@page - 1) * @rows ROWS FETCH NEXT @rows ROWS ONLY ";
        
        var employee = db.Query<EmployeeResponse>(query, new { page, rows });
        return Results.Ok(employee);
    }
}
