namespace ForumSystem.Web
{
    using System.Web.Mvc;
    using System.Web.Routing;

    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Get question by ID",
                url: "questions/tagged/{tag}",
                defaults: new { controller = "Questions", action = "GetByTag" }
                );

            routes.MapRoute(
                name: "Delete Question",
                url: "Questions/Delete/{id}",
                defaults: new { controller = "Questions", action = "Delete", id = UrlParameter.Optional }
                );

            routes.MapRoute(
                name: "Display question",
                url: "questions/{id}/{url}",
                defaults: new { controller = "Questions", action = "Display" }
                );

            routes.MapRoute(
                name: "CreateAnswer",
                url: "Answer/Create/{id}",
                defaults: new { controller = "Answer", action = "Create", id = UrlParameter.Optional }
                );

            routes.MapRoute(
                name: "ViewAnswer",
                url: "Answer/ViewAnswer/{id}",
                defaults: new { controller = "Answer", action = "ViewAll", id = UrlParameter.Optional }
                );

            routes.MapRoute(
                name: "DeleteAnswer",
                url: "Answer/ViewAnswer/Delete/{id}",
                defaults: new { controller = "Answer", action = "Delete", id = UrlParameter.Optional }
                );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
                );
        }
    }
}
