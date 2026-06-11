using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NorthwindAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        
        private List<Giocatore> giocatori= new List<Giocatore>();
        
        public ValuesController() {

            giocatori.Add(new Giocatore
            {
                Codice = 1,
                Nome = "mario",
                Eta = 15
            });

            giocatori.Add(new Giocatore
            {
                Codice = 2,
                Nome = "Franco",
                Eta = 18
            });


        }




        [HttpGet]
        public IEnumerable<Giocatore> Get()
        {
            //return new string[] { "value1", "value2" };
            return giocatori;
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public Giocatore Get(int id)
        {
            //return "value";
            var item = giocatori.FirstOrDefault(g => g.Codice == id);
            if (item == null)
            {

                return new();
            }
            else
            {
                return item;
            }


        }

        // POST api/<ValuesController>
        [HttpPost]
        public void Post([FromBody] Giocatore item)
        {

            giocatori.Add(item);

        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {


        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var item = giocatori.FirstOrDefault(g => g.Codice == id);
            if (item != null)
            {
                giocatori.Remove(item);
            }



        }
    }
}
