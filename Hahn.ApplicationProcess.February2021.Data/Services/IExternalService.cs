using Hahn.ApplicationProcess.February2021.Data.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hahn.ApplicationProcess.February2021.Data.Services
{
    public interface IExternalService
    {
        Task<List<CountryDTO>> GetCountryAsync(string countryName);
    }
}
