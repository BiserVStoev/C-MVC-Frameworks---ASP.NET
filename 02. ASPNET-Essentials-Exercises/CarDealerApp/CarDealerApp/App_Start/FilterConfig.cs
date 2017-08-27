namespace CarDealerApp
{
    using System.Web.Mvc;
    using System;

    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute()
            {
                ExceptionType = typeof(Exception),
                View = "CustomError"
            });
        }
    }
}
