using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using portal.webapi.Models;
using portal.webapi.Services;
using MongoDB.Driver;
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
        public async Task<ActionResult<Workout>> Get(string id)
        {
            Workout workout = await _workoutService.GetOneWorkoutAsync(id);
            return workout;
        }

        // POST api/workout
        [HttpPost]
        public async Task<IActionResult> SomeAction([FromBody]Workout model)
        {
            await _workoutService.InsertOneWorkoutAsync(model);
            return Ok(model);
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