﻿using System;

namespace GameProject4
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new GameProject4())
                game.Run();
        }
    }
}