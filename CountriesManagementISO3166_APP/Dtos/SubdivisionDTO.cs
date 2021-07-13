namespace CountriesManagementISO3166_APP.Dtos
{
    public class SubdivisionDTO
    {
        public int SubdivisionId { get; set; }
        public int CountryId { get; set; }
        public string Name { get; set; }
        public string SubdivisionCode { get; set; }
    }
}
