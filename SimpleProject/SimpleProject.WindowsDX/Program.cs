using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using PlatformerGameProject.Core;
using PuzzleGameProject.Core;

internal class Program
{
    /// <summary>
    /// The main entry point for the application on Windows.
    /// Configures the application for high DPI awareness.
    /// It also creates an instance of your game and calls it's Run() method 
    /// </summary>
    /// <param name="args">Command-line arguments passed to the application.</param>
    private static void Main(string[] args)
    {
        // Configure the application to be DPI-aware for better display scaling.
        Application.SetHighDpiMode(HighDpiMode.SystemAware);

        int i = 0;
        List<Type> _types =
        [
            typeof(PlatformerGame),
            typeof(PuzzleGame)
        ];
        using var executable = (Game)Activator.CreateInstance(_types[i])!;
        executable.Run();
    }
}