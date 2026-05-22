using System.Text.Json;

namespace CatalogoApp.Presentation.Helpers
{
    public static class JsonHelper
    {
        public static List<T> Leer<T>(
            string ruta)
        {
            if (!File.Exists(ruta))
            {
                return new List<T>();
            }

            var json =
                File.ReadAllText(ruta);

            return JsonSerializer.Deserialize<List<T>>(json)
                   ?? new List<T>();
        }

        public static void Guardar<T>(
            string ruta,
            List<T> datos)
        {
            var json =
                JsonSerializer.Serialize(
                    datos,
                    new JsonSerializerOptions
                    {
                        WriteIndented = true
                    });

            File.WriteAllText(ruta, json);
        }
    }
}