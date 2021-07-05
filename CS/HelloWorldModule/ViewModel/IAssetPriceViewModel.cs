using System.Collections.ObjectModel;
using System.Windows.Input;

namespace AssetPriceModule.ViewModel
{
    public interface IAssetPriceViewModel
    {
        ObservableCollection<PriceInfoViewModel> AssetsCollection { get; }
        ICommand ShowPriceHistoryCmd { get; set; }
    }
}