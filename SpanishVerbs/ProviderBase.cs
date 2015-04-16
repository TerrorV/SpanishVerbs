using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SpanishVerbs
{
    public abstract class ProviderBase
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


            if (!ValidateVerb(verb))
            {
                throw new Exception();
            }

            return verb;
        }
        private Dictionary<Person, string> GetConjugationPerTense(string rawData, Tense tense)
        {
            return ExtractConjugationFromMatches(FindMatchesPerTense(rawData, GetKeyword(tense)));
        }

        public abstract bool ValidateVerb(Verb verb);
        public abstract string GetGerund(string rawData);
        public abstract string GetKeyword(Tense tense);
        public abstract MatchCollection FindMatchesPerTense(string page, string tenseKeyword);
        public abstract Dictionary<Person, string> ExtractConjugationFromMatches(MatchCollection matchCollection);
    }
}
