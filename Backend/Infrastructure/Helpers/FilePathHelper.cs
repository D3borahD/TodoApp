using System.Text.Encodings.Web;
using System.Text.Json;

namespace BackendApi.Helpers;

public static class FilePathHelper
{
    public static string GetPath(string shortUrl)
    {
         return $"/Users/deborah/Documents/dev/TodoApp/Backend/DatasFiles/{shortUrl}";
    }
}