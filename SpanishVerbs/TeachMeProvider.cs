﻿using HtmlAgilityPack;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SpanishVerbs
{
    public class TeachMeProvider :ProviderBase<Match>, IConjugationProvider
    {
        public TeachMeProvider(string providerUrl):base(providerUrl)
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


        //    if (!ValidateVerb(verb))
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
            Regex rxTense = new Regex(@"class=""tense_heading"">\s*<a[^>]*title=""[\s\w-]+""[^>]*>(Present Participle).*\s*</td>\s*(<td\s+class=""conjugation[^""]*[^>]*>(\w+|\w+<div[^>]*>OR</div>\w+)[^<]*</td>\s+)+</tr>");
            Regex rxRow = new Regex(@"<td\s+class=""conjugation[^""]*[^>]*>(\w+|\w+<div[^>]*>OR</div>\w+)[^<]*</td>\s+");

            Match match = rxTense.Match(rawData);
            if (!match.Success)
                return string.Empty;

            return rxRow.Matches( match.Value)[1].Groups[1].Value;
        }

        //private Dictionary<Person, string> GetConjugationPerTense(string rawData, Tense tense)
        //{
        //    //switch (tense)
        //    //{
        //    //    case Tense.Present:
        //    //        //break;
        //    //    case Tense.PresentPerfect:
        //    //        //break;
        //    //    case Tense.Imperfect:
        //    //        //break;
        //    //    case Tense.Preterite:
        //    //        //break;
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
                case Tense.PresentPerfect:
                    return "Present Perfect";
                case Tense.PastPerfect:
                    return "Past Perfect";
                case Tense.FuturePerfect:
                    return "Future Perfect";
                case Tense.ConditionalPerfect:
                    return "Conditional Perfect";
                case Tense.PreteritePerfect:
                    return "Past Perfect Simple";
                default:
                    return tense.ToString();
            }
        }

        public override Dictionary<Person, string> ExtractConjugationFromMatches(IEnumerable<Match> collection)
        {
//            MatchCollection matchCollection = (MatchCollection)collection;
            Dictionary<Person, string> conjugation = new Dictionary<Person, string>();
            if (collection.Count() < 6)
                return conjugation;

            for (int i = 0; i < 6; i++)
            {
                conjugation.Add((Person)i, collection.ElementAt(i).Groups[2].Value.Trim());
            }

            return conjugation;
        }

        public override IEnumerable<Match> FindMatchesPerTense(string page, string tenseKeyword)
        {
            Regex rxTense = new Regex(string.Format(@"class=""tense_heading"">\s*<a[^>]*title=""[\s\w-]+""[^>]*>({0}).*\s*</td>\s*(<td\s+class=""conjugation[^""]*[^>]*>(\w+|\w+<div[^>]*>OR</div>\w+)[^<]*</td>\s+)+</tr>",tenseKeyword));
            Regex rxRow = new Regex(@"<td\s+class=""conjugation (irregular)*""[^>]*>([\w\s&;]+|[\w\s&;]+<div[^>]*>OR</div>[\w\s&;]+)[^<]*</td>\s+");

            Match tenseMatch = rxTense.Match(page);

            return rxRow.Matches(tenseMatch.Value).Cast<Match>();
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
                HtmlNodeCollection words = doc.DocumentNode.SelectNodes(@"//h3[normalize-space(text()) = 'Imperative']/../table/tbody/tr[1]/td[position()>2]");

                ////HtmlNodeCollection words = doc.DocumentNode.SelectNodes(@"//td[contains(@class,'vtable-word')]");

                for (int i = 0; i < 6; i++)
                {
                    //TODO i * 5 needs to be configurable to account for the Imperative and few other things (or extract it in another method)
                    tenseMatches.Add(words.ElementAt(i).InnerText);
                }
            }


            var result = ExtractConjugationFromMatches(tenseMatches);

            return result;


            throw new NotImplementedException();
        }

        public Dictionary<Person, string> ExtractConjugationFromMatches(IEnumerable<string> matchCollection)
        {
            Dictionary<Person, string> conjugation = new Dictionary<Person, string>();
            if (matchCollection.Count() < 6)
                return conjugation;

            for (int i = 0; i < 6; i++)
            {
                string currentConjugation = matchCollection.ElementAt(i).Trim();
                if (Regex.IsMatch(currentConjugation, @"\w+"))
                {
                    conjugation.Add((Person)i, currentConjugation);
                }
            }

            return conjugation;
        }
    }
}
