namespace BaobabRidge;

internal static class Program
{
    // Starts the Windows Forms application on the main UI thread.
    [STAThread]
    private static void Main()
    {
        ApplicationConfiguration.Initialize();
        Application.Run(new MainForm());
    }
}
