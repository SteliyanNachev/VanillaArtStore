namespace VanillaArtStore.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Threading.Tasks;
    using VanillaArtStore.Data.Models;

    public static class AdressExtentions
    {
        public static List<string> GetCountryList()
        {
            var CultureList = new List<string>();

            CultureInfo[] getCultureIfno = CultureInfo.GetCultures(CultureTypes.SpecificCultures);

            foreach (var getCulture in getCultureIfno)
            {
                RegionInfo GetRegionInfo = new RegionInfo(getCulture.LCID);

                if (!(CultureList.Contains(GetRegionInfo.EnglishName)))
                {
                    CultureList.Add(GetRegionInfo.EnglishName);
                }
            }

            CultureList.Sort();

            return CultureList;
        }

    }
}
