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

            tense = new BabLaProvider("").GetConjugationPerTense(SpanishDictTestResource.Page, Tense.Imperfect);
            Assert.AreEqual(6, tense.Count);
            Assert.AreEqual("comía", tense[Person.FirstSingle]);
            Assert.AreEqual("comías", tense[Person.SecondSingle]);
            Assert.AreEqual("comía", tense[Person.ThirdSingle]);
            Assert.AreEqual("comíamos", tense[Person.FirstPlural]);
            Assert.AreEqual("comíais", tense[Person.SecondPlural]);
            Assert.AreEqual("comían", tense[Person.ThirdPlural]);
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

        [TestMethod]
        public void RegExTest()
        {
            string input = @"<table class=""vtable"">
<tr><td class=""vtable-empty""></td><td class=""vtable-title"">
<a href=""http://www.spanishdict.com/answers/100045/present-tense"" title=""Tense example: *He speaks* Spanish."" data-toggle=""tooltip"" class=""vtable-title-link"">Present<span class=""icon-question-mark""></span></a></td><td class=""vtable-title""><a href=""http://www.spanishdict.com/answers/100046/preterite-simple-past"" title=""Tense example: *He spoke* to my sister yesterday."" data-toggle=""tooltip"" class=""vtable-title-link"">Preterite<span class=""icon-question-mark""></span></a></td><td class=""vtable-title""><a href=""http://www.spanishdict.com/answers/100048/imperfect-past"" title=""Tense example: *He used to speak* Italian when he was a child."" data-toggle=""tooltip"" class=""vtable-title-link"">Imperfect<span class=""icon-question-mark""></span></a></td><td class=""vtable-title""><a href=""http://www.spanishdict.com/answers/100050/conditional"" title=""Tense example: *He would speak* French in France."" data-toggle=""tooltip"" class=""vtable-title-link"">Conditional<span class=""icon-question-mark""></span></a></td>
<td class=""vtable-title""><a href=""http://www.spanishdict.com/answers/100049/future"" title=""Tense example: *He will speak* Portuguese on his vacation next week."" data-toggle=""tooltip"" class=""vtable-title-link"">Future<span class=""icon-question-mark""></span></a></td></tr>
<tr><td class=""vtable-pronoun"">yo</td><td class=""vtable-word  "">abarco</td><td class=""vtable-word"">abar<span class='conj-irregular'>qu</span>é</td><td class=""vtable-word  "">abarcaba</td><td class=""vtable-word  "">abarcaría</td><td class=""vtable-word  "">abarcaré</td></tr>
<tr><td class=""vtable-pronoun"">tú</td><td class=""vtable-word  "">abarcas</td><td class=""vtable-word  "">abarcaste</td><td class=""vtable-word  "">abarcabas</td><td class=""vtable-word  "">abarcarías</td><td class=""vtable-word  "">abarcarás</td></tr>
<tr><td class=""vtable-pronoun"">él/ella/Ud.</td><td class=""vtable-word  "">abarca</td><td class=""vtable-word  "">abarcó</td><td class=""vtable-word  "">abarcaba</td><td class=""vtable-word  "">abarcaría</td><td class=""vtable-word  "">abarcará</td></tr>
<tr><td class=""vtable-pronoun"">nosotros</td><td class=""vtable-word  "">abarcamos</td><td class=""vtable-word  "">abarcamos</td><td class=""vtable-word  "">abarcábamos</td><td class=""vtable-word  "">abarcaríamos</td><td class=""vtable-word  "">abarcaremos</td></tr>
<tr><td class=""vtable-pronoun"">vosotros</td><td class=""vtable-word  "">abarcáis</td><td class=""vtable-word  "">abarcasteis</td><td class=""vtable-word  "">abarcabais</td><td class=""vtable-word  "">abarcaríais</td><td class=""vtable-word  "">abarcaréis</td></tr>
<tr><td class=""vtable-pronoun"">ellos/ellas/Uds.</td><td class=""vtable-word  "">abarcan</td><td class=""vtable-word  "">abarcaron</td><td class=""vtable-word  "">abarcaban</td><td class=""vtable-word  "">abarcarían</td><td class=""vtable-word  "">abarcarán</td></tr>
</table>";
            SpanishDictProvider provider = new SpanishDictProvider("");
            provider.FindMatchesPerTense(input, "Present");
            //var matches = rx.Matches(input);
        }
    }
}
