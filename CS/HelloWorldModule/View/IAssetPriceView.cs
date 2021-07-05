using AssetPriceModule.ViewModel;

namespace AssetPriceModule.View
{
    public interface IAssetPriceView
    {
        /// <summary>
        ///     Gets or sets the view model.
        /// </summary>
        /// <value>
        ///     The model.
        /// </value>
        IAssetPriceViewModel ViewModel { get; set; }
    }
}