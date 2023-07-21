using System.Linq;
using System.Windows.Forms;

namespace UI_BackOffice.Tools
{
    public static class ArchivoHelper
    {
        /// <summary>
        /// Ejecuta el OpenFileDialod para seleccionar los documentos a Importar, necesita que se le indique la extención de los
        /// documentos a importar.
        /// </summary>
        /// <param name="formatos">Extensión de los formatos: ejemplo new string[]{"pdf","xml","docx","xlsx"}</param>
        /// <returns></returns>
        public static OpenFileDialog ImportarDocumento(string[] formatos, bool multiSelect = false, bool checkFileExists = false)
        {
            var archivos = string.Join(";", formatos.Select(f => $"*.{f}"));
            var filter = $"Archivos ({archivos})|{archivos}";

            using (var openFileDialog = new OpenFileDialog()
            {
                Filter = filter,
                FilterIndex = 1,
                InitialDirectory = "C:\\",
                Title = "Abrir archivo",
                CheckFileExists = checkFileExists,
                Multiselect = multiSelect
            }) { return openFileDialog; }
        }
    }
}
