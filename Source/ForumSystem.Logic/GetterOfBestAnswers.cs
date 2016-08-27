namespace ForumSystem.Logic
{
    using System.Collections.Generic;
    using System.Linq;
    using Models;

    public class GetterOfBestAnswers
    {
        private const int DefaultLength = 3;

        public static IEnumerable<Answer> GetBestAnswers(IEnumerable<Post> all, int length = DefaultLength)
        {
            var allListOfAnswers = all.Select(p => p.Answers).ToList();
            var allAnswers = new List<Answer>();
            foreach (var list in allListOfAnswers)
            {
                foreach (var answer in list)
                {
                    allAnswers.Add(answer);
                }
            }
            var ordered = allAnswers.OrderByDescending(a => a.Rating);
            var result = ordered.Take(length);

            return result;
        }
    }
}
