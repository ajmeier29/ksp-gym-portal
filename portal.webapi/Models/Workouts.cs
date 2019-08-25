using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ksp_portal.Models
{
    public class Workout
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string id { get; set; }
        public string workout_name { get; set; }
        public DateTime workout_date { get; set; }
        public string workout_image_url { get; set; }
        public List<Series> workout_series { get; set; }
    }
    public class Series
    {
        public int series_number { get; set; }
        public string series_tag { get; set; }
        public List<Exercise> exercises { get; set; }
    }
    public class Exercise
    {
        public int exercise_number { get; set; }
        public string exercise_name { get; set; }
        public string exercise_reps { get; set; }
    }
}