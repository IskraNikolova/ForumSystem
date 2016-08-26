namespace ForumSystem.Logic
{
    public class ТextТransformer
    {
        private const int DefaultLength = 250;

        public static string CutText(string text, int length = DefaultLength)
        {
            string result = text;
            if (text.Length > length)
            {
               result = text.Substring(0, length) + "...";
            }

            return result;
        }
    }
}
