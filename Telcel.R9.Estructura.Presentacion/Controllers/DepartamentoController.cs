using Microsoft.AspNetCore.Mvc;

namespace Telcel.R9.Estructura.Presentacion.Controllers
{
    public class DepartamentoController : Controller
    {
        public IActionResult GetAll()
        {

            Negocio.Result result = Negocio.Departamento.GetAll();

            Negocio.Departamento departamento = new Negocio.Departamento();


            if (result.Correct)
            {
                departamento.Departamentos = result.Objects;
            }
            return View(departamento);
        }
    }
}
