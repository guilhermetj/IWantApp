using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Data.SqlClient;

namespace IWantApp.Endpoints.Employees;

public class EmployeeGetAll
{
    public static string Template => "/employees";
    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };

    public static Delegate Handle => Action;

    [Authorize(Policy = "Employee0042Policy")]
    public static async Task<IResult> Action(int? page, int? rows, IConfiguration configuration)
    {

        var db = new SqlConnection(configuration.GetConnectionString("Default"));
        var query =
            @"select Email, ClaimValue as Name
                from AspNetUsers u inner 
                join AspNetUserClaims c 
                on u.id = c.UserId and claimtype = 'Name'
                order by name
                OFFSET (@page -1) * @rows ROWS FETCH NEXT @rows ROWS ONLY";

        var employees = await db.QueryAsync<EmployeeResponse>(
            query,
            new { page,rows}
            );
        return Results.Ok(employees);
    }
}
