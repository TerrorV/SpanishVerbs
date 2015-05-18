using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SpanishVerbs
{
    public abstract class ProviderBase<T> : SpanishVerbs.IProviderBase
    {
        string _providerUrl = string.Empty;
        public ProviderBase(string providerUrl)
        {
            _providerUrl = providerUrl;
        }

        public Verb GetConjugation(string verbString)
        {
            string rawData = CallWebSite(verbString);
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

        public Dictionary<Person, string> GetConjugationPerTense(string rawData, Tense tense)
        {
            return ExtractConjugationFromMatches(FindMatchesPerTense(rawData, GetKeyword(tense)));
        }

        public string CallWebSite(string verb)
        {
            HttpWebRequest HttpWReq =
            (HttpWebRequest)WebRequest.Create(string.Format(_providerUrl, verb));

            HttpWebResponse HttpWResp = (HttpWebResponse)HttpWReq.GetResponse();
            StreamReader sr = new StreamReader(HttpWResp.GetResponseStream());
            string result = sr.ReadToEnd();
            // Insert code that uses the response object.
            HttpWResp.Close();
            return result;
        }

        public abstract bool ValidateVerb(Verb verb);
        public abstract string GetGerund(string rawData);
        public abstract string GetKeyword(Tense tense);
        public abstract IEnumerable<T> FindMatchesPerTense(string page, string tenseKeyword);
        public abstract Dictionary<Person, string> ExtractConjugationFromMatches(IEnumerable<T> collection);
    }
}
