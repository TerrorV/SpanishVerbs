using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using SpanishVerb.DAL.Abstract;
using SpanishVerb.DAL.SQLite;

namespace SpanishVerbs
{
    public partial class Form1 : Form
    {
        List<IProviderBase> _providers = new List<IProviderBase>();
        IConnectionStringProvider _connectionStringProvider;
        IStorageProvider _storageProvider;

        public Form1()
        {
            InitializeComponent();
            _providers.Add(new TeachMeProvider("http://www.123teachme.com/spanish_verb_conjugation/{0}"));
            _providers.Add(new BabLaProvider("http://en.bab.la/conjugation/spanish/{0}"));
            _providers.Add(new SpanishDictProvider("http://www.spanishdict.com/conjugate/{0}"));
            comboBox1.DataSource = _providers;
            _connectionStringProvider = new ConnectionStringProvider();

            //comboBox1.Items.Insert(0, "");
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //if (string.IsNullOrEmpty(textBox1.Text))
            //    return;

            ////textBox2.Text = TableFrom123Spanish();
            ////if (string.IsNullOrEmpty(textBox2.Text))
            ////{

            //textBox2.Text = TableFromBabLa(textBox1.Text);
            ////}
        }

        //private string TableFromBabLa(string text)
        //{
        //    ////////Regex rxVerbs = new Regex(@"<h5 [^>]*>(Indicativo presente|Indicativo pretérito perfecto compuest|Indicativo pretérito perfecto simple)</h5>(<span [^>]*>([\w/]*)</span>\s*(\w*)(<br>)*)*");
        //    //////Regex rxVerbs = new Regex(@"<h5 [^>]*>(Indicativo presente|Indicativo pretérito perfecto compuesto|Indicativo pretérito perfecto simple)</h5>(<span [^>]*>([\w/]*)</span>\s*([\s\w]*)(<br>)*)*");

        //    //////MatchCollection matches = rxVerbs.Matches(text);
        //    //////if (matches.Count == 0)
        //    //////{
        //    //////    return string.Empty;
        //    //////}

        //    //////string[,] verbGrid = new string[matches.Count + 1, 6];
        //    //////verbGrid[0, 0] = "Yo";
        //    //////verbGrid[0, 1] = "Tu";
        //    //////verbGrid[0, 2] = "El/Ella/Ud";
        //    //////verbGrid[0, 3] = "Nosotros/as";
        //    //////verbGrid[0, 4] = "Vosotros/as";
        //    //////verbGrid[0, 5] = "Ellos/Ellas/Uds";

        //    //////for (int i = 0; i < matches.Count; i++)
        //    //////{
        //    //////    for (int j = 0; j < matches[0].Groups[4].Captures.Count; j++)
        //    //////    {
        //    //////        verbGrid[i + 1, j] = matches[i].Groups[4].Captures[j].Value;
        //    //////    }
        //    //////}

        //    //////Regex rxGerunds = new Regex(@"<h5 [^>]*>(Gerundio)</h5>(<span [^>]*>([\w/]*)</span>\s*([\s\w]*)(<br>)*)*");
        //    //////MatchCollection matchesGerund = rxGerunds.Matches(textBox1.Text);

        //    //////return TableFromArray(matches.Count, matchesGerund[0].Groups[4].Value, verbGrid);

        //    return TableFromVerb(new BabLaProvider().GetConjugation(textBox1.Text));
        //}

        //private string TableFrom123Spanish()
        //{
        //    ////////Regex rx = new Regex(@"<tr>\s*<td [^>]*><a[^>]*>([\w\s-]+)(</a>)*\s*</td>\s*(<td[^>]*>([^<]+)</td>\s*)+</tr>");
        //    //////Regex rxVerbsNew = new Regex(@"(<td\s+class=""conjugation[^""]*[^>]*>(\w+)[^<]*</td>\s+)+");
        //    //////Regex rxVerbs = new Regex(@"<tr>\s*<td [^>]*><a[^>]*title=" + "\"" + @"(Present Tense Indicative|Present perfect Indicative|Preterite)[^>]*>(Present|Present Perfect|Imperfect|Preterite)(</a>)*\s*</td>\s*(<td[^>]*>([^<]+)</td>\s*)+</tr>");
        //    ////////Regex rxGerunds = new Regex(@"Present Participle.*\s*<td.*conjugation[^>]*>(.*)</td>");
        //    //////Regex rxGerunds = new Regex(@"Present Participle.*\s*(<td.*conjugation english[^>]*>.*</td>)*\s*(<td.*conjugation[^>]*>(.*)</td>)");

        //    //////MatchCollection matches = rxVerbs.Matches(textBox1.Text);
        //    //////MatchCollection matchesGerund = rxGerunds.Matches(textBox1.Text);
        //    //////List<string> transponedMatches = new List<string>();
        //    ////////transponedMatches.AddRange(new string[matches.Count]);
        //    //////if (matches.Count == 0)
        //    //////{
        //    //////    return string.Empty;
        //    //////}

        //    //////string[,] verbGrid = new string[matches.Count + 1, 6];
        //    //////verbGrid[0, 0] = "Yo";
        //    //////verbGrid[0, 1] = "Tu";
        //    //////verbGrid[0, 2] = "El/Ella/Ud";
        //    //////verbGrid[0, 3] = "Nosotros/as";
        //    //////verbGrid[0, 4] = "Vosotros/as";
        //    //////verbGrid[0, 5] = "Ellos/Ellas/Uds";
        //    //////for (int i = 0; i < matches.Count; i++)
        //    //////{
        //    //////    Regex rxRow = new Regex(@"<td[^>]*>([^<]+)</td>\s*");
        //    //////    var rowsMatches = rxRow.Matches(matches[i].Groups[0].Value);
        //    //////    Match[] matchesArray = new Match[rowsMatches.Count];
        //    //////    rowsMatches.CopyTo(matchesArray, 0);
        //    //////    List<Match> filteredRows = matchesArray.Where(m => !m.Value.Contains("english")).ToList();

        //    //////    for (int j = 0; j < filteredRows.Count; j++)
        //    //////    {
        //    //////        verbGrid[i + 1, j] = filteredRows[j].Groups[1].Value;
        //    //////    }
        //    //////}

        //    //////return TableFromArray(matches.Count, matchesGerund[0].Groups[matchesGerund[0].Groups.Count - 1].Value, verbGrid);
        //    try
        //    {
        //        return TableFromVerb(new TeachMeProvider().GetConjugation(textBox1.Text));
        //    }
        //    catch
        //    {
        //        return string.Empty;
        //    }
        //}

        private static string TableFromArray(int numberOfTenses, string gerund, string[,] verbGrid)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<table border=\"1\" cellspacing=\"0\">");
            for (int i = 0; i < 6; i++)
            {
                sb.AppendLine("<tr>");
                for (int j = 0; j < verbGrid.Length / 6; j++)
                {
                    sb.Append(string.Format("\t<td>{0}</td>\r\n", verbGrid[j, i]));
                }
                sb.AppendLine("</tr>");
            }
            sb.Append(string.Format("<tr><td colspan='{0}'>", numberOfTenses + 2)).Append(gerund).AppendLine("</td><tr>");
            sb.Append("</table>");
            return sb.ToString();
        }

        private static string TableFromVerb(Verb verb)
        {
            int tenses = (verb.Conditional.Count > 0 ? 1 : 0) +
                         (verb.ConditionalPerfect.Count > 0 ? 1 : 0) +
                         (verb.Future.Count > 0 ? 1 : 0) +
                         (verb.FuturePerfect.Count > 0 ? 1 : 0) +
                         (verb.Imperfect.Count > 0 ? 1 : 0) +
                         (verb.PastPerfect.Count > 0 ? 1 : 0) +
                         (verb.Present.Count > 0 ? 1 : 0) +
                         (verb.PresentPerfect.Count > 0 ? 1 : 0) +
                         (verb.Preterite.Count > 0 ? 1 : 0) +
                         (verb.PreteritePerfect.Count > 0 ? 1 : 0);

            string[,] verbGrid = new string[6, 6];
            verbGrid[0, 0] = "Yo";
            verbGrid[0, 1] = "Tu";
            verbGrid[0, 2] = "El/Ella/Ud";
            verbGrid[0, 3] = "Nosotros/as";
            verbGrid[0, 4] = "Vosotros/as";
            verbGrid[0, 5] = "Ellos/Ellas/Uds";

            for (int i = 0; i < 6; i++)
            {
                var person = (Person)i;
                verbGrid[1, i] = verb.Present.ContainsKey(person) ? verb.Present[person] : string.Empty;
                verbGrid[2, i] = verb.PresentPerfect.ContainsKey(person) ? verb.PresentPerfect[person] : string.Empty;
                verbGrid[3, i] = verb.Preterite.ContainsKey(person) ? verb.Preterite[person] : string.Empty;
                verbGrid[4, i] = verb.Imperfect.ContainsKey(person) ? verb.Imperfect[person] : string.Empty;
                verbGrid[5, i] = verb.Imperative.ContainsKey(person) ? verb.Imperative[person] : string.Empty;
            }

            return TableFromArray(3, verb.PresentParticiple, verbGrid);

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.A)
            {
                textBox2.SelectAll();
            }

            if (e.Control && e.KeyCode == Keys.C)
            {
                textBox2.Copy();
            }
            if (e.Control && e.KeyCode == Keys.V)
            {
                //textBox2.Paste();
            }

            e.Handled = true;
            e.SuppressKeyPress = true;
        }

        //private void textBox1_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.Control && e.KeyCode == Keys.A)
        //    {
        //        textBox1.SelectAll();
        //    }

        //    if (e.Control && e.KeyCode == Keys.C)
        //    {
        //        textBox1.Copy();
        //    }
        //    if (e.Control && e.KeyCode == Keys.V)
        //    {
        //        //textBox1.Text = string.Empty;
        //        //textBox1.Paste();
        //        //return;
        //    }
        //}

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            //textBox1.Text = CallWebSite("http://www.123teachme.com/spanish_verb_conjugation/{0}");
            //if (string.IsNullOrEmpty(textBox1.Text))
            //    return;


            //try
            //{
            //    textBox2.Text = TableFrom123Spanish();
            //    if (string.IsNullOrEmpty(textBox2.Text) || checkBox1.Checked)
            //    {

            //        textBox1.Text = CallWebSite("http://en.bab.la/conjugation/spanish/{0}");

            //        if (string.IsNullOrEmpty(textBox1.Text))
            //            return;

            //        textBox2.Text = TableFromBabLa(textBox1.Text);
            //    }
            //}
            //catch
            //{ }
            //if (string.IsNullOrEmpty(textBox2.Text))
            //{
            //    try
            //    {
            //        textBox1.Text = CallWebSite("http://www.spanishdict.com/conjugate/{0}");
            //        if (string.IsNullOrEmpty(textBox1.Text))
            //            return;
            //        textBox2.Text = TableFromSpanishDict(textBox1.Text);

            //    }
            //    catch
            //    {

            //        throw;
            //    }
            //}
        }

        private void textBox3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!checkBox1.Checked)
                {
                    IProviderBase provider = _providers.Where(p => p == comboBox1.SelectedValue).FirstOrDefault();
                    try
                    {
                        textBox2.Text = TableFromVerb(provider.GetConjugation(textBox3.Text.Trim()));
                    }
                    catch (Exception ex)
                    {
                        textBox2.Text = string.Format($"{ex.Message}\r\n{ex.StackTrace}\r\n{ex.Data}" );
                    }
                }
                else
                {
                    foreach (IProviderBase provider in _providers)
                    {
                        string verbTable = string.Empty;
                        try
                        {
                            verbTable = TableFromVerb(provider.GetConjugation(textBox3.Text.Trim()));
                        }
                        catch (Exception ex)
                        {
                            textBox2.Text = string.Format($"{ex.Message}\r\n{ex.StackTrace}\r\n{ex.Data}");
                        }

                        if (!string.IsNullOrEmpty(verbTable))
                        {
                            textBox2.Text = verbTable;
                            break;
                        }
                    }
                }

                textBox3.Text = string.Empty;
            }

            if (e.Control && e.KeyCode == Keys.A)
            {
                textBox3.SelectAll();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }

        }

        private void openDb_Click(object sender, EventArgs e)
        {
            dbSelector.FileOk += DbSelector_FileOk;
            dbSelector.ShowDialog();
        }

        private void DbSelector_FileOk(object sender, CancelEventArgs e)
        {
            var connectionString = _connectionStringProvider.GetConnectionStringFromPath("AnkiModel", dbSelector.FileName);
            _storageProvider = new AnkiProvider(connectionString);
            var verbs = _storageProvider.GetAllVerbs();
            foreach (var verb in verbs)
            {

                foreach (IProviderBase provider in _providers)
                {
                    string verbTable = string.Empty;
                    try
                    {
                        verbTable = TableFromVerb(provider.GetConjugation(verb.Front));
                    }
                    catch (Exception ex)
                    {
                        textBox2.Text = string.Format($"{ex.Message}=\r\n{ex.StackTrace}\r\n{ex.Data}");
                    }

                    if (!string.IsNullOrEmpty(verbTable))
                    {
                        verb.Details = verbTable;
                        break;
                    }
                }

                _storageProvider.SaveNote(verb);
            }


        }

        //private string TableFromSpanishDict(string p)
        //{
        //    try
        //    {
        //        return TableFromVerb(new SpanishDictProvider().GetConjugation(textBox1.Text));
        //    }
        //    catch
        //    {
        //        return string.Empty;
        //    }
        //}


    }
}
