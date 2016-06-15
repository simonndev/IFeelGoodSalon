using Fluent;
using IFeelGoodSalon.Infrastructure.Views;

namespace IFeelGoodSalon.Desktop.Views
{
    /// <summary>
    /// Interaction logic for ShellView.xaml
    /// </summary>
    public partial class ShellView : RibbonWindow, IShell
    {
        public ShellView()
        {
            InitializeComponent();
        }
    }
}
