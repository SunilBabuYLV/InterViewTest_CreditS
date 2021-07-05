using System.Windows;
using AssetPriceModule.ViewModel;
using System.Windows.Controls;

namespace AssetPriceModule.View
{
    /// <summary>
    /// Interaction logic for IAssetPriceHistoryView.xaml
    /// </summary>
    public partial class AssetPriceHistoryView : Window, IAssetPriceHistoryView
    {

        public AssetPriceHistoryView(IPriceInfoViewModel viewModel)
            : this()
        {
            DataContext = viewModel;
        }

        private AssetPriceHistoryView()
        {
            InitializeComponent();
        }

        public IPriceInfoViewModel ViewModel
        {
            get => DataContext as IPriceInfoViewModel;
            set => DataContext = value;
        }
    }
}
