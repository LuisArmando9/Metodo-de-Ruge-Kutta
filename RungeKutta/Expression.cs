using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SevenZ.Calculator;

namespace RungeKutta
{
    class Expression
    {

        static Calculator calculator = new Calculator();
        static public string GetFinalExpression(string expression, string valueX, string valueY)
        {
            
            string FinalExpression = expression.ToLower().Replace("x", valueX).
                                            Replace("y", valueY);
            return FinalExpression;
        }
        static public bool IsValideMathExpression(string expression, string valueX, string valueY)
        {
            double parseExpression = 0;

            try
            {
                 parseExpression = calculator.Evaluate(GetFinalExpression(expression, valueX,valueY));
            }
            catch (CalculateException ex)
            {
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
            if (Double.IsNaN(parseExpression) || Double.IsInfinity(parseExpression))
                return false;
            return true;
           

        }
    }
}
