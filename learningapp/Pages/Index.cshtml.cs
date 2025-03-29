using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;


namespace learningapp.Pages;

public class IndexModel : PageModel
{
     public List<Course> Courses=new List<Course>();
    private readonly ILogger<IndexModel> _logger;
    private IConfiguration _configuration;
    public IndexModel(ILogger<IndexModel> logger,IConfiguration configuration)
    {
        _logger = logger;
        _configuration=configuration;
    }

    public void OnGet()
    {
       
        string connectionString = _configuration.GetConnectionString("MySQLConnectionString");
        var sqlConnection = new MySqlConnection(connectionString);
        sqlConnection.Open();

        var sqlcommand = new MySqlCommand(
        "SELECT CourseID,CourseName,Rating FROM Course;",sqlConnection);
         using (MySqlDataReader sqlDatareader = sqlcommand.ExecuteReader())
         {
             while (sqlDatareader.Read())
                {
                    Courses.Add(new Course() {CourseID=Int32.Parse(sqlDatareader["CourseID"].ToString()),
                    CourseName=sqlDatareader["CourseName"].ToString(),
                    Rating=Decimal.Parse(sqlDatareader["Rating"].ToString())});
                }
         }
    }
}
