using System;

using ViewModel;
using ViewModel.Entities;

namespace TechnologicalView
{
    class Program
    {
        static void Main(string[] args)
        {
            AuthView startupView = new AuthView();
            startupView.Run();
        }
    }
}
