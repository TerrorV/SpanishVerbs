using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SpanishVerbs
{
    public class TeachMeProvider : SpanishVerbs.IConjugationProvider
    {
        public Verb GetConjugation(string rawData)
        {
            Verb verb = new Verb();
            verb.Present = GetConjugationPerTense(rawData, Tense.Present);
            verb.PresentPerfect = GetConjugationPerTense(rawData, Tense.PresentPerfect);
            verb.Imperfect = GetConjugationPerTense(rawData, Tense.Imperfect);
            verb.Preterite = GetConjugationPerTense(rawData, Tense.Preterite);
            verb.PastPerfect = GetConjugationPerTense(rawData, Tense.PastPerfect);
            verb.Future = GetConjugationPerTense(rawData, Tense.Future);
            verb.FuturePerfect = GetConjugationPerTense(rawData, Tense.FuturePerfect);
            verb.Conditional = GetConjugationPerTense(rawData, Tense.Conditional);
            verb.ConditionalPerfect = GetConjugationPerTense(rawData, Tense.ConditionalPerfect);
            verb.PreteritePerfect = GetConjugationPerTense(rawData, Tense.PreteritePerfect);
            verb.PresentParticiple = GetGerund(rawData);

            return verb;
        }

        private string GetGerund(string rawData)
        {
            Regex rxTense = new Regex(@"class=""tense_heading"">\s*<a[^>]*title=""[\s\w-]+""[^>]*>(Present Participle).*\s*</td>\s*(<td\s+class=""conjugation[^""]*[^>]*>(\w+|\w+<div[^>]*>OR</div>\w+)[^<]*</td>\s+)+</tr>");
            Regex rxRow = new Regex(@"<td\s+class=""conjugation[^""]*[^>]*>(\w+|\w+<div[^>]*>OR</div>\w+)[^<]*</td>\s+");

            Match match = rxTense.Match(rawData);
            if (!match.Success)
                return string.Empty;

            return rxRow.Matches( match.Value)[1].Groups[1].Value;
        }

        private Dictionary<Person, string> GetConjugationPerTense(string rawData, Tense tense)
        {
            switch (tense)
            {
                case Tense.Present:
                    //break;
                case Tense.PresentPerfect:
                    //break;
                case Tense.Imperfect:
                    //break;
                case Tense.Preterite:
                    //break;
                case Tense.PastPerfect:
                    break;
                case Tense.Future:
                    break;
                case Tense.FuturePerfect:
                    break;
                case Tense.Conditional:
                    break;
                case Tense.ConditionalPerfect:
                    break;
                case Tense.PreteritePerfect:
                    break;
                default:
                    //FindMatchesPerTense(tense);
                    break;
            }

            return ExtractConjugationFromMatches(FindMatchesPerTense(rawData, GetKeyword(tense)));
        }

        private string GetKeyword(Tense tense)
        {
            return tense.ToString();
        }

        private Dictionary<Person, string> ExtractConjugationFromMatches(MatchCollection matchCollection)
        {
            Dictionary<Person, string> conjugation = new Dictionary<Person, string>();
            if (matchCollection.Count < 6)
                return conjugation;

            for (int i = 0; i < 6; i++)
            {
                conjugation.Add((Person)i, matchCollection[i].Groups[1].Value);
            }

            return conjugation;
        }

        MatchCollection FindMatchesPerTense(string page, string tenseKeyword)
        {
            Regex rxTense = new Regex(string.Format(@"class=""tense_heading"">\s*<a[^>]*title=""[\s\w-]+""[^>]*>({0}).*\s*</td>\s*(<td\s+class=""conjugation[^""]*[^>]*>(\w+|\w+<div[^>]*>OR</div>\w+)[^<]*</td>\s+)+</tr>",tenseKeyword));
            Regex rxRow = new Regex(@"<td\s+class=""conjugation ""[^>]*>(\w+|\w+<div[^>]*>OR</div>[\w&;]+)[^<]*</td>\s+");

            Match tenseMatch = rxTense.Match(page);

            return rxRow.Matches(tenseMatch.Value);
        }
    }
}
