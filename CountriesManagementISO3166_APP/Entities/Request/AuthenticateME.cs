using System;
using System.Collections.Generic;
using System.Text;

namespace CountriesManagementISO3166_APP.Entities.Request
{
    public class AuthenticateME
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
