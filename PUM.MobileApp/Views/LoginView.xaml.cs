using PUM.MobileApp.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace PUM.MobileApp.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LoginView : Page
    {
        private enum Orientation
        {
            Landscape,
            Portrait
        };

        private Orientation orientation;

        public LoginView()
        {
            this.InitializeComponent();
            
            orientation = Height > Width ? Orientation.Portrait : Orientation.Landscape;

            VisualStateManager.GoToState(this, orientation.ToString(), true);
        }

        public LoginViewModel ViewModel { get; private set; }

        private void Page_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var prev = orientation;
            orientation = e.NewSize.Height > e.NewSize.Width ? Orientation.Portrait : Orientation.Landscape;

            if (prev != orientation)
            {
                string s = orientation.ToString();
                string state = orientation == Orientation.Landscape ? "Landscape" : "Portrait";
                bool bTrans = VisualStateManager.GoToState(this, state, false);
                double size = LoginButton.FontSize;
            }




        }
    }
}
