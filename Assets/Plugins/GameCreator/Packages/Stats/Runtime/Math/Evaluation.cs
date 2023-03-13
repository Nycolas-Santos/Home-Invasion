using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.Stats.Formulas;
using GameCreator.Runtime.Variables;

namespace GameCreator.Runtime.Stats
{
    public static class Evaluation
    {
        private static readonly Regex RX_IS_VAR_CHECK = new Regex(@"^[a-zA-Z0-9-_\/]+$");
        private static readonly char[] ALLOWED_MATH_SYMBOLS = 
        {
            '0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
            '+', '-', '/', '*', '.', '(', ')'
        };

        private static readonly Regex RX_REMOVE_WHITESPACE = new Regex(@"\s+");
        private static readonly Regex RX_EXTRACT_NAME = new Regex(@"\b[^\[\]]+\[(.*)\]$");

        private static readonly Dictionary<char, char> DELIMITERS = new Dictionary<char, char>
        {
            { ')', '(' },
            { ']', '[' },
        };

        private static readonly CultureInfo CULTURE = CultureInfo.InvariantCulture;

        private static readonly Operation[] OPERATIONS = 
        {
            new Operation(@"^source.base\[\S+\]", Function_BaseSourceName),                        // base:source[name]
            new Operation(@"^source.stat\[\S+\]", Function_StatSourceName),                        // stat:source[name]
            new Operation(@"^source.attr\[\S+\]", Function_AttrSourceName),                        // attr:source[name]
            new Operation(@"^target.base\[\S+\]", Function_BaseTargetName),                        // base:target[name]
            new Operation(@"^target.stat\[\S+\]", Function_StatTargetName),                        // stat:target[name]
            new Operation(@"^target.attr\[\S+\]", Function_AttrTargetName),                        // attr:target[name]
            new Operation(@"^source.var\[\S+\]", Function_VariableSourceName),                     // var:source[name]
            new Operation(@"^target.var\[\S+\]", Function_VariableTargetName),                     // var:target[name]
            new Operation(@"^random\[\S+,\S+\]", Function_Random),                                 // random[min,max]
            new Operation(@"^dice\[\S+,\S+\]", Function_Dice),                                     // dice[rolls,sides]
            new Operation(@"^chance\[\S+\]", Function_Chance),                                     // chance[value]
            new Operation(@"^min\[\S+,\S+\]", Function_Min),                                       // min[a,b]
            new Operation(@"^max\[\S+,\S+\]", Function_Max),                                       // max[a,b]
            new Operation(@"^round\[\S+\]", Function_Round),                                       // round[value]
            new Operation(@"^floor\[\S+\]", Function_Floor),                                       // floor[value]
            new Operation(@"^ceil\[\S+\]", Function_Ceil),                                         // ceil[value]
            new Operation(@"^table.level\[\S+\]", Function_TableLevel),                            // table:level[value]
            new Operation(@"^table.value\[\S+\]", Function_TableValue),                            // table:value[value]
            new Operation(@"^table.increment\[\S+\]", Function_TableIncrement),                    // table:increment[value]
            new Operation(@"^table.current\[\S+\]", Function_TableExpForCurrentLevel),             // table:value:current[level]
            new Operation(@"^table.next\[\S+\]", Function_TableExpToNextLevel),                    // table:value:next[level]
            new Operation(@"^table.ratio\[\S+\]", Function_TableRatioForCurrentLevel),             // table:ratio:current[value]
        };

        private const string DEFAULT_VALUE = "0";
        
        // FUNCTION METHODS: ----------------------------------------------------------------------

        private static string Function_BaseSourceName(Domain data, string clause)
        {
            string name = Clause_ParseName(clause, data);
            return data.Source != null 
                ? data.Source.RuntimeStats.Get(name).Base.ToString(CULTURE) 
                : DEFAULT_VALUE;
        }
        
        private static string Function_StatSourceName(Domain data, string clause)
        {
            string name = Clause_ParseName(clause, data);
            return data.Source != null 
                ? data.Source.RuntimeStats.Get(name).Value.ToString(CULTURE) 
                : DEFAULT_VALUE;
        }

        private static string Function_AttrSourceName(Domain data, string clause)
        {
            string name = Clause_ParseName(clause, data);
            return data.Source != null 
                ? data.Source.RuntimeAttributes.Get(name).Value.ToString(CULTURE) 
                : DEFAULT_VALUE;
        }

        private static string Function_BaseTargetName(Domain data, string clause)
        {
            string name = Clause_ParseName(clause, data);
            return data.Target != null 
                ? data.Target.RuntimeStats.Get(name).Base.ToString(CULTURE) 
                : DEFAULT_VALUE;
        }
        
        private static string Function_StatTargetName(Domain data, string clause)
        {
            string name = Clause_ParseName(clause, data);
            return data.Target != null 
                ? data.Target.RuntimeStats.Get(name).Value.ToString(CULTURE) 
                : DEFAULT_VALUE;
        }

        private static string Function_AttrTargetName(Domain data, string clause)
        {
            string name = Clause_ParseName(clause, data);
            return data.Target != null 
                ? data.Target.RuntimeAttributes.Get(name).Value.ToString(CULTURE) 
                : DEFAULT_VALUE;
        }

        private static string Function_VariableSourceName(Domain data, string clause)
        {
            string name = Clause_ParseName(clause, data);
            if (data.Source == null) return DEFAULT_VALUE;
            
            LocalNameVariables variables = data.Source.gameObject.Get<LocalNameVariables>();
            if (variables == null) return DEFAULT_VALUE;

            object value = variables.Get(name);
            return Convert.ToSingle(value).ToString(CULTURE);
        }

        private static string Function_VariableTargetName(Domain data, string clause)
        {
            string name = Clause_ParseName(clause, data);
            if (data.Target == null) return DEFAULT_VALUE;

            LocalNameVariables variables = data.Target.gameObject.Get<LocalNameVariables>();
            if (variables == null) return DEFAULT_VALUE;

            object value = variables.Get(name);
            return Convert.ToSingle(value).ToString(CULTURE);
        }

        private static string Function_TableLevel(Domain data, string clause)
        {
            string value = Clause_ParseOneInput(clause, data);
            int input = (int) Math.Floor(double.Parse(value));
            
            return data.Table.CurrentLevel(input).ToString(CULTURE);
        }
        
        private static string Function_TableValue(Domain data, string clause)
        {
            string value = Clause_ParseOneInput(clause, data);
            int level = (int) Math.Floor(double.Parse(value));
            
            return data.Table.CumulativeExperienceForLevel(level).ToString(CULTURE);
        }
        
        private static string Function_TableIncrement(Domain data, string clause)
        {
            string value = Clause_ParseOneInput(clause, data);
            int level = (int) Math.Floor(double.Parse(value));
            
            return data.Table.ExperienceForLevel(level).ToString(CULTURE);
        }

        private static string Function_TableRatioForCurrentLevel(Domain data, string clause)
        {
            string value = Clause_ParseOneInput(clause, data);
            int input = (int) Math.Floor(double.Parse(value));
            
            return data.Table.RatioFromCurrentLevel(input).ToString(CULTURE);
        }

        private static string Function_TableExpForCurrentLevel(Domain data, string clause)
        {
            string value = Clause_ParseOneInput(clause, data);
            int input = (int) Math.Floor(double.Parse(value));
            
            return data.Table.ExperienceForCurrentLevel(input).ToString(CULTURE);
        }
        
        private static string Function_TableExpToNextLevel(Domain data, string clause)
        {
            string value = Clause_ParseOneInput(clause, data);
            int input = (int) Math.Floor(double.Parse(value));
            
            return data.Table.ExperienceToNextLevel(input).ToString(CULTURE);
        }

        private static string Function_Random(Domain data, string clause)
        {
            string[] parse = Clause_ParseTwoInputs(clause, data);
            float min = float.Parse(parse[0]);
            float max = float.Parse(parse[1]);
            return UnityEngine.Random.Range(min, max).ToString(CULTURE);
        }

        private static string Function_Dice(Domain data, string clause)
        {
            string[] parse = Clause_ParseTwoInputs(clause, data);

            int rolls = int.Parse(parse[0]);
            int sides = int.Parse(parse[1]);

            float amount = 0.0f;
            for (int i = 0; i < rolls; ++i)
            {
                amount += UnityEngine.Random.Range(1, sides + 1);
            }

            return amount.ToString(CULTURE);
        }

        private static string Function_Chance(Domain data, string clause)
        {
            string value = Clause_ParseOneInput(clause, data);
            float chance = float.Parse(value);

            float percent = UnityEngine.Random.Range(0f, 1f);
            return (chance <= percent ? 1 : 0).ToString(CULTURE);
        }

        private static string Function_Min(Domain data, string clause)
        {
            string[] parse = Clause_ParseTwoInputs(clause, data);

            double a = double.Parse(parse[0]);
            double b = double.Parse(parse[1]);
            return Math.Min(a, b).ToString(CULTURE);
        }

        private static string Function_Max(Domain data, string clause)
        {
            string[] parse = Clause_ParseTwoInputs(clause, data);
            double a = double.Parse(parse[0]);
            double b = double.Parse(parse[1]);
            return Math.Max(a, b).ToString(CULTURE);
        }

        private static string Function_Round(Domain data, string clause)
        {
            string input = Clause_ParseOneInput(clause, data);

            double value = Math.Round(double.Parse(input));
            return value.ToString(CULTURE);
        }

        private static string Function_Floor(Domain data, string clause)
        {
            string input = Clause_ParseOneInput(clause, data);
            double value = Math.Floor(double.Parse(input));
            return value.ToString(CULTURE);
        }

        private static string Function_Ceil(Domain data, string clause)
        {
            string input = Clause_ParseOneInput(clause, data);
            double value = Math.Ceiling(double.Parse(input));
            return value.ToString(CULTURE);
        }
        
        // CLAUSES: -------------------------------------------------------------------------------

        private static string Clause_ParseName(string clause, Domain data)
        {
            StringBuilder value = new StringBuilder();

            Match match = RX_EXTRACT_NAME.Match(clause);
            if (match.Success && match.Groups.Count == 2)
            {
                value = new StringBuilder(match.Groups[1].Value);
            }

            bool isVariableName = RX_IS_VAR_CHECK.IsMatch(value.ToString());

            if (!isVariableName) value = ParseFormula(value, data);
            return value.ToString();
        }

        private static string Clause_ParseOneInput(string clause, Domain data)
        {
            StringBuilder value = new StringBuilder();

            Match match = RX_EXTRACT_NAME.Match(clause);
            if (match.Success && match.Groups.Count == 2)
            {
                value = new StringBuilder(match.Groups[1].Value);
            }

            value = ParseFormula(value, data);
            return Evaluate.FromString(value.ToString()).ToString(CULTURE);
        }

        private static string[] Clause_ParseTwoInputs(string clause, Domain data)
        {
            StringBuilder value1 = new StringBuilder();
            StringBuilder value2 = new StringBuilder();

            Match match = RX_EXTRACT_NAME.Match(clause);

            if (match.Success && match.Groups.Count == 2)
            {
                List<StringBuilder> parameters = ExtractParameters(match.Groups[1].Value);
                if (parameters.Count == 2)
                {
                    value1 = parameters[0];
                    value2 = parameters[1];
                }
            }

            value1 = ParseFormula(value1, data);
            value2 = ParseFormula(value2, data);

            return new[]
            {
                Evaluate.FromString(value1.ToString()).ToString(CULTURE),
                Evaluate.FromString(value2.ToString()).ToString(CULTURE)
            };
        }

        private static List<StringBuilder> ExtractParameters(string content)
        {
            int parametersIndex = 0;
            List<StringBuilder> parameters = new List<StringBuilder>
            {
                new StringBuilder()
            };

            Stack<char> delimiters = new Stack<char>();
            foreach (char character in content)
            {
                switch (character)
                {
                    case ',':
                        if (delimiters.Count == 0)
                        {
                            parameters.Add(new StringBuilder());
                            parametersIndex += 1;
                        }
                        else
                        {
                            parameters[parametersIndex].Append(character);
                        }
                        break;

                    case '(':
                    case '[':
                        delimiters.Push(character);
                        parameters[parametersIndex].Append(character);
                        break;

                    case ')':
                    case ']':
                        if (delimiters.Count > 0 && delimiters.Peek() == DELIMITERS[character]) delimiters.Pop();
                        parameters[parametersIndex].Append(character);
                        break;

                    default:
                        parameters[parametersIndex].Append(character);
                        break;
                }
            }

            return parameters;
        }
        
        ///////////////////////////////////////////////////////////////////////////////////////////
        // STATIC & CONSTANTS: --------------------------------------------------------------------

        private const int MAX_ITERATIONS = 500;
        private static int NUM_ITERATIONS;

        // PUBLIC METHODS: ------------------------------------------------------------------------

        public static double Calculate(Traits source, Traits target, string formula, Table table)
        {
            NUM_ITERATIONS = 0;

            string stringFormula = RX_REMOVE_WHITESPACE.Replace(formula, string.Empty);
            if (string.IsNullOrEmpty(stringFormula)) return 0f;

            Domain data = new Domain(source, target, table);
            StringBuilder sbFormula = new StringBuilder(stringFormula);

            sbFormula = ParseFormula(sbFormula, data);
            return Evaluate.FromString(sbFormula.ToString());
        }

        // PRIVATE METHODS: -----------------------------------------------------------------------

        private static StringBuilder ParseFormula(StringBuilder sbFormula, Domain data)
        {
            if (NUM_ITERATIONS++ > MAX_ITERATIONS)
            {
                return new StringBuilder("-1");
            }

            if (sbFormula.Length == 0) return sbFormula;

            List<Expression> expressions = SplitClauses(sbFormula);
            sbFormula.Remove(0, sbFormula.Length);

            foreach (Expression expression in expressions)
            {
                StringBuilder sbTerm = expression.Term;
                string term = sbTerm.ToString();

                string mathExpression = IsMathExpression(term);
                if (mathExpression != string.Empty)
                {
                    sbFormula.Append('(').Append(mathExpression).Append(')');
                    if (expression.HasOperation())
                    {
                        sbFormula.Append(expression.Operation);
                    }

                    continue;
                }

                if (expression.NeedsEvaluation && expressions.Count > 1)
                {
                    StringBuilder result = ParseFormula(sbTerm, data);
                    sbFormula.Append('(').Append(result).Append(')');

                    if (expression.HasOperation())
                    {
                        sbFormula.Append(expression.Operation);
                    }

                    continue;
                }

                int operationsLength = OPERATIONS.Length;
                for (int i = 0; i < operationsLength; ++i)
                {
                    Match match = OPERATIONS[i].Match(term);
                    if (match.Success)
                    {
                        string result = OPERATIONS[i].Run(data, match.Value);
                        sbFormula.Append(result);

                        if (expression.HasOperation())
                        {
                            sbFormula.Append(expression.Operation);
                        }
                        
                        break;
                    }
                }
            }

            return sbFormula;
        }

        private static string IsMathExpression(string content)
        {
            bool atLeastNumber = false;
            int contentLength = content.Length;

            for (int i = 0; i < contentLength; ++i)
            {
                if (char.IsDigit(content[i])) atLeastNumber = true;

                bool characterAllowedFound = false;
                foreach (char allowedSymbol in ALLOWED_MATH_SYMBOLS)
                {
                    if (allowedSymbol == content[i])
                    {
                        characterAllowedFound = true;
                        break;
                    }
                }

                if (!characterAllowedFound) return string.Empty;
            }

            return atLeastNumber ? content : string.Empty;
        }

        private static List<Expression> SplitClauses(StringBuilder content)
        {
            List<Expression> expressions = new List<Expression>
            {
                new Expression()
            };
            
            int expressionIndex = 0;

            Stack<char> delimiters = new Stack<char>();

            if (content.Length > 1 && content[0] == '-')
            {
                content.Remove(0, 1);
                content.Insert(0, "0-");
            }

            for (int i = 0; i < content.Length; ++i)
            {
                switch (content[i])
                {
                    case '[':
                    case '(':
                        delimiters.Push(content[i]);
                        expressions[expressionIndex].Term.Append(content[i]);
                        break;

                    case ']':
                    case ')':
                        if (delimiters.Count > 0 && delimiters.Peek() == DELIMITERS[content[i]]) delimiters.Pop();
                        expressions[expressionIndex].Term.Append(content[i]);
                        break;

                    case '+':
                    case '-':
                    case '/':
                    case '*':
                        if (delimiters.Count == 0)
                        {
                            expressions[expressionIndex].Operation = content[i];
                            expressions.Add(new Expression());
                            expressionIndex += 1;
                        }
                        else
                        {
                            expressions[expressionIndex].Term.Append(content[i]);
                            expressions[expressionIndex].NeedsEvaluation = true;
                        }
                        break;

                    default:
                        expressions[expressionIndex].Term.Append(content[i]);
                        break;
                }
            }

            foreach (Expression expression in expressions)
            {
                StringBuilder term = expression.Term;
                while (term.Length > 1 && term[0] == '(' && term[^1] == ')')
                {
                    term.Remove(0, 1);
                    term.Remove(term.Length - 1, 1);
                }
            }

            return expressions;
        }
    }
}