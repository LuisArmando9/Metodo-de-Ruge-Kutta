using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace RungeKutta
{
    class SolutionVariables
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double K1 { get; set; }
        public double K2 { get; set; }
        public double K3 { get; set; }
        public double K4 { get; set; }  
       
    }

    class Coordinates
    {
        public double[] X{ get; set; }
        public double[] Y { get; set; }
    }
}
