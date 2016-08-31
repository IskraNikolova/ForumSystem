namespace ForumSystem.Web.Logic
{
    using System.Collections.Generic;
    using System.Linq;
    using ForumSystem.Models;

    public static class GetUserWithTheBestResult
    {
        private const int DefaultLengthForRating = 7;
        public static IList<ApplicationUser> RatingByPoints(IList<ApplicationUser> allUser)
        {
            var result = allUser
                .OrderByDescending(u => u.Points)
                .Take(DefaultLengthForRating)
                .ToList();

            return result;
        }
    }
}