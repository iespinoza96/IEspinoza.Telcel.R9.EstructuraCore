using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Telcel.R9.Estructura.Negocio
{
    public class Empleado
    {
        public int EmpleadoId { get; set; }

        public string? Nombre { get; set; }

        public Negocio.Departamento? Departamento { get; set; }

        public Negocio.Puesto? Puesto { get; set; }

        public List<object> Empleados { get; set; }

        public static Result GetAll()
        {
            Result result = new Result();

            try
            {
                using (AccesoDatos.IespinozaTelcelR9estructuraContext context = new AccesoDatos.IespinozaTelcelR9estructuraContext())
                {
                    var query = (from empleadoLINQ in context.Empleados
                                 join deptoLINQ in context.Departamentos on empleadoLINQ.DepartamentoId equals deptoLINQ.DepartamentoId
                                 join puestoLINQ in context.Puestos on empleadoLINQ.PuestoId equals puestoLINQ.PuestoId

                                 select new
                                 {
                                     IdEmpleado = empleadoLINQ.EmpleadoId,
                                     Nombre = empleadoLINQ.Nombre,
                                     DepartamentoId = empleadoLINQ.DepartamentoId,
                                     DescDepto = deptoLINQ.Descripcion,
                                     IdPuesto = empleadoLINQ.PuestoId,
                                     DescPuesto = puestoLINQ.Descripcion

                                 }).ToList();

                    if (query != null)
                    {
                        result.Objects = new List<object>();

                        foreach (var obj in query)
                        {
                            Empleado empleado = new Empleado();

                            empleado.EmpleadoId = obj.IdEmpleado;
                            empleado.Nombre = obj.Nombre;

                            empleado.Departamento = new Departamento();
                            empleado.Departamento.DepartamentoID = obj.DepartamentoId.Value;
                            empleado.Departamento.Descripcion = obj.DescDepto;

                            empleado.Puesto = new Puesto();
                            empleado.Puesto.PuestoId = obj.IdPuesto.Value;
                            empleado.Puesto.Descripcion = obj.DescPuesto;

                            result.Objects.Add(empleado);
                        }

                        result.Correct = true;
                    }
                }

            }
            catch (Exception ex)
            {
                result.Ex = ex;
                result.Correct = false;
                result.Message = "Ocurrio un error al realizar la consutla  " + ex.Message;
                throw;
            }

            return result;
        }

        public static Result Add(Negocio.Empleado empleado)
        {
            Result result = new Result();
            try
            {
                using (AccesoDatos.IespinozaTelcelR9estructuraContext context = new AccesoDatos.IespinozaTelcelR9estructuraContext())
                {
                    AccesoDatos.Empleado empleadoAD = new AccesoDatos.Empleado(); // es un objeto del Modelo de EF

                    empleadoAD.Nombre = empleado.Nombre;
                    empleadoAD.DepartamentoId = empleado.Departamento.DepartamentoID;
                    empleadoAD.PuestoId = empleado.Puesto.PuestoId;
                   

                    context.Empleados.Add(empleadoAD);
                    context.SaveChanges();
                }
            }

            catch (Exception ex)
            {
                result.Correct = false;
                result.Message = "Ocurrio un error al insertar el empleado " + ex.Message;
            }

            return result;
        }

    }
}
