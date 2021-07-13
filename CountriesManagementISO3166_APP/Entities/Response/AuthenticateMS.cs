using System;
using System.Collections.Generic;
using System.Text;

namespace CountriesManagementISO3166_APP.Entities.Response
{
    public class AuthenticateMS
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
    }
}
