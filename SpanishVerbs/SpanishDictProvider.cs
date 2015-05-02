using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SpanishVerbs
{
    public class SpanishDictProvider : ProviderBase, IConjugationProvider
    {
        public SpanishDictProvider(string providerUrl)
            : base(providerUrl)
        {

        }

        public override string GetGerund(string rawData)
        {
            throw new NotImplementedException();
        }

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

        public override string GetKeyword(Tense tense)
        {
            switch (tense)
            {
                case Tense.PresentPerfect:
                    return "Present Perfect";
                case Tense.PastPerfect:
                    return "Past Perfect";
                case Tense.FuturePerfect:
                    return "Future Perfect";
                case Tense.ConditionalPerfect:
                    return "Conditional Perfect";
                case Tense.PreteritePerfect:
                    return "Preterite Perfect";
                default:
                    return tense.ToString();
            }
        }

        public override Dictionary<Person, string> ExtractConjugationFromMatches(MatchCollection matchCollection)
        {
            Dictionary<Person, string> conjugation = new Dictionary<Person, string>();
            if (matchCollection.Count < 6)
                return conjugation;

            for (int i = 0; i < 6; i++)
            {
                conjugation.Add((Person)i, matchCollection[i].Groups[2].Value.Trim());
            }

            return conjugation;
        }

        public override MatchCollection FindMatchesPerTense(string page, string tenseKeyword)
        {
            Regex rxTense = new Regex(string.Format(@"class=""tense_heading"">\s*<a[^>]*title=""[\s\w-]+""[^>]*>({0}).*\s*</td>\s*(<td\s+class=""conjugation[^""]*[^>]*>(\w+|\w+<div[^>]*>OR</div>\w+)[^<]*</td>\s+)+</tr>", tenseKeyword));
            Regex rxRow = new Regex(@"<td\s+class=""conjugation (irregular)*""[^>]*>([\w\s&;]+|[\w\s&;]+<div[^>]*>OR</div>[\w\s&;]+)[^<]*</td>\s+");

            Match tenseMatch = rxTense.Match(page);

            return rxRow.Matches(tenseMatch.Value);
        }

    }
}
