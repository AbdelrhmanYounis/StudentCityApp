using System;
using StudentCityUI.Dto;
using Newtonsoft.Json;

namespace StudentCityUI.Helpers
{
    public class JsonConverter
    {
       

        public static TokenDto Deserialize(String token)
        {
            var res = JsonConvert.DeserializeObject<TokenDto>(token);
            return res;
        }
        public static string Serialize(TokenDto token)
        {
            return JsonConvert.SerializeObject(token) ;
        }
    }
}
