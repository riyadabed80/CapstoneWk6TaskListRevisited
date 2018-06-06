using System.Web;
using System.Web.Mvc;

namespace CapstoneWk6TaskListRevisted
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
