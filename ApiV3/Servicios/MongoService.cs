using ApiV3.Infraestructura.DbContexto;
using MongoDB.Bson;
using MongoDB.Driver;
using System;

namespace ApiV3.Servicios
{
    public class MongoService
    {
        private IMongoCollection<dynamic> elemento;
        private readonly IMongoDatabase database;

        public MongoService(IMongoDbContext settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            this.database = client.GetDatabase(settings.DatabaseName);
        }

        public dynamic Create(string objetoJsonAnterior, dynamic id, string objetoJsonActual, string tabla, string accion, string usuario)
        {
            try
            {
                DateTime fechaActual = DateTime.Today;

                elemento = this.database.GetCollection<dynamic>(tabla);

                var Coleccion = new BsonDocument
                {
                    { "Usuario", BsonValue.Create(usuario)},
                    { "Accion", new BsonString(accion)},
                    { "Fecha", new BsonString(fechaActual.ToString())},
                    { "Id", new BsonString(id)},
                    { "ObjetoAnterior", new BsonString(objetoJsonAnterior) },
                    { "ObjetoActual", new BsonString(objetoJsonActual) }
                };


                elemento.InsertOneAsync(Coleccion);

                return elemento;
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
    }
}

