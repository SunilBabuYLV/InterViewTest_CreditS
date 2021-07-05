using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.Contract
{
    public interface IPriceFileReader
    {
        Task ReadContinuously();
    }

    public class PriceDto
    {
        public string AssetName { get; private set; }
        public decimal Price { get; private set; }
        public bool HasError { get; private set; }
        public string ErrorMessage { get; private set; }


        public PriceDto(string rawData)
        {
            var splitString = rawData.Split(':');
            if (splitString.Length == 2)
            {
                AssetName = splitString[0];
                if (decimal.TryParse(splitString[1], out var priceValue))
                {
                    Price = priceValue;
                    return;
                }
            }

            HasError = true;
            ErrorMessage = $"Invalid data and its ignored . , log this into log file... Data received :{rawData}";
        }



    }
}
