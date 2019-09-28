using System.Web;
using System.Web.Mvc;

namespace AT_Marcos_Vinicius {
    public class FilterConfig {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
