using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SpanishVerbs
{
    public class BabLaProvider:IConjugationProvider
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
            Regex rxGerunds = new Regex(@"Present Participle.*\s*(<td.*conjugation english[^>]*>.*</td>)*\s*(<td.*conjugation[^>]*>(.*)</td>)");
            MatchCollection matchesGerund = rxGerunds.Matches(rawData);

            return matchesGerund[0].Groups[matchesGerund[0].Groups.Count - 1].Value;
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

            for (int i = 0; i < 6; i++)
            {
                conjugation.Add((Person)i, matchCollection[i].Value);
            }

            return conjugation;
        }

        MatchCollection FindMatchesPerTense(string page, string tenseKeyword)
        {
            Regex rxVerbs = new Regex(@"<h5 [^>]*>(Indicativo presente|Indicativo pretérito perfecto compuesto|Indicativo pretérito perfecto simple)</h5>(<span [^>]*>([\w/]*)</span>\s*([\s\w]*)(<br>)*)*");

            Regex rxTense = new Regex(string.Format(@"class=""tense_heading"">\s*<a[^>]*title=""[\s\w-]+""[^>]*>({0}).*\s*</td>\s*(<td\s+class=""conjugation[^""]*[^>]*>(\w+|\w+<div[^>]*>OR</div>\w+)[^<]*</td>\s+)+</tr>", tenseKeyword));
            Regex rxRow = new Regex(@"<td\s+class=""conjugation[^""]*[^>]*>(\w+|\w+<div[^>]*>OR</div>\w+)[^<]*</td>\s+");

            Match tenseMatch = rxTense.Match(page);

            return rxRow.Matches(tenseMatch.Value);
        }

    }
}
