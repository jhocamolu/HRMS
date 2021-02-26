using System.Collections.Generic;

namespace ApiV3.Servicios.RequestData
{
    public class RequestData : IRequestData
    {
        private IDictionary<string, string> data;

        public RequestData()
        {
            data = new Dictionary<string, string>();
        }

        public string getData(string key)
        {
            foreach (var reg in data)
            {
                if (string.Equals(reg.Key, key))
                {
                    return reg.Value;
                }
            }
            return null;
        }

        public void setData(string key, string value)
        {
            if (data.ContainsKey(key))
                data.Remove(key);

            data.Add(key, value);
        }
    }
}
