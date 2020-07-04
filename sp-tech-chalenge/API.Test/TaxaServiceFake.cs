using API.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace API.Test
{
    public class TaxaServiceFake : ITaxaService
    {
        public Task<double> TaxaJuros()
        {
            return Task.FromResult(0.01);
        }
    }
}
