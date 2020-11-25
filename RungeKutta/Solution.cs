using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SevenZ.Calculator;


namespace RungeKutta
{
    class Solution
    {

        private const int MAX_ITERATIONS = 10;
        private Calculator calculator = new Calculator();
       
   
        
        public double ToRoundDecimal(double num)
        {
            if (Double.IsNaN(num) || Double.IsInfinity(num))
                return 0;
            double result = Math.Round(num, 5);
            return result;
        }
       
        private double CalculateFunction(string expression, double x,double y, double h)
        {


            double functionResult = calculator.Evaluate(Expression.GetFinalExpression(expression,
                x.ToString(), y.ToString()));
            return h* ToRoundDecimal(functionResult);

        }
     
   
        public Coordinates GetCoordinates(List<SolutionVariables> table)
        {
           
            return (new Coordinates { 
                    X = table.Select(var => var.X).ToArray(),
                    Y= table.Select(var => var.Y).ToArray()});  
        }
        
        public List<SolutionVariables> GetTable(string expression, string x, string y, string h)
        {
            List<SolutionVariables> SolutionTable = new List<SolutionVariables>();
         
             double VAL_H = double.Parse(h);
            double x0 = double.Parse(x);
            double y0 = double.Parse(y);

            for (int i = 1; i <= MAX_ITERATIONS; i++)
            {
                SolutionVariables aux = new SolutionVariables();
                aux.K1 = CalculateFunction(expression, x0, y0, VAL_H);
                aux.K2 = CalculateFunction(expression, (x0 + (VAL_H / 2)), (y0 + (aux.K1 / 2)), VAL_H);
                aux.K3 = CalculateFunction(expression, (x0 + (VAL_H / 2)), (y0 + (aux.K2 / 2)), VAL_H);
                aux.K4 = CalculateFunction(expression, (x0 + VAL_H), (y0 + aux.K3), VAL_H);
                aux.Y = y0 + (aux.K1 + 2 * aux.K2 + 2 * aux.K3 + aux.K4)/6;
                aux.X = x0;
                x0 += VAL_H;
                y0 = aux.Y;
            
                SolutionTable.Add(aux);
            }
            return SolutionTable;
        }
    }
}
