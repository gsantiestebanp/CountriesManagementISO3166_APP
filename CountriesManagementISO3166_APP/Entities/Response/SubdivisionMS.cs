using System;
using System.Collections.Generic;
using System.Text;

namespace CountriesManagementISO3166_APP.Entities.Response
{
    public class SubdivisionMS
    {
        public int SubdivisionId { get; set; }
        public int CountryId { get; set; }
        public string Name { get; set; }
        public string SubdivisionCode { get; set; }
    }
}
