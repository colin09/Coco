

using Microsoft.AspNetCore.Mvc;

namespace com.fs.api.Controllers
{

    [Route("api/[controller]")]
    public class TodoController : Controller
    {

        /// <summary>
        /// get all xxxxxxx.
        /// </summary>
        [HttpGet]
        public string GetAll()
        {
            return "all is here";
        }



        [HttpGet("{id}")]
        public string GetOne(int id)
        {
            return "id is" + id;
        }


        [HttpGet("state/{state}")]
        public string GetOneByState(int state)
        {
            return "state is " + state;
        }

    }




}


