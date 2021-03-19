using System.Windows;

namespace SmileBoyClient
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            ViewModelLocator.Initialize();
        }
    }
}
