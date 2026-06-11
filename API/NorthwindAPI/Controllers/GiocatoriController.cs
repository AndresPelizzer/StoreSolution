using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace NorthwindAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GiocatoriController : ControllerBase
    {
        private string connString = "Server=MTSWEBTEST\\SQLTEST;Database=Campionato;User Id=sa;Password=Mts.2010;TrustServerCertificate=true;";

        [HttpGet]
        public ActionResult<IEnumerable<Giocatore>> Get()
        {
            var result = new List<Giocatore>();

            using SqlConnection connection = new SqlConnection(connString);
            connection.Open();

            string sqlCommandText = "SELECT Codice, Name, Username, Email FROM dbo.Giocatori";
            SqlCommand command = new SqlCommand(sqlCommandText, connection);

            using SqlDataReader dr = command.ExecuteReader();

            while (dr.Read())
            {
                var giocatore = new Giocatore
                {
                    Codice = (int)dr["Codice"],
                    Name = dr["Name"] != DBNull.Value ? (string)dr["Name"] : string.Empty,
                    Username = dr["Username"] != DBNull.Value ? (string)dr["Username"] : string.Empty,
                    Email = dr["Email"] != DBNull.Value ? (string)dr["Email"] : string.Empty
                };

                result.Add(giocatore);
            }

            return Ok(result);
        }

        [HttpGet("{id}")]
        public ActionResult<Giocatore> GetById(int id)
        {
            using SqlConnection connection = new SqlConnection(connString);
            connection.Open();

            string sqlCommandText = "SELECT Codice, Name, Username, Email FROM dbo.Giocatori WHERE Codice = @Codice";
            SqlCommand command = new SqlCommand(sqlCommandText, connection);
            command.Parameters.AddWithValue("@Codice", id);

            using SqlDataReader dr = command.ExecuteReader();

            if (dr.Read())
            {
                var giocatore = new Giocatore
                {
                    Codice = (int)dr["Codice"],
                    Name = dr["Name"] != DBNull.Value ? (string)dr["Name"] : string.Empty,
                    Username = dr["Username"] != DBNull.Value ? (string)dr["Username"] : string.Empty,
                    Email = dr["Email"] != DBNull.Value ? (string)dr["Email"] : string.Empty
                };
                return Ok(giocatore);
            }

            return NotFound();
        }


        [HttpPost]
        public IActionResult Post([FromBody] Giocatore nuovoGiocatore)
        {
            using SqlConnection connection = new SqlConnection(connString);
            connection.Open();

            string sqlCommandText = "INSERT INTO dbo.Giocatori (Name, Username, Email) VALUES (@Name, @Username, @Email)";
            SqlCommand command = new SqlCommand(sqlCommandText, connection);

            command.Parameters.AddWithValue("@Name", nuovoGiocatore.Name ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@Username", nuovoGiocatore.Username ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@Email", nuovoGiocatore.Email ?? (object)DBNull.Value);

            command.ExecuteNonQuery();

            return Ok();
        }

        
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Giocatore giocatoreModificato)
        {
            using SqlConnection connection = new SqlConnection(connString);
            connection.Open();

            string sqlCommandText = "UPDATE dbo.Giocatori SET Name = @Name, Username = @Username, Email = @Email WHERE Codice = @Codice";
            SqlCommand command = new SqlCommand(sqlCommandText, connection);

            command.Parameters.AddWithValue("@Codice", id);
            command.Parameters.AddWithValue("@Name", giocatoreModificato.Name ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@Username", giocatoreModificato.Username ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@Email", giocatoreModificato.Email ?? (object)DBNull.Value);

            int rowsAffected = command.ExecuteNonQuery();

            if (rowsAffected == 0)
            {
                return NotFound();
            }

            return Ok();
        }

       
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            using SqlConnection connection = new SqlConnection(connString);
            connection.Open();

            string sqlCommandText = "DELETE FROM dbo.Giocatori WHERE Codice = @Codice";
            SqlCommand command = new SqlCommand(sqlCommandText, connection);

            command.Parameters.AddWithValue("@Codice", id);

            int rowsAffected = command.ExecuteNonQuery();

            if (rowsAffected == 0)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}