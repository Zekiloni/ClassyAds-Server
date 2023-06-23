namespace ClassyAdsServer.Utils
{
    public class LoggerUtil
    {
        public static void Info(string message)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"[INFO] {message}");
            Console.ResetColor();
        }

        public static void Error(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"[ERROR] {message}");
            Console.ResetColor();
        }

        public static void Warn(string message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"[WARN] {message}");
            Console.ResetColor();
        }
    }
}
