using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    static public class LastOperation
    {
        static public double Number = 0;
        static public State lastSymbol = State.Void;

        static public void Remember(double num, State state)
        {
            Number = num;
            lastSymbol = state;
        }

        public enum State
        {
            Void,
            Plus,
            Minus,
            Umnoj,
            Delen
        }
    }
}
