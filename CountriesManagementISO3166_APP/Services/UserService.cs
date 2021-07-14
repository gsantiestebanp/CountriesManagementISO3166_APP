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

        public async Task DeleteCountry(CountryME country)
        {
            Priority prioridad = Priority.Explicit;
            await DeleteCountryApi(country, prioridad);
        }

        public async Task DeleteCountryApi(CountryME country, Priority prority)
        {
            Task task;
            try
            {
                switch (prority)
                {
                    case Priority.Background:
                        task = ApiService.Background.DeleteCountry(country);
                        break;
                    case Priority.UserInitiated:
                        task = ApiService.UserInitiated.DeleteCountry(country);
                        break;
                    case Priority.Speculative:
                        task = ApiService.Speculative.DeleteCountry(country);
                        break;
                    default:
                        task = ApiService.UserInitiated.DeleteCountry(country);
                        break;
                }

                if (CheckInternetAccess.CheckConnection())
                {
                    await Policy
                          .Handle<ApiException>()
                          .Or<WebException>()
                          .Or<TaskCanceledException>()
                          .RetryAsync(retryCount: 2)
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
        }

        public Task DeleteSubdivision(SubdivisionME subdivision)
        {
            throw new System.NotImplementedException();
        }

        public async Task<List<CountryMS>> GetCountries()
        {
            Priority prioridad = Priority.Explicit;
            return await GetCountriesApi(prioridad);
        }

        private async Task<List<CountryMS>> GetCountriesApi(Priority priority)
        {
            List<CountryMS> response = new List<CountryMS>();
            Task<List<CountryMS>> task;
            try
            {
                switch (priority)
                {
                    case Priority.Background:
                        task = ApiService.Background.GetCountries();
                        break;
                    case Priority.UserInitiated:
                        task = ApiService.UserInitiated.GetCountries();
                        break;
                    case Priority.Speculative:
                        task = ApiService.Speculative.GetCountries();
                        break;
                    default:
                        task = ApiService.UserInitiated.GetCountries();
                        break;
                }

                if (CheckInternetAccess.CheckConnection())
                {
                    response = await Policy
                          .Handle<ApiException>()
                          .Or<WebException>()
                          .Or<TaskCanceledException>()
                          .RetryAsync(retryCount: 2)
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

        public async Task<List<SubdivisionMS>> GetSubdivisions()
        {
            Priority prioridad = Priority.Explicit;
            return await GetSubdivisionsApi(prioridad);
        }

        private async Task<List<SubdivisionMS>> GetSubdivisionsApi(Priority priority)
        {
            List<SubdivisionMS> response = new List<SubdivisionMS>();
            Task<List<SubdivisionMS>> task;
            try
            {
                switch (priority)
                {
                    case Priority.Background:
                        task = ApiService.Background.GetSubdivisions();
                        break;
                    case Priority.UserInitiated:
                        task = ApiService.UserInitiated.GetSubdivisions();
                        break;
                    case Priority.Speculative:
                        task = ApiService.Speculative.GetSubdivisions();
                        break;
                    default:
                        task = ApiService.UserInitiated.GetSubdivisions();
                        break;
                }

                if (CheckInternetAccess.CheckConnection())
                {
                    response = await Policy
                          .Handle<ApiException>()
                          .Or<WebException>()
                          .Or<TaskCanceledException>()
                          .RetryAsync(retryCount: 2)
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

        public async Task InsertCountry(CountryME country)
        {           
            Priority prioridad = Priority.Explicit;
            await InsertCountryApi(country, prioridad);
        }

        public async Task InsertCountryApi(CountryME country, Priority priority)
        {
            Task task;
            try
            {
                switch (priority)
                {
                    case Priority.Background:
                        task = ApiService.Background.InsertCountry(country);
                        break;
                    case Priority.UserInitiated:
                        task = ApiService.UserInitiated.InsertCountry(country);
                        break;
                    case Priority.Speculative:
                        task = ApiService.Speculative.InsertCountry(country);
                        break;
                    default:
                        task = ApiService.UserInitiated.InsertCountry(country);
                        break;
                }

                if (CheckInternetAccess.CheckConnection())
                {
                    await Policy
                          .Handle<ApiException>()
                          .Or<WebException>()
                          .Or<TaskCanceledException>()
                          .RetryAsync(retryCount: 2)
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

        }

        public Task<SubdivisionMS> InsertCountryApi(SubdivisionME subdivision)
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

        public async Task InsertSubdivision(SubdivisionME subdivision)
        {
            Priority prioridad = Priority.Explicit;
            await InsertSubdivisionApi(subdivision, prioridad);
        }

        public async Task InsertSubdivisionApi(SubdivisionME subdivision, Priority priority)
        {
            Task task;
            try
            {
                switch (priority)
                {
                    case Priority.Background:
                        task = ApiService.Background.InsertSubdivision(subdivision);
                        break;
                    case Priority.UserInitiated:
                        task = ApiService.UserInitiated.InsertSubdivision(subdivision);
                        break;
                    case Priority.Speculative:
                        task = ApiService.Speculative.InsertSubdivision(subdivision);
                        break;
                    default:
                        task = ApiService.UserInitiated.InsertSubdivision(subdivision);
                        break;
                }

                if (CheckInternetAccess.CheckConnection())
                {
                    await Policy
                          .Handle<ApiException>()
                          .Or<WebException>()
                          .Or<TaskCanceledException>()
                          .RetryAsync(retryCount: 2)
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

        }

        public async Task<List<SubdivisionMS>> GetSubdivisionsByCountryId(int id)
        {
            Priority priority = Priority.Explicit;
            return await GetSubdivisionsByCountryIdApi(id, priority);
        }

        public async Task<List<SubdivisionMS>> GetSubdivisionsByCountryIdApi(int id, Priority priority)
        {
            List<SubdivisionMS> response = new List<SubdivisionMS>();
            Task<List<SubdivisionMS>> task;
            try
            {
                switch (priority)
                {
                    case Priority.Background:
                        task = ApiService.Background.GetSubdivisionsByCountryId(id);
                        break;
                    case Priority.UserInitiated:
                        task = ApiService.UserInitiated.GetSubdivisionsByCountryId(id);
                        break;
                    case Priority.Speculative:
                        task = ApiService.Speculative.GetSubdivisionsByCountryId(id);
                        break;
                    default:
                        task = ApiService.UserInitiated.GetSubdivisionsByCountryId(id);
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
    }
}