namespace MontyTask.Helpers;

public static class Helpers
{
    public static List<List<T>> Split<T>(IList<T> source, int range)
    {
        return source
            .Select((x, i) => new { Index = i, Value = x })
            .GroupBy(x => x.Index / range)
            .Select(x => x.Select(v => v.Value).ToList())
            .ToList();
    }
    public static string ExtractMessage(this Exception ex)
    {
        if (ex.InnerException != null)
            return $"An exception was thrown of type {ex.GetType()} \n Message: " + ex.Message + "\n Inner exception :" + ex.InnerException.Message;
        return $"An exception was thrown of type {ex.GetType()} \n Message: " + ex.Message;
    }
}
