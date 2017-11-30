using HtmlAgilityPack;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SpanishVerbs
{
    public class BabLaProvider : ProviderBase<string>, IConjugationProvider
    {
        public BabLaProvider(string providerUrl)
            : base(providerUrl)
        {

        }
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
                verb.PreteritePerfect.Count > 0 ||
                verb.Imperative.Count > 0;

            return isValid;
        }

        public override string GetGerund(string rawData)
        {
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(rawData);

            if (doc.ParseErrors != null && doc.ParseErrors.Count() > 0)
            {
                // Handle any parse errors as required
            }
            List<string> tenseMatches = new List<string>();
            if (doc.DocumentNode == null)
            {
                return string.Empty;
            }

            HtmlNodeCollection words = doc.DocumentNode.SelectNodes(@"((//h3[contains(text(),'Gerundio')])[2]/../div/div[@class='conj-result'])");

            //    Regex rxTense = new Regex(@"<table>((?!table).)*(Gerundio)((?!table).)*</table>");
            //Regex rxGerunds = new Regex(@"<tr[^>]*>\s*<td[^>]*>([\w\s]+)</td>\s*</tr>");
            //var tenseMatch = rxTense.Match(rawData);
            //Regex rxGerunds = new Regex(@"<h5 [^>]*>(Gerundio)</h5>(<span [^>]*>([\w/]*)</span>\s*([\s\w]*)(<br>)*)*");
            //MatchCollection matchesGerund = rxGerunds.Matches(tenseMatch.Captures[0].Value);
            if (words.Count == 0)
                return string.Empty;

            return words.ElementAt(0).InnerText;
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

        public override Dictionary<Person, string> ExtractConjugationFromMatches(IEnumerable<string> matchCollection)
        {
            Dictionary<Person, string> conjugation = new Dictionary<Person, string>();
            if (matchCollection.Count() < 6)
                return conjugation;

            for (int i = 0; i < 6; i++)
            {
                conjugation.Add((Person)i, matchCollection.ElementAt(i));
            }

            return conjugation;
        }

        ////public Dictionary<Person, string> ExtractConjugationFromMatches(IEnumerable<string> matchCollection)
        ////{
        ////    Dictionary<Person, string> conjugation = new Dictionary<Person, string>();
        ////    if (matchCollection.Count() < 6)
        ////        return conjugation;

        ////    for (int i = 0; i < 6; i++)
        ////    {
        ////        string currentConjugation = matchCollection.ElementAt(i).Trim();
        ////        if (Regex.IsMatch(currentConjugation, @"\w+"))
        ////        {
        ////            conjugation.Add((Person)i, currentConjugation);
        ////        }
        ////    }

        ////    return conjugation;
        ////}



        public override IEnumerable<string> FindMatchesPerTense(string page, string tenseKeyword)
        {
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(page);

            if (doc.ParseErrors != null && doc.ParseErrors.Count() > 0 || doc.DocumentNode == null)
            {
                throw new FormatException(string.Format("Could not parse tense '{0}'", tenseKeyword));
                // Handle any parse errors as required
            }

            List<string> tenseMatches = new List<string>();
            //HtmlNode preteriteNode = doc.DocumentNode.SelectSingleNode(@"html/body/div[2]/div[2]/div[2]/div[4]");
            //string preterite = preteriteNode.SelectSingleNode("//span").InnerText;
            //HtmlNode conjugationTable = doc.DocumentNode.SelectSingleNode(@"html/body/div[2]/div[2]/div[2]/div[6]/table");
            HtmlNodeCollection words = doc.DocumentNode.SelectNodes(string.Format(@"((//h3[contains(text(),'{0}')])[1]/../div/div[@class='conj-result'])", tenseKeyword));
            HtmlNodeCollection words1 = doc.DocumentNode.SelectNodes(string.Format(@"((//h3[contains(text(),'{0}')])[1]/../div/div[@class='conj-result'])", tenseKeyword));
            int tenseIndex = 0;// GetTenseIndex(GetKeyword(Tense.Imperative));

            for (int i = 0; i < 6; i++)
            {
                //TODO i * 5 needs to be configurable to account for the Imperative and few other things (or extract it in another method)
                tenseMatches.Add(words.ElementAt(i + tenseIndex).InnerText);
            }
            //Regex rxTense = new Regex(string.Format(@"<table>((?!table).)*({0})((?!table).)*</table>", tenseKeyword));

            //Regex rxRow = new Regex(@"<tr[^>]*><td[^>]*>([\w/]+)</td>\s*<td[^>]*>([\w\s]+)</td>\s*</tr>");

            //Match tenseMatch = rxTense.Match(page);

            //return rxRow.Matches(tenseMatch.Value).Cast<Match>();
            return tenseMatches;
        }


        public override Dictionary<Person, string> GetImperative(string rawData)
        {
            string tenseKeyword;
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(rawData);

            if (doc.ParseErrors != null && doc.ParseErrors.Count() > 0)
            {
                // Handle any parse errors as required
            }
            List<string> tenseMatches = new List<string>();
            if (doc.DocumentNode != null)
            {
                //HtmlNode preteriteNode = doc.DocumentNode.SelectSingleNode(@"html/body/div[2]/div[2]/div[2]/div[4]");
                //string preterite = preteriteNode.SelectSingleNode("//span").InnerText;
                //HtmlNode conjugationTable = doc.DocumentNode.SelectSingleNode(@"html/body/div[2]/div[2]/div[2]/div[6]/table");
                HtmlNodeCollection words = doc.DocumentNode.SelectNodes(@"((//h3[contains(text(),'Imperativo')])[2]/../div/div[@class='conj-result'])");
                HtmlNodeCollection words1 = doc.DocumentNode.SelectNodes(@"//b");
                int tenseIndex = 0;// GetTenseIndex(GetKeyword(Tense.Imperative));

                for (int i = 0; i < 5; i++)
                {
                    //TODO i * 5 needs to be configurable to account for the Imperative and few other things (or extract it in another method)
                    tenseMatches.Add(words.ElementAt(i + tenseIndex).InnerText);
                }
            }

            tenseMatches.Insert(0, "a");

            var result = ExtractConjugationFromMatches(tenseMatches);
            result.Remove(Person.FirstSingle);

            return result;
        }
    }
}
