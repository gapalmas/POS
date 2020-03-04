using POS.ViewModels;

namespace POS.Locator
{
    public class ServiceLocator
    {
        public MainViewModel Main { get; set; }

        public ServiceLocator()
        {
            this.Main = new MainViewModel();
        }
    }
}
