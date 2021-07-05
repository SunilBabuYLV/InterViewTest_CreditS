using AssetPriceModule.ViewModel;

namespace AssetPriceModule.View
{
    public interface IAssetPriceHistoryView
    {
        IPriceInfoViewModel ViewModel { get; set; }
    }
}