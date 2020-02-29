using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using portal.webapi.Repository;
using portal.webapi.Models;
using portal.webapi.Services;
using MongoDB.Driver;
using Microsoft.AspNetCore.Http.Internal;
using MongoDB.Bson.Serialization;
using MongoDB.Bson;
using Microsoft.Extensions.Configuration;

namespace portal.webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkoutController : ControllerBase
    {
        // private RepositoryFactory _repositoryFactory {get;set;}
        private WorkoutService _workoutService { get; set; }
        private IRepository<Workout> _workoutRepository { get; set; }

        public WorkoutController(WorkoutService workoutService)
        {
            _workoutService = workoutService;
            _workoutRepository = _workoutService.GetRepository();
        }
        // This endpoint will return the proper workout to display
        // by filtering by the device id.
        // GET api/workout/getdeviceworkout/{device_id}
        [Route("[action]/{id}")]
        [HttpGet]
        public async Task<ActionResult<Workout>> GetDeviceWorkout(string id)
        {
            var workout = await _workoutRepository.GetDeviceWorkout(id);
            return workout;
        }

        // GET api/workout/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Workout>> Get(string id)
        {
            Workout workout = await _workoutRepository.GetOneByIdAsync(id);
            return workout;
        }

        [HttpGet]
        public ActionResult Get()
        {
            return Ok("Hello World");
        }

        // [HttpPost]
        // public async Task<DeleteResult> DeleteOneRecordAsync(string id)
        // {

        // }

        [Route("[action]/{limit}")]
        [HttpGet]
        // Get api/workout/GetLatestWorkoutsLimitAsync/2
        public async Task<ActionResult<List<Workout>>> GetLatestWorkoutsLimitAsync(int limit)
        {
            return await _workoutRepository.GetLatestAsync(limit);
        }

        // POST api/workout
        [HttpPost]
        public async Task<IActionResult> InsertNewWorkout([FromBody]Workout model)
        {
            await _workoutRepository.InsertOneAsync(model);
            return Ok(model);
        }
        // PUT api/workout/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        { }

        // DELETE api/workout/5
        [HttpDelete("{id}")]
        public async Task<DeleteResult> Delete(string id)
        {
            DeleteResult deleteResult = await _workoutRepository.DeleteRecordAsync(id);
            return deleteResult;
        }
    }
}