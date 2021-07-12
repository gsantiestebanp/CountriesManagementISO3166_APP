using CountriesManagementISO3166_APP.Entities;
using CountriesManagementISO3166_APP.Entities.Request;
using CountriesManagementISO3166_APP.Entities.Response;
using CountriesManagementISO3166_APP.Interfaces;
using Fusillade;
using Newtonsoft.Json;
using Polly;
using Refit;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace CountriesManagementISO3166_APP.Services
{
    public class UserService : IUserService
    {
        private static readonly string apiUrl = Resources.Resources.ApiUrl;

        private static readonly ApiService<IUserService> ApiService = new ApiService<IUserService>(apiUrl);

        public UserService()
        {
        }

        public Task DeleteCountry(CountryME country)
        {
            throw new System.NotImplementedException();
        }

        public Task DeleteSubdivision(SubdivisionME subdivision)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<CountryMS>> GetCountries()
        {
            throw new System.NotImplementedException();
        }

        public Task<List<CountryMS>> GetCountryByAlpha2Code(GetCountryByAlpha2CodeME alpha2Code)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<CountryMS>> GetCountryByCommonName(GetCountryByCommonNameME commonName)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<SubdivisionMS>> GetSubdivisionById(GetSubdivisionByIdME subdivisionId)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<SubdivisionMS>> GetSubdivisions()
        {
            throw new System.NotImplementedException();
        }

        public Task<CountryMS> InsertCountry(CountryME country)
        {
            throw new System.NotImplementedException();
        }

        public Task<SubdivisionMS> InsertSubdivision(SubdivisionME subdivision)
        {
            throw new System.NotImplementedException();
        }

        public async Task<AuthenticateMS> Login(AuthenticateME authenticationData)
        {
            Priority priority = Priority.Explicit;
            return await LoginApi(authenticationData, priority);
        }
       
        public async Task<AuthenticateMS> LoginApi(AuthenticateME authenticationData, Priority priority)
        {
            AuthenticateMS response = new AuthenticateMS();
            Task<AuthenticateMS> task;
            try
            {
                switch (priority)
                {
                    case Priority.Background:
                        task = ApiService.Background.Login(authenticationData);
                        break;
                    case Priority.UserInitiated:
                        task = ApiService.UserInitiated.Login(authenticationData);
                        break;
                    case Priority.Speculative:
                        task = ApiService.Speculative.Login(authenticationData);
                        break;
                    default:
                        task = ApiService.UserInitiated.Login(authenticationData);
                        break;
                }

                if (CheckInternetAccess.CheckConnection())
                {
                    response = await Policy
                          .Handle<ApiException>()
                          .Or<WebException>()
                          .Or<TaskCanceledException>()
                          .RetryAsync(retryCount: 4)
                          .ExecuteAsync(async () => await task);
                }
            }
            catch (ApiException ex)
            {
                var message = JsonConvert.DeserializeObject<ErrorMessage>(ex.Content);
                throw new Exception(message.InnerException.ExceptionMessage);
            }
            catch (WebException ex)
            {
                throw new Exception(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return response;
        }

        public Task UpdateCountry(CountryME country)
        {
            throw new System.NotImplementedException();
        }

        public Task UpdateSubdivision(SubdivisionME subdivision)
        {
            throw new System.NotImplementedException();
        }
    }
}