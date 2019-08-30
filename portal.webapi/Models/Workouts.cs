using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace portal.webapi.Models
{
    public class Workout
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string id { get; set; }
        public string workout_name { get; set; } = $"Workout {DateTime.Now.ToString("MM/dd/yyyy | hh:mm")}";
        public DateTime workout_date { get; set; }
        public string workout_image_url { get; set; }
        public List<Series> workout_series { get; set; }
    }
    public class NewWorkout
    {
        public string workout_name { get; set; }
        public DateTime workout_date { get; set; }
        public string workout_image_url { get; set; }
        public List<Series> workout_series { get; set; }
    }
    public class Series
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage="Please enter a valid integer > 0!")]
        public int series_number { get; set; }
        [Required]
        public string series_tag { get; set; }
        [Required]
        public List<Exercise> exercises { get; set; }
    }
    public class Exercise
    {
        [Required]
        public int exercise_number { get; set; }
        [Required]
        public string exercise_name { get; set; }
        [Required]
        public string exercise_reps { get; set; }
    }
}