namespace WpfApp
{
    using OmniGui;
    using Common;

    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();

            DataContext = new SampleViewModel(new WpfMessageBoxService());
        }        
    }
}