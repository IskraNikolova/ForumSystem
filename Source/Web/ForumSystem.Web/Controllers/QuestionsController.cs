namespace ForumSystem.Web.Controllers
{
    using System.Web.Mvc;
    using InputModels.Question;

    public class QuestionsController : Controller
    {
        // GET: Questions
        public ActionResult Display(int id, string url, int page = 1)
        {
            return this.Content(id + " " + url);
        }

        public ActionResult GetByTag(string tag)
        {
            return this.Content(tag);
        }

        [HttpGet]
        public ActionResult Ask()
        {
            return this.Content("Get");
        }

        [HttpPost]
        public ActionResult Ask(AskInputModel input)
        {
            return this.Content("Post");
        }
    }
}