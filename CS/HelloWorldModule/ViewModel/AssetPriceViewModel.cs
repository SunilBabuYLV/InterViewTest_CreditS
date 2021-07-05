using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using AssetPriceModule.Common;
using AssetPriceModule.View;
using Infrastructure.Events;
using Infrastructure.Services.Contract;
using Microsoft.Practices.Prism.Events;

namespace AssetPriceModule.ViewModel
{

    public class AssetPriceViewModel : INotifyPropertyChanged, IAssetPriceViewModel
    {
        private readonly object _locker = new object();
        private ObservableCollection<PriceInfoViewModel> _assetsCollection;
        private readonly IEventAggregator _eventAggregator;

        public AssetPriceViewModel(IEventAggregator eventAggregator, IPriceFileReader svc)
        {
            AssetsCollection = new ObservableCollection<PriceInfoViewModel>();
            AssetsCollection.CollectionChanged += AssetsCollection_CollectionChanged;
            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<NewDataAvailableEvent>().Subscribe(ProcessNewData, ThreadOption.BackgroundThread);
            ShowPriceHistoryCmd = new RelayCommand(ExecuteShowPriceHistoryCmd);
            svc.ReadContinuously();
        }

        public ObservableCollection<PriceInfoViewModel> AssetsCollection
        {
            get => _assetsCollection;

            private set
            {
                if (_assetsCollection != value)
                {
                    _assetsCollection = value;
                    OnPropertyChanged();
                }
            }
        }

        public ICommand ShowPriceHistoryCmd { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private void ExecuteShowPriceHistoryCmd(object obj)
        {
            var ds = obj as IPriceInfoViewModel;
            if (ds == null)
            {
                //Log this message...
                return;
            }
            var existingWindow = Application.Current.Windows.Cast<Window>().FirstOrDefault(k =>
                 ds.UniqueId.Equals(k.Tag));

            if (existingWindow == null)
            {
                existingWindow = new AssetPriceHistoryView(ds);

            }
            existingWindow.Show();
            existingWindow.Activate();
            existingWindow.Focus();
        }

        private void AssetsCollection_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged("AssetsCollection");
        }

        private async void ProcessNewData(IList<PriceDto> newData)
        {
            foreach (var newDataItem in newData)
            {
                var matchingAsset = AssetsCollection.FirstOrDefault(k => k.AssetName == newDataItem.AssetName);
                if (matchingAsset == null)
                {
                    matchingAsset = new PriceInfoViewModel { AssetName = newDataItem.AssetName };
                    lock (_locker)
                    {
                        AssetsCollection.Add(matchingAsset);
                    }
                }

                matchingAsset.Price = newDataItem.Price;
            }
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        ~AssetPriceViewModel()
        {
            _eventAggregator.GetEvent<NewDataAvailableEvent>().Unsubscribe(ProcessNewData);
        }
    }
}