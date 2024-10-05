using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace learningapp.Pages;

public class IndexModel : PageModel
{
     public List<Course> Courses=new List<Course>();
    public System.Collections.Generic.List<Customer> customers = new List<Customer>();  
     
    private readonly ILogger<IndexModel> _logger;
    private IConfiguration _configuration;
    public IndexModel(ILogger<IndexModel> logger,IConfiguration configuration)
    {
        _logger = logger;
        _configuration=configuration;
    }

    public void OnGet()
    {
       
        string connectionString = _configuration.GetConnectionString("AZURE_SQL_CONNECTIONSTRING")!;
        var sqlConnection = new SqlConnection(connectionString);
        sqlConnection.Open();

        //var sqlcommand = new SqlCommand(
        //"SELECT CourseID,CourseName,Rating FROM Course;",sqlConnection);

        var sqlCommand = new SqlCommand("select CustomerId, FirstName + LastName as CustomerName, " +
            "CompanyName from [SalesLT].[Customer];", sqlConnection);

         using (SqlDataReader sqlDatareader = sqlCommand.ExecuteReader())
         {
             while (sqlDatareader.Read())
                {
                    // Courses.Add(new Course() {CourseID=Int32.Parse(sqlDatareader["CourseID"].ToString()),
                    // CourseName=sqlDatareader["CourseName"].ToString(),
                    // Rating=Decimal.Parse(sqlDatareader["Rating"].ToString())});

                    customers.Add(new Customer()
                    {
                        CustomeID = Int32.Parse(sqlDatareader["CustomerId"].ToString()),
                        CustomerName = sqlDatareader["CustomerName"].ToString(),
                        CompanyName = sqlDatareader["CompanyName"].ToString()
                    });
                }
         }
    }
}
