namespace CountriesManagementISO3166_APP.Entities.Response
{
    public class CountryMS
    {
        public int CountryId { get; set; }
        public string CommonName { get; set; }
        public string IsoName { get; set; }
        public string Alpha2Code { get; set; }
        public string Alpha3Code { get; set; }
        public int NumericCode { get; set; }
        public int NumberSubdivisions { get; set; }
        public string Observation { get; set; }
    }
}
