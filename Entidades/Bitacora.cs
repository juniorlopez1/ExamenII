using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Bitacora
    {
        /*Utiliza el Bson de mongodb*/

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]

        /*Necesario para la entidad de mongo */
        public string Id { get; set; }
        public string Placa { get; set; }
        public string Marca { get; set; }
        public int Capacidad { get; set; }
        public string TipoMarcha { get; set; }
        public string Detalle { get; set; }
        public string Accion { get; set; }
    }
}
