namespace MontyTask.Helpers;

public static class Helpers
{
    public static string ExtractMessage(this Exception ex)
    {
        if (ex.InnerException != null)
            return $"An exception was thrown of type {ex.GetType()} \n Message: " + ex.Message + "\n Inner exception :" + ex.InnerException.Message;
        return $"An exception was thrown of type {ex.GetType()} \n Message: " + ex.Message;
    }
}
