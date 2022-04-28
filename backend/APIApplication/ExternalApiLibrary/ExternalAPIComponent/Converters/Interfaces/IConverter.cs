using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExternalApiLibrary.DTO;

namespace ExternalApiLibrary.ExternalAPIComponent.Converters.Interfaces
{
    public interface IConverter
    {
        public List<object> Convert(List<IFilteredDto> list);
    }
}
