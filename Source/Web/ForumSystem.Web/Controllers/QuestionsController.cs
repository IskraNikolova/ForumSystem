namespace ForumSystem.Web.Controllers
{
    using System.Web.Mvc;

    public class QuestionsController : Controller
    {
        // GET: Questions
        public ActionResult Display(int id, string url)
        {
            return this.Content(id + " " + url);
        }
    }
}