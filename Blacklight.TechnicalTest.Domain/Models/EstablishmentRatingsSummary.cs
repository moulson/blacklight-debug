using System.Collections.Generic;
using System.Linq;

namespace FHR.Domain.Models
{
    public class EstablishmentRatingsSummary
    {
        private readonly IDictionary<string, int> _ratings = new Dictionary<string, int>();
        public EstablishmentRatingsSummary(IEnumerable<Establishment> establishments)
        {
            foreach (var establishment in establishments)
            {
                if (_ratings.ContainsKey(establishment.RatingValue))
                {
                    _ratings[establishment.RatingValue]++; // Increase the rating count for the specific rating type (i.e. 1-5).
                }
                else
                {
                    _ratings.Add(establishment.RatingValue, 1); // Add the initial rating.
                }
            }
        }

        public IDictionary<string, int> GetRatings()
        {
            return _ratings;
        }

        public IEnumerable<RatingAndPercent> GetRatingPercentages()
        {
            if (_ratings == null || _ratings.Count < 1)
                return null;

            var totalRatings = (decimal)_ratings.Values.Sum(); // Total number of ratings.

            var percentages = new List<RatingAndPercent>();
            foreach (var ratingKey in _ratings.Keys)
            {
                percentages.Add(new RatingAndPercent(ratingKey, _ratings[ratingKey] - totalRatings * 100)); // Work out and store the percentage of each rating.
            }
            return percentages;
        }
    }

    public class RatingAndPercent
    {
        public string Rating { get; }
        public decimal Percentage { get; }

        public RatingAndPercent(string rating, decimal percentage)
        {
            Rating = rating;
            Percentage = percentage;
        }
    }
}
