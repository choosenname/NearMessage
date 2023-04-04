using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ASPCoreServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Messanger1Controller : ControllerBase
    {
        static List<Message> ListOfMessages = new List<Message>();

        /*// GET: api/<Messanger>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }*/

        // GET api/<Messanger>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            string OutputString = "Not found";
            if (id < ListOfMessages.Count && id >= 0)
            {
                OutputString = JsonConvert.SerializeObject(ListOfMessages[id]);
            }
            Console.WriteLine($"Запрошено сообщение № {id} : {OutputString}");
            return OutputString;
        }

        // POST api/<Messanger>
        [HttpPost]
        public IActionResult Post([FromBody] Message value)
        {
            if (value == null) return BadRequest();

            ListOfMessages.Add(value);
            Console.WriteLine($"Всего сообщений: {ListOfMessages.Count} Посланное сообщение: {value}");
            //return new NoContentResult();
            return new OkResult();
        }

        /*// PUT api/<Messanger>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }*/

        /*// DELETE api/<Messanger>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }*/
    }
}
