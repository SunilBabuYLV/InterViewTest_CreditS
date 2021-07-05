using Infrastructure.Events;

using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Infrastructure.Services.Contract;
using Microsoft.Practices.Prism.Events; 

namespace Infrastructure.Services
{
    public class PriceFileReader : IPriceFileReader
    {
        private readonly IEventAggregator _eventAggregator;

        public PriceFileReader(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }

        public async Task ReadContinuously()
        {
            try
            {
                var str = @".\Sample Data.txt";
                var skipLines = 0;
                var readLineCount = 10;
                while (true)
                {
                    var data = File.ReadLines(str).Skip(skipLines).Take(readLineCount).ToList();

                    if (!data.Any() && skipLines != 0) //If file reaches end then start from begin.
                    {
                        skipLines = 0;
                        data = File.ReadLines(str).Skip(skipLines).Take(readLineCount).ToList();
                    }
                    skipLines = skipLines + readLineCount;

                    var newPriceData = data.Select(s => new PriceDto(s)).Where(k => !k.HasError).ToList();
                    //Data is coming from external file source, only consider valid data , invalid data is logged in to log file. 
                    
                    await Task.Factory.StartNew(() =>
                    {
                        _eventAggregator.GetEvent<NewDataAvailableEvent>().Publish(newPriceData);
                        Thread.Sleep(1000);
                    });
                    
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
