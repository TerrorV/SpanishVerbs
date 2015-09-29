using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace SpanishVerbs.Test
{
    [TestClass]
    public class BabLaProviderTest
    {
        [TestMethod]
        public void LoadSingleTense()
        {
            string page =
    @"<div class=""result-wrapper""><div class=""row-fluid result-row""><div class=""span4 result-left""><p><h5 class=""h5-conj"">Indicativo presente</h5><span class=""babGy"">yo</span> como<br><span class=""babGy"">tú</span> comes<br><span class=""babGy"">él/ella</span> come<br><span class=""babGy"">nosotros/nosotras</span> comemos<br><span class=""babGy"">vosotros/vosotras</span> coméis<br><span class=""babGy"">ellos/ellas</span> comen<br></p></div><div class=""span4 result-left""><p><h5 class=""h5-conj"">Indicativo pretérito imperfecto</h5><span class=""babGy"">yo</span> comía<br><span class=""babGy"">tú</span> comías<br><span class=""babGy"">él/ella</span> comía<br><span class=""babGy"">nosotros/nosotras</span> comíamos<br><span class=""babGy"">vosotros/vosotras</span> comíais<br><span class=""babGy"">ellos/ellas</span> comían<br></p></div><div class=""span4 result-left""><p><h5 class=""h5-conj"">Indicativo pretérito perfecto simple</h5><span class=""babGy"">yo</span> comí<br><span class=""babGy"">tú</span> comiste<br><span class=""babGy"">él/ella</span> comió<br><span class=""babGy"">nosotros/nosotras</span> comimos<br><span class=""babGy"">vosotros/vosotras</span> comisteis<br><span class=""babGy"">ellos/ellas</span> comieron<br></p></div></div></div><div class=""result-wrapper""><div class=""row-fluid result-row""><div class=""span4 result-left""><p><h5 class=""h5-conj"">Indicativo futuro</h5><span class=""babGy"">yo</span> comeré<br><span class=""babGy"">tú</span> comerás<br><span class=""babGy"">él/ella</span> comerá<br><span class=""babGy"">nosotros/nosotras</span> comeremos<br><span class=""babGy"">vosotros/vosotras</span> comeréis<br><span class=""babGy"">ellos/ellas</span> comerán<br></p></div><div class=""span4 result-left""><p><h5 class=""h5-conj"">Indicativo pretérito perfecto compuesto</h5><span class=""babGy"">yo</span> he comido<br><span class=""babGy"">tú</span> has comido<br><span class=""babGy"">él/ella</span> ha comido<br><span class=""babGy"">nosotros/nosotras</span> hemos comido<br><span class=""babGy"">vosotros/vosotras</span> habéis comido<br><span class=""babGy"">ellos/ellas</span> han comido<br></p></div><div class=""span4 result-left""><p><h5 class=""h5-conj"">Indicativo pretérito pluscuamperfecto</h5><span class=""babGy"">yo</span> había comido<br><span class=""babGy"">tú</span> habías comido<br><span class=""babGy"">él/ella</span> había comido<br><span class=""babGy"">nosotros/nosotras</span> habíamos comido<br><span class=""babGy"">vosotros/vosotras</span> habíais comido<br><span class=""babGy"">ellos/ellas</span> habían comido<br></p></div></div></div><div class=""result-wrapper""><div class=""row-fluid result-row""><div class=""span4 result-left""><p><h5 class=""h5-conj"">Indicativo pretérito anterior</h5><span class=""babGy"">yo</span> hube comido<br><span class=""babGy"">tú</span> hubiste comido<br><span class=""babGy"">él/ella</span> hubo comido<br><span class=""babGy"">nosotros/nosotras</span> hubimos comido<br><span class=""babGy"">vosotros/vosotras</span> hubisteis comido<br><span class=""babGy"">ellos/ellas</span> hubieron comido<br></p></div><div class=""span4 result-left""><p><h5 class=""h5-conj"">Indicativo futuro perfecto</h5><span class=""babGy"">yo</span> habré comido<br><span class=""babGy"">tú</span> habrás comido<br><span class=""babGy"">él/ella</span> habrá comido<br><span class=""babGy"">nosotros/nosotras</span> habremos comido<br><span class=""babGy"">vosotros/vosotras</span> habréis comido<br><span class=""babGy"">ellos/ellas</span> habrán comido<br></p></div><div class=""span4 result-left""><p><h5 class=""h5-conj"">Condicional simple</h5><span class=""babGy"">yo</span> comería<br><span class=""babGy"">tú</span> comerías<br><span class=""babGy"">él/ella</span> comería<br><span class=""babGy"">nosotros/nosotras</span> comeríamos<br><span class=""babGy"">vosotros/vosotras</span> comeríais<br><span class=""babGy"">ellos/ellas</span> comerían<br></p></div></div></div><div class=""result-wrapper""><div class=""row-fluid result-row""><div class=""span4 result-left""><p><h5 class=""h5-conj"">Condicional compuesto</h5><span class=""babGy"">yo</span> habría comido<br><span class=""babGy"">tú</span> habrías comido<br><span class=""babGy"">él/ella</span> habría comido<br><span class=""babGy"">nosotros/nosotras</span> habríamos comido<br><span class=""babGy"">vosotros/vosotras</span> habríais comido<br><span class=""babGy"">ellos/ellas</span> habrían comido<br></p></div><div class=""span4 result-left""><p><h5 class=""h5-conj"">Subjuntivo presente</h5><span class=""babGy"">yo</span> coma<br><span class=""babGy"">tú</span> comas<br><span class=""babGy"">él/ella</span> coma<br><span class=""babGy"">nosotros/nosotras</span> comamos<br><span class=""babGy"">vosotros/vosotras</span> comáis<br><span class=""babGy"">ellos/ellas</span> coman<br></p></div><div class=""span4 result-left""><p><h5 class=""h5-conj"">Subjuntivo pretérito imperfecto 1</h5><span class=""babGy"">yo</span> comiera<br><span class=""babGy"">tú</span> comieras<br><span class=""babGy"">él/ella</span> comiera<br><span class=""babGy"">nosotros/nosotras</span> comiéramos<br><span class=""babGy"">vosotros/vosotras</span> comierais<br><span class=""babGy"">ellos/ellas</span> comieran<br></p></div></div></div><div class=""result-wrapper""><div class=""row-fluid result-row""><div class=""span4 result-left""><p><h5 class=""h5-conj"">Subjuntivo pretérito imperfecto 2</h5><span class=""babGy"">yo</span> comiese<br><span class=""babGy"">tú</span> comieses<br><span class=""babGy"">él/ella</span> comiese<br><span class=""babGy"">nosotros/nosotras</span> comiésemos<br><span class=""babGy"">vosotros/vosotras</span> comieseis<br><span class=""babGy"">ellos/ellas</span> comiesen<br></p></div><div class=""span4 result-left""><p><h5 class=""h5-conj"">Subjuntivo futuro</h5><span class=""babGy"">yo</span> comiere<br><span class=""babGy"">tú</span> comieres<br><span class=""babGy"">él/ella</span> comiere<br><span class=""babGy"">nosotros/nosotras</span> comiéremos<br><span class=""babGy"">vosotros/vosotras</span> comiereis<br><span class=""babGy"">ellos/ellas</span> comieren<br></p></div><div class=""span4 result-left""><p><h5 class=""h5-conj"">Subjuntivo pretérito perfecto</h5><span class=""babGy"">yo</span> haya comido<br><span class=""babGy"">tú</span> hayas comido<br><span class=""babGy"">él/ella</span> haya comido<br><span class=""babGy"">nosotros/nosotras</span> hayamos comido<br><span class=""babGy"">vosotros/vosotras</span> hayáis comido<br><span class=""babGy"">ellos/ellas</span> hayan comido<br></p></div></div></div><div class=""result-wrapper""><div class=""row-fluid result-row""><div class=""span4 result-left""><p><h5 class=""h5-conj"">Subjuntivo pretérito pluscuamperfecto 1</h5><span class=""babGy"">yo</span> hubiera comido<br><span class=""babGy"">tú</span> hubieras comido<br><span class=""babGy"">él/ella</span> hubiera comido<br><span class=""babGy"">nosotros/nosotras</span> hubiéramos comido<br><span class=""babGy"">vosotros/vosotras</span> hubierais comido<br><span class=""babGy"">ellos/ellas</span> hubieran comido<br></p></div><div class=""span4 result-left""><p><h5 class=""h5-conj"">Subjuntivo pretérito pluscuamperfecto 2</h5><span class=""babGy"">yo</span> hubiese comido<br><span class=""babGy"">tú</span> hubieses comido<br><span class=""babGy"">él/ella</span> hubiese comido<br><span class=""babGy"">nosotros/nosotras</span> hubiésemos comido<br><span class=""babGy"">vosotros/vosotras</span> hubieseis comido<br><span class=""babGy"">ellos/ellas</span> hubiesen comido<br></p></div><div class=""span4 result-left""><p><h5 class=""h5-conj"">Subjuntivo futuro perfecto</h5><span class=""babGy"">yo</span> hubiere comido<br><span class=""babGy"">tú</span> hubieres comido<br><span class=""babGy"">él/ella</span> hubiere comido<br><span class=""babGy"">nosotros/nosotras</span> hubiéremos comido<br><span class=""babGy"">vosotros/vosotras</span> hubiereis comido<br><span class=""babGy"">ellos/ellas</span> hubieren comido<br></p></div></div></div><div class=""result-wrapper""><div class=""row-fluid result-row""><div class=""span4 result-left""><p><h5 class=""h5-conj"">Imperativo</h5><span class=""babGy"">tú</span> come<br><span class=""babGy"">él/ella</span> coma<br><span class=""babGy"">nosotros/nosotras</span> comamos<br><span class=""babGy"">vosotros/vosotras</span> comed<br><span class=""babGy"">ellos/ellas</span> coman<br></p></div><div class=""span4 result-left""><p><h5 class=""h5-conj"">Gerundio</h5><span class=""babGy""></span> comiendo<br></p></div><div class=""span4 result-left""><p><h5 class=""h5-conj"">Participio</h5><span class=""babGy""></span> comido<br></p></div></div></div></div>
</section>
<div class=""bab20""></div>
";
            Dictionary<Person,string> tense = new BabLaProvider("").GetConjugationPerTense(page,Tense.Present);

            Assert.AreEqual(6, tense.Count);
            Assert.AreEqual("como", tense[Person.FirstSingle]);
            Assert.AreEqual("comes", tense[Person.SecondSingle]);
            Assert.AreEqual("come", tense[Person.ThirdSingle]);
            Assert.AreEqual("comemos", tense[Person.FirstPlural]);
            Assert.AreEqual("coméis", tense[Person.SecondPlural]);
            Assert.AreEqual("comen", tense[Person.ThirdPlural]);

            tense = new BabLaProvider("").GetConjugationPerTense(page, Tense.Imperfect);
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
        public void LoadImperative()
        {
            Dictionary<Person, string> tense = new TeachMeProvider("").GetImperative(BabLaTestResource.Page);
            Assert.AreEqual(5, tense.Count);
            Assert.AreEqual("ten", tense[Person.SecondSingle]);
            Assert.AreEqual("tenga", tense[Person.ThirdSingle]);
            Assert.AreEqual("tengamos", tense[Person.FirstPlural]);
            Assert.AreEqual("tened", tense[Person.SecondPlural]);
            Assert.AreEqual("tengan", tense[Person.ThirdPlural]);
            Assert.IsFalse(tense.ContainsKey(Person.FirstSingle));

        }
    }
}
