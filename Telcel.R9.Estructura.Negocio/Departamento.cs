using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telcel.R9.Estructura.Negocio
{
    public class Departamento
    {
        public int DepartamentoID { get; set; }

        public string Descripcion { get; set; }

        public List<object> Departamentos { get; set; }

        public static Result GetAll()
        {
            Result result = new Result();

            try
            {
                using (AccesoDatos.IespinozaTelcelR9estructuraContext context = new AccesoDatos.IespinozaTelcelR9estructuraContext())
                {
                    var query = (from deptoLINQ in context.Departamentos
                                select new
                                {
                                    DepartamentoId = deptoLINQ.DepartamentoId,
                                    Descripcion = deptoLINQ.Descripcion
                                }).ToList();

                    if (query != null)
                    {
                        result.Objects = new List<object>();

                        foreach (var obj in query)
                        {
                            Departamento departamento = new Departamento();

                            departamento.DepartamentoID = obj.DepartamentoId;
                            departamento.Descripcion = obj.Descripcion;

                            result.Objects.Add(departamento);
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
