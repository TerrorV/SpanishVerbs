using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace SpanishVerbs.Test
{
    [TestClass]
    public class SpanishDictProviderTest
    {
        [TestMethod]
        public void LoadSingleTense()
        {
            Dictionary<Person, string> tense = new SpanishDictProvider("").GetConjugationPerTense(SpanishDictTestResource.Page, Tense.Present);

            Assert.AreEqual(6, tense.Count);
            Assert.AreEqual("tengo", tense[Person.FirstSingle]);
            Assert.AreEqual("tienes", tense[Person.SecondSingle]);
            Assert.AreEqual("tiene", tense[Person.ThirdSingle]);
            Assert.AreEqual("tenemos", tense[Person.FirstPlural]);
            Assert.AreEqual("tenéis", tense[Person.SecondPlural]);
            Assert.AreEqual("tienen", tense[Person.ThirdPlural]);

            tense = new SpanishDictProvider("").GetConjugationPerTense(SpanishDictTestResource.Page, Tense.Imperfect);
            Assert.AreEqual(6, tense.Count);
            Assert.AreEqual("tenía", tense[Person.FirstSingle]);
            Assert.AreEqual("tenías", tense[Person.SecondSingle]);
            Assert.AreEqual("tenía", tense[Person.ThirdSingle]);
            Assert.AreEqual("teníamos", tense[Person.FirstPlural]);
            Assert.AreEqual("teníais", tense[Person.SecondPlural]);
            Assert.AreEqual("tenían", tense[Person.ThirdPlural]);


        }

        [TestMethod]
        public void LoadImperative()
        {
            Dictionary<Person, string> tense = new SpanishDictProvider("").GetImperative(SpanishDictTestResource.Page);
            Assert.AreEqual(5, tense.Count);
            Assert.AreEqual("ten", tense[Person.SecondSingle]);
            Assert.AreEqual("tenga", tense[Person.ThirdSingle]);
            Assert.AreEqual("tengamos", tense[Person.FirstPlural]);
            Assert.AreEqual("tened", tense[Person.SecondPlural]);
            Assert.AreEqual("tengan", tense[Person.ThirdPlural]);
            Assert.IsFalse(tense.ContainsKey(Person.FirstSingle));
            
        }

        [TestMethod]
        public void LoadConjugation()
        {
            //añadir
            string gerund = new SpanishDictProvider("").GetGerund(SpanishDictTestResource.Page);

            Assert.AreEqual("teniendo", gerund);

            gerund = new SpanishDictProvider("").GetGerund(SpanishDictTestResource.Page2);

            Assert.AreEqual("andando", gerund);

            gerund = new SpanishDictProvider("").GetGerund(SpanishDictTestResource.Page3);

            Assert.AreEqual("añadiendo", gerund);
        }

        [TestMethod]
        public void LoadResourceFromSite()
        {
            var provider = new SpanishDictProvider("http://www.spanishdict.com/conjugate/{0}");
            var page = provider.CallWebSite("abandonar");
            var gerund = provider.GetGerund(page);
            Assert.AreEqual("abandonando", gerund);
        }
    }
}
