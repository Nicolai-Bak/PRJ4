using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseLibrary.Models;
using ExternalApiLibrary.DTO;

namespace ExternalApiLibrary.Converters.Interfaces
{
    public interface IConverter
    {
        public List<IDbModelsDto> Convert(List<IFilteredDto> list);
    }
}
