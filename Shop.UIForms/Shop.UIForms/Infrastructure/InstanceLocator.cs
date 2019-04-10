namespace Shop.UIForms.Infrastructure
{
   using ViewModels;

    public class InstanceLocator
    {
        public MainViewModel mainViewModel { get; set; }

        public InstanceLocator()
        {
            mainViewModel = new MainViewModel();
        }
    }

}
