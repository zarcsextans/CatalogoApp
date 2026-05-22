using CatalogoApp.Domain.Models;
using CatalogoApp.Presentation.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace CatalogoApp.Presentation.Controllers
{
    public class LoginController : Controller
    {
        private readonly string rutaUsuarios =
            "Data/usuarios.json";

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Entrar(
            string usuario,
            string password)
        {
            var usuarios =
                JsonHelper.Leer<Usuario>(
                    rutaUsuarios);

            var existe = usuarios.FirstOrDefault(
                u => u.UsuarioNombre == usuario
                  && u.Password == password);

            if (existe == null)
            {
                ViewBag.Error =
                    "Usuario o contraseña incorrectos";

                return View("Index");
            }

            HttpContext.Session.SetString(
                "Usuario",
                usuario);

            return RedirectToAction(
                "Index",
                "Catalogo");
        }

        public IActionResult Registro()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Registro(
            string usuario,
            string password)
        {
            var usuarios =
                JsonHelper.Leer<Usuario>(
                    rutaUsuarios);

            var existe = usuarios.Any(
                u => u.UsuarioNombre == usuario);

            if (existe)
            {
                ViewBag.Error =
                    "Ese usuario ya existe";

                return View();
            }

            usuarios.Add(new Usuario
            {
                UsuarioNombre = usuario,
                Password = password
            });

            JsonHelper.Guardar(
                rutaUsuarios,
                usuarios);

            return RedirectToAction(
                "Index");
        }

        public IActionResult Salir()
        {
            HttpContext.Session.Clear();

            return RedirectToAction(
                "Index",
                "Catalogo");
        }
    }
}