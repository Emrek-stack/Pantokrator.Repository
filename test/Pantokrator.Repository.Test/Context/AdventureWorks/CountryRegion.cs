using System;
using System.Collections.Generic;

namespace Pantokrator.Repository.Test.Context.AdventureWorks
{
    public partial class CountryRegion
    {
        public CountryRegion()
        {
            CountryRegionCurrency = new HashSet<CountryRegionCurrency>();
            SalesTerritory = new HashSet<SalesTerritory>();
            StateProvince = new HashSet<StateProvince>();
        }

        public string CountryRegionCode { get; set; }
        public string Name { get; set; }
        public DateTime ModifiedDate { get; set; }

        public ICollection<CountryRegionCurrency> CountryRegionCurrency { get; set; }
        public ICollection<SalesTerritory> SalesTerritory { get; set; }
        public ICollection<StateProvince> StateProvince { get; set; }
    }
}
