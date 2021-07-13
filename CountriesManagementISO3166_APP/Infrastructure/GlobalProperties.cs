using Newtonsoft.Json;

namespace CountriesManagementISO3166_APP.Infraestructure
{
    public class GlobalProperties
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }

        public GlobalProperties()
        {
            
        }

        public void Add()
        {
            var recipeJson = JsonConvert.SerializeObject(this);

            if (App.Current.Properties.ContainsKey("GlobalData"))
                App.Current.Properties["GlobalData"] = recipeJson;
            else
                App.Current.Properties.Add("GlobalData", recipeJson);

            App.Current.SavePropertiesAsync();
        }

        public static GlobalProperties Get()
        {            
            GlobalProperties data = new GlobalProperties(); 

            if (App.Current.Properties.ContainsKey("GlobalData"))
            {
                string globalData = App.Current.Properties["GlobalData"] as string;
                data = JsonConvert.DeserializeObject<GlobalProperties>(globalData);
            }

            return data;
        }                    
    }
}
