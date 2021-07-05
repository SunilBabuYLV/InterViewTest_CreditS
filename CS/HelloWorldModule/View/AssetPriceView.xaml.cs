using System.Windows.Controls;
using AssetPriceModule.ViewModel;

namespace AssetPriceModule.View
{
    /// <summary>
    ///     Interaction logic for PricingTmpView.xaml
    /// </summary>
    public partial class AssetPriceView : UserControl, IAssetPriceView
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="AssetPriceView" /> class.
        /// </summary>
        public AssetPriceView()
        {
            InitializeComponent();
        }

        public AssetPriceView(IAssetPriceViewModel viewModel)
            : this()
        {
            DataContext = viewModel;
        }

        /// <summary>
        ///     Gets or sets the view model.
        /// </summary>
        /// <value>
        ///     The model.
        /// </value>
        public IAssetPriceViewModel ViewModel
        {
            get => DataContext as IAssetPriceViewModel;
            set => DataContext = value;
        }
    }
}