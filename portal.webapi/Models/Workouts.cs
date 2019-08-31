using System;
using System.Linq;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;
using FluentValidation;

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
        [Display(Name="SeriesNumber")]
        [Range(1, int.MaxValue, ErrorMessage="Your {0} Please enter a valid integer > 0!")]
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

    public class WorkoutValidator : AbstractValidator<Workout> {
	public WorkoutValidator() {
        // Workout Series
        RuleFor(x => x.workout_series).NotEmpty().WithMessage("No Series Information Was Entered!!");
        // Workout Date Validations
		RuleFor(x => x.workout_date).NotEqual(DateTime.Parse("0001-01-01T00:00:00")).WithMessage("Please enter a valid date!");
        RuleFor(x => x.workout_date).GreaterThan(DateTime.Now).WithMessage($"Please enter a date > {DateTime.Now.ToString("MM/dd/yyyy hh:mm tt")}");
        // Series Validations
        // Validate there are no duplicate series numbers
        RuleFor(x => x.workout_series).Must(y => 
        {
            var list = y.Select(i => i.series_number).ToList();
            return list.Count() == list.Distinct().Count();
        }
        ).WithMessage("Series Numbers cannot have the same value!");
    }
}
}