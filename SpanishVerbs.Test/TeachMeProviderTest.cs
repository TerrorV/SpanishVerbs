using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
            Verb verb = new TeachMeProvider().GetConjugation(page);

            Assert.AreEqual(6, verb.Present.Count);
            Assert.AreEqual(6, verb.Imperfect.Count);
            Assert.AreEqual("como", verb.Present[Person.FirstSingle]);
            Assert.AreEqual("comes", verb.Present[Person.SecondSingle]);
            Assert.AreEqual("come", verb.Present[Person.ThirdSingle]);
            Assert.AreEqual("comemos", verb.Present[Person.FirstPlural]);
            Assert.AreEqual("coméis", verb.Present[Person.SecondPlural]);
            Assert.AreEqual("comen", verb.Present[Person.ThirdPlural]);

            Assert.AreEqual("comiera<div class=\"or_block\">OR</div>comiese", verb.Imperfect[Person.FirstSingle]);
            Assert.AreEqual("comieras<div class=\"or_block\">OR</div>comieses", verb.Imperfect[Person.SecondSingle]);
            Assert.AreEqual("comiera<div class=\"or_block\">OR</div>comiese", verb.Imperfect[Person.ThirdSingle]);
            Assert.AreEqual("comiéramos<div class=\"or_block\">OR</div>comi&eacute;semos", verb.Imperfect[Person.FirstPlural]);
            Assert.AreEqual("comierais<div class=\"or_block\">OR</div>comieseis", verb.Imperfect[Person.SecondPlural]);
            Assert.AreEqual("comieran<div class=\"or_block\">OR</div>comiesen", verb.Imperfect[Person.ThirdPlural]);
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

            Verb verb = new TeachMeProvider().GetConjugation(page);

            Assert.AreEqual("comiendo", verb.PresentParticiple);
        }

    }
}
