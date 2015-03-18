using System;

namespace FishORama
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            Player player1 = new Player();
            using (Kernel game = new Kernel())
            {
                game.Run();
            }
        }
    }
#endif
}

