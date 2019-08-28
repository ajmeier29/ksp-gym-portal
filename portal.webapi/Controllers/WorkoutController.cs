using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using portal.webapi.Models;
using portal.webapi.Services;
using Microsoft.AspNetCore.Http.Internal;
using MongoDB.Bson.Serialization;
using MongoDB.Bson;

namespace portal.webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkoutController : ControllerBase
    {
        private WorkoutService _workoutService;

        public WorkoutController(WorkoutService service)
        {
            _workoutService = service;
            // BsonClassMap.RegisterClassMap<Workout>();
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
            // var bsonObject = model.ToBsonDocument();
            _workoutService.InsertOneWorkout(model);
            // play the body string again
            return Ok(test);
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