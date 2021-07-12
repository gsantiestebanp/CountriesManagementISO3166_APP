namespace CountriesManagementISO3166_APP.Entities.Request
{
    public class SubdivisionME
    {
        public int SubdivisionId { get; set; }
        public int CountryId { get; set; }
        public string Name { get; set; }
        public string SubdivisionCode { get; set; }
    }
}
