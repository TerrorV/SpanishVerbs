using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;

namespace SpanishVerbs
{
    public class SpanishDictProvider : ProviderBase<string>, IConjugationProvider
    {
        public SpanishDictProvider(string providerUrl)
            : base(providerUrl)
        {

        }

        public override string GetGerund(string rawData)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(rawData);
            XmlNode gerund = doc.SelectNodes(@"//div[contains(@class,'conj-row')]/span").Item(0);

            return gerund.InnerXml;
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
                case Tense.Present:
                    return "Indicative Present";
                case Tense.PresentPerfect:
                    return "Perfect Present";
                case Tense.Imperfect:
                    return "Indicative Imperfect";
                case Tense.Preterite:
                    return "Indicative Preterite";
                case Tense.PastPerfect:
                    return "Perfect Past";
                case Tense.Future:
                    return "Indicative Future";
                case Tense.FuturePerfect:
                    return "Perfect Future";
                case Tense.Conditional:
                    return "Indicative Conditional";
                case Tense.ConditionalPerfect:
                    return "Perfect Conditional";
                case Tense.PreteritePerfect:
                    return "Perfect Preterite";
                default:
                    return tense.ToString();
            }
        }

        public override Dictionary<Person, string> ExtractConjugationFromMatches(IEnumerable<string> matchCollection)
        {
            Dictionary<Person, string> conjugation = new Dictionary<Person, string>();
            if (matchCollection.Count() < 6)
                return conjugation;

            for (int i = 0; i < 6; i++)
            {
                conjugation.Add((Person)i, matchCollection.ElementAt(i).Trim());
            }

            return conjugation;
        }

        public override IEnumerable<string> FindMatchesPerTense(string page, string tenseKeyword)
        {
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(page);

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
                HtmlNodeCollection words = doc.DocumentNode.SelectNodes(@"//td[contains(@class,'vtable-word')]");
                int tenseIndex = GetTenseIndex(tenseKeyword);

                for (int i = 0; i < 6; i++)
                {
                    tenseMatches.Add(words.ElementAt(i * 5 + tenseIndex).InnerText);
                }
            }

            return tenseMatches;
        }

        private int GetTenseIndex(string tenseKeyword)
        {
            string[] testType = tenseKeyword.Split(' ');
            int mainTypeIndex = 0;
            switch (testType[0])
            {
                case "Indicative":
                    mainTypeIndex = 0;
                    break;
                case "Subjunctive":
                    mainTypeIndex = 30;
                    break;
                case "Imperative":
                    mainTypeIndex = 60;
                    break;
                case "Perfect":
                    mainTypeIndex = 66;
                    break;
                case "PerfectSubjunctive":
                    mainTypeIndex = 66;
                    break;
                default:
                    mainTypeIndex = 0;
                    break;
            }

            int tenseTypeIndex = 0;
            switch(testType[1])
            {
                case "Present":
                    tenseTypeIndex =0;
                    break;
                  case "Preterite":
                    tenseTypeIndex =1;
                    break;
                case "Imperfect":
                    tenseTypeIndex =2;
                    break;
                case "Conditional":
                    tenseTypeIndex =3;
                    break;
                case "Future":
                    tenseTypeIndex =4;
                    break;
              default:
                    tenseTypeIndex =0;
                    break;
            }

            return mainTypeIndex + tenseTypeIndex;
        }

        private string GetParticiple(string rawData)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(rawData);
            XmlNode gerund = doc.SelectNodes(@"//div[contains(@class,'conj-row')]/span").Item(1);

            return gerund.InnerXml;
        }

    }
}
