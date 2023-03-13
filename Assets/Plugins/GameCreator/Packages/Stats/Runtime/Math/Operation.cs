using System;
using System.Text.RegularExpressions;

namespace GameCreator.Runtime.Stats.Formulas
{
    public class Operation
    {
        // MEMBERS: -------------------------------------------------------------------------------
        
        private readonly Func<Domain, string, string> m_Function;
        private readonly Regex m_Regex;

        // CONSTRUCTORS: --------------------------------------------------------------------------
        
        public Operation(string pattern, Func<Domain, string, string> function)
        {
            this.m_Regex = new Regex(pattern);
            this.m_Function = function;
        }
        
        // PUBLIC METHODS: ------------------------------------------------------------------------

        public Match Match(string input)
        {
            return this.m_Regex.Match(input);
        }

        public string Run(Domain data, string clause)
        {
            return this.m_Function.Invoke(data, clause);
        }
    }
}