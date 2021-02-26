namespace ApiV3.Infraestructura.Utilidades
{
    public static class ConstantesExpresionesRegulares
    {
        public const string Numerico = "0-9";

        public const string Alfabetico = "A-Za-zäÄëËïÏöÖüÜáéíóúáéíóúÁÉÍÓÚñÑ";

        public const string Espacio = "\\s";

        public const string Alfanumerico = "0-9A-Za-zñÑ";

        public const string SignosPuntuacion = ".:;,";

        public const string Parentesis = "()";

        public const string Llaves = "{}";

        public const string Corchetes = "[]";

        public const string Guion = "-";

        public const string Grado = "°";

        public const string GuionAlPiso = "_";

        public const string Slash = "/";

        public const string Expresiones = "¿?¡!";

        public const string Numeral = "#";

        public const string TipoOracion = Alfabetico + Espacio + SignosPuntuacion;

        public const string Decimales = "^[0-9]+([.][0-9]+)?$";
    }
}
