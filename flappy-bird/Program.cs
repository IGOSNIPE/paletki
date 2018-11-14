using System;

namespace flappy_bird
{
#if WINDOWS || LINUX
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            FlappyMain game = new FlappyMain();
            game.Run();
        }
    }
#endif
}
