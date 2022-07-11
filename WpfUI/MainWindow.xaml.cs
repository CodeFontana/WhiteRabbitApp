using System.Windows;

namespace WhiteRabbit;
public partial class MainWindow : Window
{
    public MainWindow(object dataContext)
    {
        InitializeComponent();
        DataContext = dataContext;
    }
}
