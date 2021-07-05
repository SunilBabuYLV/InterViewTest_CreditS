using System.Collections.Generic;
using Infrastructure.Services.Contract;
using Microsoft.Practices.Prism.Events;

namespace Infrastructure.Events
{
    public class NewDataAvailableEvent : CompositePresentationEvent<IList<PriceDto>>
    { }
}
