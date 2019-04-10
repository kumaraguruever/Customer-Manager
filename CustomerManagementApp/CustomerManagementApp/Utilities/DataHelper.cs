using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CustomerManagementApp.Utilities
{
    public static class DataHelper
    {
        public const string ALL_VALUE = "All";
        public static List<string> GetCountries()
        {
            List<string> _CountryList = new List<string>();
            CultureInfo[] CInfoList = CultureInfo.GetCultures(CultureTypes.SpecificCultures);
            foreach (CultureInfo CInfo in CInfoList)
            {
                RegionInfo R = new RegionInfo(CInfo.LCID);
                if (!(_CountryList.Contains(R.EnglishName)))
                {
                    _CountryList.Add(R.EnglishName);
                }
            }
            return _CountryList.OrderBy(country => country).ToList();
        }

        public static List<SelectListItem> GetCountriesDropDownList(bool includeAll = false)
        {
            List<SelectListItem> _Countries = new List<SelectListItem>();
            if (includeAll)
            {
                _Countries.Add(new SelectListItem() { Text = ALL_VALUE, Value = ALL_VALUE }); 
            }
            _Countries.AddRange(GetCountries().Select(country => new SelectListItem()
            {
                Text = country,
                Value = country
            }).ToList());

            return _Countries;
        }
    }
}