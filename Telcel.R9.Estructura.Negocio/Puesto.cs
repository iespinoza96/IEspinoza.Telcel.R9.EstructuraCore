using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telcel.R9.Estructura.Negocio
{
    public class Puesto
    {
        public int PuestoId { get; set; }

        public string? Descripcion { get; set; }

        public List<object> Puestos { get; set; }

        public static Result GetAll()
        {
            Result result = new Result();

            try
            {
                using (AccesoDatos.IespinozaTelcelR9estructuraContext context = new AccesoDatos.IespinozaTelcelR9estructuraContext())
                {
                    var query = (from puestoLINQ in context.Puestos
                                 select new
                                 {
                                     PuestoId = puestoLINQ.PuestoId,
                                     Descripcion = puestoLINQ.Descripcion
                                 }).ToList();

                    if (query != null)
                    {
                        result.Objects = new List<object>();

                        foreach (var obj in query)
                        {
                            Puesto puesto = new Puesto();

                            puesto.PuestoId = obj.PuestoId;
                            puesto.Descripcion = obj.Descripcion;

                            result.Objects.Add(puesto);
                        }

                        result.Correct = true;
                    }
                }

            }
            catch (Exception)
            {

                throw;
            }

            return result;
        }

    }
}
