// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using Microsoft.AspNetCore.Mvc;
// using ksp_portal.Models;
// using ksp_portal.Services;

// namespace ksp_portal.Controllers
// {
//     [Route("api/[controller]")]
//     [ApiController]
//     public class ValuesController : ControllerBase
//     {
//         private WorkoutService _workoutService;
//         public ValuesController(WorkoutService service)
//         {
//             _workoutService = service;
//         }
//         // public IConfiguration Configuration { get; set; }
//         // public ValuesController(IConfiguration config) {
//         //     Configuration = config;
//         // }
//         // GET api/values
//         [HttpGet]
//         public ActionResult<string> Get()
//         {
            
//             return _workoutService.Get();
//             // var test = Configuration["db_username"];
//             //return new string[] { "Hello World one", "Hello World 2" };
//         }
//         // [HttpGet]
//         // public ActionResult<IEnumerable<string>> Get()
//         // {
//         //     // var test = Configuration["db_username"];
//         //     return new string[] { "Hello World one", "Hello World 2" };
//         // }

//         // GET api/values/5
//         [HttpGet("{id}")]
//         public ActionResult<string> Get(int id)
//         {
//             return "value";
//         }

//         // POST api/values
//         [HttpPost]
//         public void Post([FromBody] string value)
//         { }

//         // PUT api/values/5
//         [HttpPut("{id}")]
//         public void Put(int id, [FromBody] string value)
//         { }

//         // DELETE api/values/5
//         [HttpDelete("{id}")]
//         public void Delete(int id)
//         { }
//     }
// }