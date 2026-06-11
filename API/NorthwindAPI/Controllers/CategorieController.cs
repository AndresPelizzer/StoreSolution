using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NorthwindAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategorieController : ControllerBase
    {
        // GET: api/<CategorieController>

        private string connString = "Server=MTSWEBTEST\\SQLTEST;Database=Northwind;User Id=sa;Password=Mts.2010;TrustServerCertificate=true;";

        [HttpGet]
        public IEnumerable<Categoria> Get()
        {
            //return new string[] { "value1", "value2" };


            var result = new List<Categoria>();

            using SqlConnection connection = new SqlConnection(connString);

            //string sql = @"SELECT CategoryID, CategoryName, Description
            //       FROM Categories
            //       ORDER BY CategoryID";

            //SqlDataAdapter da = new SqlDataAdapter(sql, connection);

            //DataTable dt = new DataTable();
            //da.Fill(dt);

            if (connection.State == System.Data.ConnectionState.Closed)
            {
                connection.Open();
            }

            string sqlCommandText = "SELECT * FROM Categories";
            SqlCommand command = new SqlCommand(sqlCommandText, connection);

            SqlDataReader dr = command.ExecuteReader();
            
            if (dr.HasRows)
            {

                while (dr.Read())
                {


                    var categoria = new Categoria
                    {
                        CategoryID = (int)dr[0],
                        CategoryName = (string)dr[1],
                        Description= (string)dr[2]
                    };


                    result.Add(categoria);

                }

            }

            connection.Close();



            return result;

        }

        // GET api/<CategorieController>/5
        [HttpGet("{id}")]
        public ActionResult<Categoria> Get(int id)
        {
            
            using SqlConnection connection= new SqlConnection(connString);
            if (connection.State == System.Data.ConnectionState.Closed)
            {
                connection.Open();
            }
            string sqlCommandText = "SELECT CategoryID, CategoryName, Description FROM Categories WHERE CategoryID = @id";
            SqlCommand command= new SqlCommand(sqlCommandText, connection);
            command.Parameters.AddWithValue("@id", id);
            using SqlDataReader dr = command.ExecuteReader();
        
            if (dr.Read())
            {
                
               
                    var categoria = new Categoria
                    {
                        CategoryID = (int)dr[0],
                        CategoryName = (string)dr[1],
                        Description = (string)dr[2]

                    };
                    return Ok(categoria);
                }

            return NotFound($"Categoria con @id non trovata");
            
        }




        // POST api/<CategorieController>
        [HttpPost]
        public OkObjectResult Post([FromBody] Categoria nuovaCategoria)
        {

            using SqlConnection connection = new SqlConnection(connString);
            if (connection.State == System.Data.ConnectionState.Closed)
            {
                connection.Open();
            }
            string sqlCommandText = "INSERT INTO Categories(CategoryName, Description) VALUES (@name,@description) ;";
            SqlCommand command = new SqlCommand(sqlCommandText, connection);
            command.Parameters.AddWithValue("@name", nuovaCategoria.CategoryName);
            command.Parameters.AddWithValue("@description", nuovaCategoria.Description );



            command.ExecuteNonQuery();
            return Ok("Categoria inserita con successo!");
        }
        // PUT api/<CategorieController>/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Categoria categoriaAggiornata)

        {
            using SqlConnection connection = new SqlConnection(connString);
            if (connection.State == System.Data.ConnectionState.Closed)
            {
                connection.Open();
            }
            string sqlCommandText = "UPDATE Categories SET CategoryName=@name, Description=@desc WHERE CategoryID=@id;";
            SqlCommand command = new SqlCommand(sqlCommandText, connection);
            command.Parameters.AddWithValue("@name", categoriaAggiornata.CategoryName);
            command.Parameters.AddWithValue("@desc", categoriaAggiornata.Description);
            command.Parameters.AddWithValue("@id", id);

            command.ExecuteNonQuery();
            return Ok("Categoria aggiornata con successo!");
        }


        

        // DELETE api/<CategorieController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            using SqlConnection connection = new SqlConnection(connString);
            if (connection.State == System.Data.ConnectionState.Closed)
            {
                connection.Open();
            }
            string sqlCommandText = "DELETE FROM Categories WHERE CategoryID=@id";
            SqlCommand command = new SqlCommand(sqlCommandText, connection);
            command.Parameters.AddWithValue("@id", id);
            command.ExecuteNonQuery();
            return Ok($"Categoria con ID {id} eliminata con successo.");
        }
    }
}
