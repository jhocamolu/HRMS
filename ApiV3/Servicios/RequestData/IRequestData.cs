namespace ApiV3.Servicios.RequestData
{
    public interface IRequestData
    {
        void setData(string key, string value);
        string getData(string key);

    }
}
