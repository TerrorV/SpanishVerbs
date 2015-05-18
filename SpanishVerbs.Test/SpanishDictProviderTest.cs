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
        public void LoadConjugation()
        {
            string page =
                @" 
<span class=""babGy"">vosotros/vosotras</span> comed<br><span class=""babGy"">ellos/ellas</span> coman<br></p></div><div class=""span4 result-left""><p><h5 class=""h5-conj"">Gerundio</h5><span class=""babGy""></span> comiendo<br></p></div><div class=""span4 result-left""><p><h5 class=""h5-conj"">Participio</h5><span class=""babGy""></span> comido<br></p></div></div></div></div>
";

            string gerund = new BabLaProvider("").GetGerund(page);

            Assert.AreEqual("comiendo", gerund);
        }
    }
}
