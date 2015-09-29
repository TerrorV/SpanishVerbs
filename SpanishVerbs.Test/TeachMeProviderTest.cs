using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace SpanishVerbs.Test
{
    [TestClass]
    public class TeachMeProviderTest
    {
        [TestMethod]
        public void LoadSingleTense()
        {
            string page =
    @"	<tbody>
		 			<tr>
		        <td class=""tense_heading"">
		          <a title=""Present Tense Indicative"">Present<i class=""fa ""></i></a>
		        </td>
		              <td class=""conjugation english "" >I eat, am eating
		              </td>
		              <td class=""conjugation "" >como
		              </td>
		              <td class=""conjugation "" >comes
		              </td>
		              <td class=""conjugation "" >come
		              </td>
		              <td class=""conjugation "" >comemos
		              </td>
		              <td class=""conjugation "" >coméis
		              </td>
		              <td class=""conjugation "" >comen
		              </td>
		        	</tr>
    	</div>
    </div>  
	
	
	
	

		 			<tr>
		        <td class=""tense_heading"">
		          <a title=""Imperfect - Simple Past Subjunctive"">Imperfect<i class=""fa ""></i></a>
		        </td>
		              <td class=""conjugation english "" >I ate, was eating
		              </td>
		              <td class=""conjugation "" >comiera<div class=""or_block"">OR</div>comiese
		              </td>
		              <td class=""conjugation "" >comieras<div class=""or_block"">OR</div>comieses
		              </td>
		              <td class=""conjugation "" >comiera<div class=""or_block"">OR</div>comiese
		              </td>
		              <td class=""conjugation "" >comiéramos<div class=""or_block"">OR</div>comi&eacute;semos
		              </td>
		              <td class=""conjugation "" >comierais<div class=""or_block"">OR</div>comieseis
		              </td>
		              <td class=""conjugation "" >comieran<div class=""or_block"">OR</div>comiesen.
		              </td>
		        	</tr>
    	</div>
    </div>";
            Dictionary<Person, string> tense = new TeachMeProvider("").GetConjugationPerTense(page,Tense.Present);

            Assert.AreEqual(6, tense.Count);
            Assert.AreEqual("como", tense[Person.FirstSingle]);
            Assert.AreEqual("comes", tense[Person.SecondSingle]);
            Assert.AreEqual("come", tense[Person.ThirdSingle]);
            Assert.AreEqual("comemos", tense[Person.FirstPlural]);
            Assert.AreEqual("coméis", tense[Person.SecondPlural]);
            Assert.AreEqual("comen", tense[Person.ThirdPlural]);

            tense = new TeachMeProvider("").GetConjugationPerTense(page, Tense.Imperfect);
            Assert.AreEqual(6, tense.Count);
            Assert.AreEqual("comiera<div class=\"or_block\">OR</div>comiese", tense[Person.FirstSingle]);
            Assert.AreEqual("comieras<div class=\"or_block\">OR</div>comieses", tense[Person.SecondSingle]);
            Assert.AreEqual("comiera<div class=\"or_block\">OR</div>comiese", tense[Person.ThirdSingle]);
            Assert.AreEqual("comiéramos<div class=\"or_block\">OR</div>comi&eacute;semos", tense[Person.FirstPlural]);
            Assert.AreEqual("comierais<div class=\"or_block\">OR</div>comieseis", tense[Person.SecondPlural]);
            Assert.AreEqual("comieran<div class=\"or_block\">OR</div>comiesen", tense[Person.ThirdPlural]);
        }

        [TestMethod]
        public void LoadConjugation()
        {
            string page =
                @"    <div class=""table_wrapper"">
    <div class=""tense"" > 
	    <h3 class=""category_heading"">
    		Other Forms
     	</h3> 
    	<table class=""research other-form""> 
      	<tbody>
		 			<tr>
		        <td class=""tense_heading"">
		          <a title=""Present participle"">Present Participle<i class=""fa ""></i></a>
		        </td>
		              <td class=""conjugation english "" >eating
		              </td>
		              <td class=""conjugation ""  colspan=&quot;6&quot; >comiendo
		              </td>
		        	</tr>
    	</div>
    </div>  


		 			<tr>
		        <td class=""tense_heading"">
		          <a title=""new_format"">Past Participle<i class=""fa ""></i></a>
		        </td>
		              <td class=""conjugation english "" >eaten
		              </td>
		              <td class=""conjugation ""  colspan=&quot;6&quot; >comido
		              </td>
		        	</tr>
		    		</tbody> 
    			</table>
    	</div>
    </div>  

</div>
";

            string gerund = new TeachMeProvider("").GetGerund(page);

            Assert.AreEqual("comiendo", gerund);
        }

        [TestMethod]
        public void LoadImperative()
        {
            Dictionary<Person, string> tense = new TeachMeProvider("").GetImperative(TeachMeTestResource.Page);
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
