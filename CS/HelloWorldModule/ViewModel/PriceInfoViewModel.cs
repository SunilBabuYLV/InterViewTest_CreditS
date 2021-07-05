using System;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using AssetPriceModule.Common;

namespace AssetPriceModule.ViewModel
{
    public class PriceInfoViewModel : INotifyPropertyChanged, IPriceInfoViewModel
    {
        private string _assetName;

        private decimal _avgPrice;
        private readonly int _avgPriceForItems = 5;
        private Momentum _avgPriceMomentum;
        private readonly int _maxAllowedPriceHistory = 10;
        private decimal _price;
        private ObservableConcurrentQueue<decimal> _priceHistory;
        private Momentum _priceMomentum;

        public PriceInfoViewModel()
        {
            PriceHistory = new ObservableConcurrentQueue<decimal>();
            UniqueId = Guid.NewGuid();
        }

        public string AssetName
        {
            get => _assetName;
            set
            {
                if (_assetName != value)
                {
                    _assetName = value;
                    OnPropertyChanged();
                }
            }
        }

        public decimal Price
        {
            get => _price;
            set
            {
                // if (_price != value) //SY : We don't check for this, because price can be same for next cycle. but avg price can change.
                PriceMomentum = CalcMomentum(_price, value);
                _price = value;

                UpdateAssetPriceHistory(value);
                OnPropertyChanged();
            }
        }


        public decimal AvgPrice
        {
            get => _avgPrice;
            private set
            {
                // if (_avgPrice != value) //SY : We don't check for this, because avg price can be same but Momentum /direction can change
                AvgPriceMomentum = CalcMomentum(_avgPrice, value);
                _avgPrice = value;
                OnPropertyChanged();
            }
        }

        public Momentum PriceMomentum
        {
            get => _priceMomentum;
            set
            {
                if (_priceMomentum != value)
                {
                    _priceMomentum = value;
                    OnPropertyChanged();
                }
            }
        }


        public Momentum AvgPriceMomentum
        {
            get => _avgPriceMomentum;
            set
            {
                if (_avgPriceMomentum != value)
                {
                    _avgPriceMomentum = value;
                    OnPropertyChanged();
                }
            }
        }

        public ObservableConcurrentQueue<decimal> PriceHistory
        {
            get => _priceHistory;
            private set
            {
                if (_priceHistory != value)
                {
                    _priceHistory = value;
                    OnPropertyChanged();
                }
            }
        }

        public Guid UniqueId { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void UpdateAssetPriceHistory(decimal newPrice)
        {
            PriceHistory.Enqueue(newPrice);
            if (PriceHistory.Count > _maxAllowedPriceHistory) 
                PriceHistory.Dequeue();
            OnPropertyChanged("PriceHistory");

            AvgPrice = PriceHistory.Count > _avgPriceForItems
                ? PriceHistory.Skip(PriceHistory.Count - _avgPriceForItems).Average()
                : PriceHistory.Average();
        }

        private Momentum CalcMomentum(decimal previousValue, decimal newValue)
        {
            if (previousValue == newValue)
                return Momentum.None;
            if (previousValue > newValue)
                return Momentum.Down;
            return Momentum.Up;
        }

        public bool Equals(PriceInfoViewModel x, PriceInfoViewModel y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (ReferenceEquals(x, null)) return false;
            if (ReferenceEquals(y, null)) return false;
            if (x.GetType() != y.GetType()) return false;
            return x._assetName == y._assetName;
        }

        public int GetHashCode(PriceInfoViewModel obj)
        {
            return obj._assetName != null ? obj._assetName.GetHashCode() : 0;
        }
    }
}