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
        public DateTime date_added { get; set; } = DateTime.Now;
        public List<DateTime> workout_times { get;set;}
        public string workout_image_url { get; set; }
        public List<WorkoutLocation> locations { get; set; }
        public List<WorkoutDevice> devices { get; set; }
        public List<Series> workout_series { get; set; }
    }

    public class Series
    {
        [Required]
        [Display(Name = "SeriesNumber")]
        // [Range(1, int.MaxValue, ErrorMessage = "Your {0} Please enter a valid integer > 0!")]
        public int series_number { get; set; }
        // [Required(ErrorMessage="test error messagexxxxxyyyyyy")]
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

    public class WorkoutValidator : AbstractValidator<Workout>
    {
        public WorkoutValidator()
        {
            // Workout Date Validations
            // RuleFor(x => x.workout_date).NotEqual(DateTime.Parse("0001-01-01T00:00:00")).WithMessage("Please enter a valid date!");
            // Workout Date was null
            //RuleFor(x => x.date_added).Must(x => x != null).WithMessage("A workout date must be set!!");
            // Workout Date was previous date from current
            //RuleFor(x => x.workout_date).GreaterThan(DateTime.Now).WithMessage($"Please enter a date > {DateTime.Now.ToString("MM/dd/yyyy hh:mm tt")}");
            // Workout Series
            // No Series information at all was sent
            RuleFor(x => x.workout_series).NotEmpty().WithMessage("No Series Information Was Entered!!");
            // Series Validations
            // Rule for No Series tag
            RuleFor(x => x.workout_series).Must(y =>
            {
                foreach (var series in y)
                {
                    if (String.IsNullOrEmpty(series.series_tag))
                    {
                        return false;
                    }
                }
                return true;
            }).WithMessage("All series must have a tag name!");
            // Duplicate Series number
            RuleFor(x => x.workout_series).Must(y =>
            {
                var numbersFromList = y.Select(i => i.series_number).ToList();
                return numbersFromList.Count() == numbersFromList.Distinct().Count();
            }
            ).WithMessage("Series Numbers cannot have the same value!");
            // Series numbers must be > 0
            RuleFor(x => x.workout_series).Must(s =>
            {
                var nubmerList = s.Select(n => n.series_number).ToList();
                return !nubmerList.Any(x => x == 0);
            }).WithMessage("Invalid Series Number used. Series number must be > 0");
            // Exercise Validations
            // Duplicate Exercise numbers in a given Series
            RuleFor(x => x.workout_series).Must(y =>
            {
                List<Exercise> exercises = new List<Exercise>();
                foreach (Series series in y)
                {
                    List<int> nubmerList = new List<int>();
                    foreach (Exercise exercise in series.exercises)
                    {
                        nubmerList.Add(exercise.exercise_number);
                    }
                    if (nubmerList.Count() != nubmerList.Distinct().Count())
                    {
                        return false;
                    }
                }
                return true;
            }).WithMessage("Exercise Numbers cannont have the same value for a given Series!");
            // Exercise number must be > 0
            RuleFor(x => x.workout_series).Must(y =>
            {
                foreach (Series series in y)
                {
                    foreach (Exercise exercise in series.exercises)
                    {
                        if (exercise.exercise_number == 0)
                        {
                            return false;
                        }
                    }
                }
                return true;
            }).WithMessage("Invalid Exercise Number used. Exercise number must be > 0");
        }
    }
}