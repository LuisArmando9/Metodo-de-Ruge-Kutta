using System;
using System.Collections.Generic;
using System.Text;
/*
 * For more information can visit 
 * https://www.codeproject.com/Articles/21137/Inside-the-Mathematical-Expressions-Evaluator?fbclid=IwAR1hQ-FwW-V0SW15g5IibLMKx6e3eGemcggLh6TWrkl6jrXcMpwNtFIUo9c
 * 
 * And downloand project
 * 
 */

namespace SevenZ.Calculator
{
   public partial class Calculator
   {
      public delegate void CalcVariableDelegate(object sender, EventArgs e);
      public event CalcVariableDelegate OnVariableStore;

      Dictionary<string, double> variables;

      public const string AnswerVar = "r";

      private void LoadConstants()
      {
         variables = new Dictionary<string, double>();
         variables.Add("pi", Math.PI);
         variables.Add("e", Math.E);
         variables.Add(AnswerVar, 0);

         if (OnVariableStore != null)
            OnVariableStore(this, new EventArgs());
      }

      public Dictionary<string, double> Variables
      {
         get { return variables; }
      }

      public void SetVariable(string name, double val)
      {
         if (variables.ContainsKey(name))
            variables[name] = val;
         else
            variables.Add(name, val);

         if (OnVariableStore != null)
            OnVariableStore(this, new EventArgs());
      }

      public double GetVariable(string name)
      {  // return variable's value // if variable ha push default value, 0
         return variables.ContainsKey(name) ? variables[name] : 0;                    
      }
   }
}
