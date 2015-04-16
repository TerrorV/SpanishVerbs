using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SpanishVerbs
{
    public class BabLaProvider:ProviderBase, IConjugationProvider
    {
        //public Verb GetConjugation(string rawData)
        //{
        //    Verb verb = new Verb();
        //    verb.Present = GetConjugationPerTense(rawData, Tense.Present);
        //    verb.PresentPerfect = GetConjugationPerTense(rawData, Tense.PresentPerfect);
        //    verb.Imperfect = GetConjugationPerTense(rawData, Tense.Imperfect);
        //    verb.Preterite = GetConjugationPerTense(rawData, Tense.Preterite);
        //    verb.PastPerfect = GetConjugationPerTense(rawData, Tense.PastPerfect);
        //    verb.Future = GetConjugationPerTense(rawData, Tense.Future);
        //    verb.FuturePerfect = GetConjugationPerTense(rawData, Tense.FuturePerfect);
        //    verb.Conditional = GetConjugationPerTense(rawData, Tense.Conditional);
        //    verb.ConditionalPerfect = GetConjugationPerTense(rawData, Tense.ConditionalPerfect);
        //    verb.PreteritePerfect = GetConjugationPerTense(rawData, Tense.PreteritePerfect);
        //    verb.PresentParticiple = GetGerund(rawData);

        //    if(! ValidateVerb(verb))
        //    {
        //        throw new Exception();
        //    }

        //    return verb;
        //}

        public override bool ValidateVerb(Verb verb)
        {
            bool isValid = verb.Future.Count > 0 ||
                verb.Conditional.Count > 0 ||
                verb.ConditionalPerfect.Count > 0 ||
                verb.FuturePerfect.Count > 0 ||
                verb.Future.Count > 0 ||
                verb.Imperfect.Count > 0 ||
                verb.PastPerfect.Count > 0 ||
                verb.Present.Count > 0 ||
                verb.PresentPerfect.Count > 0 ||
                verb.Preterite.Count > 0 ||
                verb.PreteritePerfect.Count > 0;

                return isValid;
        }

        public override string GetGerund(string rawData)
        {
            Regex rxGerunds = new Regex(@"<h5 [^>]*>(Gerundio)</h5>(<span [^>]*>([\w/]*)</span>\s*([\s\w]*)(<br>)*)*");
            MatchCollection matchesGerund = rxGerunds.Matches(rawData);
            if (matchesGerund.Count == 0)
                return string.Empty;

            return matchesGerund[0].Groups[matchesGerund[0].Groups.Count - 2].Value;
        }

        //private Dictionary<Person, string> GetConjugationPerTense(string rawData, Tense tense)
        //{
        //    //switch (tense)
        //    //{
        //    //    case Tense.Present:
        //    //    //break;
        //    //    case Tense.PresentPerfect:
        //    //    //break;
        //    //    case Tense.Imperfect:
        //    //    //break;
        //    //    case Tense.Preterite:
        //    //    //break;
        //    //    case Tense.PastPerfect:
        //    //        break;
        //    //    case Tense.Future:
        //    //        break;
        //    //    case Tense.FuturePerfect:
        //    //        break;
        //    //    case Tense.Conditional:
        //    //        break;
        //    //    case Tense.ConditionalPerfect:
        //    //        break;
        //    //    case Tense.PreteritePerfect:
        //    //        break;
        //    //    default:
        //    //        //FindMatchesPerTense(tense);
        //    //        break;
        //    //}

        //    return ExtractConjugationFromMatches(FindMatchesPerTense(rawData, GetKeyword(tense)));
        //}

        public override string GetKeyword(Tense tense)
        {
            switch (tense)
            {
                case Tense.Present:
                    return "Indicativo presente";
                case Tense.PresentPerfect:
                    return "Indicativo pretérito perfecto compuesto";
                case Tense.Imperfect:
                    return "Indicativo pretérito imperfecto";
                case Tense.Preterite:
                    return "Indicativo pretérito perfecto simple";
                case Tense.PastPerfect:
                    return "Indicativo pretérito pluscuamperfecto";
                case Tense.Future:
                    return "Indicativo futuro";
                case Tense.FuturePerfect:
                    return "Indicativo futuro perfecto";
                case Tense.Conditional:
                    return "Condicional simple";
                case Tense.ConditionalPerfect:
                    return "Condicional compuesto";
                case Tense.PreteritePerfect:
                    return "Indicativo pretérito anterior";
                default:
                    break;
            }
            return tense.ToString();
        }

        public override Dictionary<Person, string> ExtractConjugationFromMatches(MatchCollection matchCollection)
        {
            Dictionary<Person, string> conjugation = new Dictionary<Person, string>();
            if (matchCollection.Count < 6)
                return conjugation;

            for (int i = 0; i < 6; i++)
            {
                conjugation.Add((Person)i, matchCollection[i].Groups[2].Value);
            }

            return conjugation;
        }

        public override MatchCollection FindMatchesPerTense(string page, string tenseKeyword)
        {
            Regex rxTense = new Regex(string.Format(@"<h5 [^>]*>({0})</h5>(<span [^>]*>([\w/]*)</span>\s*([\s\w]*)(<br>)*)*", tenseKeyword));

            Regex rxRow = new Regex(@"<span [^>]*>([\w/]*)</span>\s*([\s\w]*)(<br>)*");

            Match tenseMatch = rxTense.Match(page);

            return rxRow.Matches(tenseMatch.Value);
        }

    }
}
