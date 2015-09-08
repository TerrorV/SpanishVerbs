using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpanishVerbs
{
    public enum Person
    {
        FirstSingle = 0,
        SecondSingle = 1,
        ThirdSingle=2,
        FirstPlural=3,
        SecondPlural=4,
        ThirdPlural=5
    }
    public enum Tense
    {
        Present=0,
        PresentPerfect=1,
        Imperfect=2,
        Preterite=3,
        PastPerfect=4,
        Future=5,
        FuturePerfect=6,
        Conditional=7,
        ConditionalPerfect=8,
        PreteritePerfect=9,
        Imperative =10
    }
    public class Verb
    {

        public Verb()
        {
            Present = new Dictionary<Person, string>();
            PresentPerfect = new Dictionary<Person, string>();
            Imperfect = new Dictionary<Person,string>();
            Preterite = new Dictionary<Person,string>();
            PastPerfect = new Dictionary<Person,string>();
            Future = new Dictionary<Person,string>();
            FuturePerfect = new Dictionary<Person,string>();
            Conditional = new Dictionary<Person,string>();
            ConditionalPerfect = new Dictionary<Person,string>();
            PreteritePerfect = new Dictionary<Person, string>();
            Imperative = new Dictionary<Person, string>();
        }
        public string Infinitive { get; set; }
        public string PresentParticiple { get; set; }

        public Dictionary<Person,string> Present { get; set; }
        public Dictionary<Person,string> PresentPerfect { get; set; }
        public Dictionary<Person,string> Imperfect { get; set; }
        public Dictionary<Person,string> Preterite { get; set; }
        public Dictionary<Person,string> PastPerfect { get; set; }
        public Dictionary<Person,string> Future { get; set; }
        public Dictionary<Person,string> FuturePerfect { get; set; }
        public Dictionary<Person,string> Conditional { get; set; }
        public Dictionary<Person,string> ConditionalPerfect { get; set; }
        public Dictionary<Person, string> PreteritePerfect { get; set; }
        public Dictionary<Person, string> Imperative { get; set; }
        //public Dictionary<Person,string> Present { get; set; }
        //public Dictionary<Person,string> Present { get; set; }
        //public Dictionary<Person,string> Present { get; set; }
        //public Dictionary<Person,string> Present { get; set; }
        //public Dictionary<Person,string> Present { get; set; }
        //public Dictionary<Person,string> Present { get; set; }
        //public Dictionary<Person,string> Present { get; set; }
        //public Dictionary<Person,string> Present { get; set; }
    }
}
