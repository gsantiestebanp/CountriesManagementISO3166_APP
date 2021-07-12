using CountriesManagementISO3166_APP.Entities.Request;
using CountriesManagementISO3166_APP.Entities.Response;
using Refit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CountriesManagementISO3166_APP.Interfaces
{
    [Headers("Content-Type: application/json")]

    public interface IUserService
    {
        #region Login
        [Post("/api/User/Authenticate")]
        Task<AuthenticateMS> Login(AuthenticateME authenticationData);
        #endregion

        #region Countries
        [Get("/api/Country/GetAllCountries")]
        Task<List<CountryMS>> GetCountries();

        [Post("/api/Country/GetCountryByCommonName")]
        Task<List<CountryMS>> GetCountryByCommonName(GetCountryByCommonNameME commonName);

        [Post("/api/Country/GetCountryByAlpha2Code")]
        Task<List<CountryMS>> GetCountryByAlpha2Code(GetCountryByAlpha2CodeME alpha2Code);

        [Post("/api/Country/InsertCountry")]
        Task<CountryMS> InsertCountry(CountryME country);

        [Post("/api/Country/UpdateCountry")]
        Task UpdateCountry(CountryME country);

        [Post("/api/Country/DeleteCountry")]
        Task DeleteCountry(CountryME country);
        #endregion

        #region Subdivisions
        [Get("/api/Subdivision/GetAllSubdivisions")]
        Task<List<SubdivisionMS>> GetSubdivisions();

        [Post("/api/Subdivision/GetSubdivisionById")]
        Task<List<SubdivisionMS>> GetSubdivisionById(GetSubdivisionByIdME subdivisionId);

        [Post("/api/Subdivision/InsertSubdivision")]
        Task<SubdivisionMS> InsertSubdivision(SubdivisionME subdivision);

        [Post("/api/Subdivision/UpdateSubdivision")]
        Task UpdateSubdivision(SubdivisionME subdivision);

        [Post("/api/Subdivision/DeleteSubdivision")]
        Task DeleteSubdivision(SubdivisionME subdivision);
        #endregion
    }
}