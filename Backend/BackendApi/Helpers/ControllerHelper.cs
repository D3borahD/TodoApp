using System.Text.Encodings.Web;
using System.Text.Json;

namespace BackendApi.Helpers;

public static class ControllerHelper
{
    public static string GetPath(string shortUrl)
    {
         string path = $"/Users/deborah/Documents/dev/TodoApp/Backend/DatasFiles/{shortUrl}";

         return path;
    }
}