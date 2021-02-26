namespace ApiV3.Servicios.Autenticacion
{
    public interface IAutenticacionService
    {
        bool TokenValido(string token, string permiso = null);
    }
}
