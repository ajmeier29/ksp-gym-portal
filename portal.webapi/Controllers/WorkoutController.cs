using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ksp_portal.Models;
using ksp_portal.Services;
using Microsoft.AspNetCore.Http.Internal;

namespace ksp_portal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkoutController : ControllerBase
    {
        private WorkoutService _workoutService;

        public WorkoutController(WorkoutService service)
        {
            _workoutService = service;
        }
        // GET api/workout
        [HttpGet]
        public ActionResult<List<Workout>> Get()
        {
            return _workoutService.Get();
        }
        // GET api/workout/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/workout
        [HttpPost]
        // [EnableBodyRewind]
        public IActionResult SomeAction([FromBody]Workout model)
        {
            var test = model;
            // play the body string again
            return null;
        }
        // PUT api/workout/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        { }

        // DELETE api/workout/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        { }
    }
}