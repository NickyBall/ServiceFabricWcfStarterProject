using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorService
{
    public class Calculator : ICalculatorService
    {
        public Task<double> Add(double a, double b) => Task.FromResult(a + b);
    }
}
