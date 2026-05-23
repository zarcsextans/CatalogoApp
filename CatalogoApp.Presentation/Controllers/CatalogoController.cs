using CatalogoApp.Domain.Models;
using CatalogoApp.Presentation.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace CatalogoApp.Presentation.Controllers
{
    public class CatalogoController : Controller
    {
        private readonly string rutaItems =
            "Data/videojuegos.json";

        private readonly string rutaReviews =
            "Data/reviews.json";

        public IActionResult Index(string? genero)
        {
            var items =
                JsonHelper.Leer<Item>(
                    rutaItems);

            var resultado =
                string.IsNullOrEmpty(genero)
                ? items
                : items.Where(i => i.Genero == genero)
                      .ToList();

            ViewBag.Generos =
                items.Select(i => i.Genero)
                     .Distinct()
                     .ToList();

            return View(resultado);
        }

        public IActionResult Detalle(int id)
        {
            var items =
                JsonHelper.Leer<Item>(
                    rutaItems);

            var item =
                items.FirstOrDefault(i => i.Id == id);

            if (item == null)
            {
                return NotFound();
            }

            var reviews =
                JsonHelper.Leer<Review>(
                    rutaReviews);

            ViewBag.Reviews =
                reviews.Where(r => r.ItemId == id)
                       .ToList();

            return View(item);
        }

        // ✅ AGREGAR VIDEOJUEGO (GET)
        public IActionResult Agregar()
        {
            return View();
        }

        // ✅ AGREGAR VIDEOJUEGO (POST)
        [HttpPost]
        public IActionResult Agregar(Item item)
        {
            var items =
                JsonHelper.Leer<Item>(
                    rutaItems);

            item.Id =
                items.Count > 0
                ? items.Max(i => i.Id) + 1
                : 1;

            items.Add(item);

            JsonHelper.Guardar(
                rutaItems,
                items);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult AgregarReview(
            int itemId,
            string comentario,
            int estrellas)
        {
            var usuario =
                HttpContext.Session.GetString("Usuario");

            if (usuario == null)
            {
                return RedirectToAction("Index", "Login");
            }

            var reviews =
                JsonHelper.Leer<Review>(
                    rutaReviews);

            reviews.Add(new Review
            {
                ItemId = itemId,
                Usuario = usuario,
                Comentario = comentario,
                Estrellas = estrellas
            });

            JsonHelper.Guardar(
                rutaReviews,
                reviews);

            return RedirectToAction(
                "Detalle",
                new { id = itemId });
        }
    }
}