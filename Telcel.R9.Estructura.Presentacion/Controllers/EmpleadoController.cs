using Microsoft.AspNetCore.Mvc;

namespace Telcel.R9.Estructura.Presentacion.Controllers
{
    public class EmpleadoController : Controller
    {
        public IActionResult GetAll()
        {

            Negocio.Result result = Negocio.Empleado.GetAll();

            Negocio.Empleado empleado = new Negocio.Empleado();


            if (result.Correct)
            {
                empleado.Empleados = result.Objects;
            }
            else
            {
                ViewBag.Message = result.Message;
            }
            return View(empleado);
        }

        [HttpGet]
        public IActionResult Form(int? empleadoID) 
        {
            Negocio.Empleado empleado = new Negocio.Empleado();

            empleado.Departamento = new Negocio.Departamento();
            empleado.Puesto = new Negocio.Puesto();

            Negocio.Result resultDepto = Negocio.Departamento.GetAll();
            Negocio.Result resultPuesto = Negocio.Puesto.GetAll();

            empleado.Departamento.Departamentos = resultDepto.Objects;
            empleado.Puesto.Puestos = resultPuesto.Objects;

            if (empleadoID == null)
            {
                ViewBag.Accion = "Agregar";
            }
            else
            {
                ViewBag.Accion = "Actualizar";

                //GetByID
            }
            

            return View(empleado);
        }

        [HttpPost]
        public IActionResult Form(Negocio.Empleado empleado)
        {
            Negocio.Result result = new Negocio.Result();

            if (empleado.EmpleadoId == 0)
            {
                //add
               result = Negocio.Empleado.Add(empleado);
            }
            else
            {
                //update
            }
            return RedirectToAction("GetAll","Empleado");
        }
    }
}
