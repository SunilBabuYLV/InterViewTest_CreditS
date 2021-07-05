using System;
using System.Collections.Concurrent;
using AssetPriceModule.Common;

namespace AssetPriceModule.ViewModel
{
    public interface IPriceInfoViewModel
    {
        string AssetName { get; set; }
        decimal Price { get; set; }
        decimal AvgPrice { get; }
        Momentum PriceMomentum { get; set; }
        Momentum AvgPriceMomentum { get; set; }
        ObservableConcurrentQueue<decimal> PriceHistory { get; }
        Guid UniqueId { get; set; }
    }
}