using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using Microsoft.Win32;

namespace XmlSync
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public static class Extention
    {
        public static IEnumerable<T> getMoreThanOnceRepeated<T>(this IEnumerable<T> extList, Func<T, object> groupProps) where T : class
        { //Return only the second and next reptition
            return extList
                .GroupBy(groupProps)
                .SelectMany(z => z.Skip(1)); //Skip the first occur and return all the others that repeats
        }
        public static IEnumerable<T> getAllRepeated<T>(this IEnumerable<T> extList, Func<T, object> groupProps) where T : class
        {
            //Get All the lines that has repeating
            return extList
                .GroupBy(groupProps)
                .Where(z => z.Count() > 1) //Filter only the distinct one
                .SelectMany(z => z);//All in where has to be retuned
        }
    }
    public static class PHVExtensions
    {
        public static IEnumerable<T> SetValue<T>(this IEnumerable<T> items, Action<T>
             updateMethod)
        {
            foreach (T item in items)
            {
                updateMethod(item);
            }
            return items;
        }
    }
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        List<XmlLanguageViewModel> xlvm = new List<XmlLanguageViewModel>();
        string fileNameMain = "";
        string fileNameSec = "";
        private void ButtonReadFile_Click(object sender, RoutedEventArgs e)
        {
            List<XmlLanguageModel> IxmlGlobal = new List<XmlLanguageModel>();
            List<XmlLanguageModel> IxmlSecond = new List<XmlLanguageModel>();
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                {
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.Load(openFileDialog.FileName);
                    fileNameMain = openFileDialog.FileName;
                    var cultureMain = xmlDoc.DocumentElement.SelectNodes("/localizationDictionary")[0].Attributes.GetNamedItem("culture").InnerText;
                    txtMain.Text = cultureMain.ToString();
                    XmlNodeList nodeList = xmlDoc.DocumentElement.SelectNodes("/localizationDictionary/texts/text");
                    int i = 0;
                    foreach (XmlNode node in nodeList)
                    {
                        XmlLanguageModel xlm = new XmlLanguageModel();
                        xlm.ID = i;
                        xlm.Name = node.Attributes.GetNamedItem("name").InnerText;
                        xlm.Text = node.InnerText;
                        IxmlGlobal.Add(xlm);
                    }
                }
                OpenFileDialog openFileDialog1 = new OpenFileDialog();
                if (openFileDialog1.ShowDialog() == true)
                {
                    XmlDocument xmlDoc1 = new XmlDocument();
                    xmlDoc1.Load(openFileDialog1.FileName);
                    fileNameSec = openFileDialog1.FileName;
                    var cultureSec = xmlDoc1.DocumentElement.SelectNodes("/localizationDictionary")[0].Attributes.GetNamedItem("culture").InnerText;
                    txtSecond.Text = cultureSec.ToString();
                    XmlNodeList nodeList1 = xmlDoc1.DocumentElement.SelectNodes("/localizationDictionary/texts/text");
                    /*xmlGlobal = new XmlLanguageModel[nodeList.Count]0;*/
                    int i1 = 0;
                    foreach (XmlNode node in nodeList1)
                    {
                        XmlLanguageModel xlm = new XmlLanguageModel();
                        xlm.ID = i1;
                        xlm.Name = node.Attributes.GetNamedItem("name").InnerText;
                        xlm.Text = node.InnerText;
                        IxmlSecond.Add(xlm);
                    }
                }
                List<XmlLanguageModel> SortedGlobal = IxmlGlobal.OrderBy(o => o.Name).ToList();
                List<XmlLanguageModel> SortedSecond = IxmlSecond.OrderBy(o => o.Name).ToList();
                List<XmlLanguageModel> DoseNotInGlobal = new List<XmlLanguageModel>();
                List<XmlLanguageModel> DoseNotInSecend = new List<XmlLanguageModel>();
                List<XmlLanguageModel> Repeated = new List<XmlLanguageModel>();


                #region trash

                //for (int i1 = 0; i1 < ((SortedGlobal.Count > SortedSecond.Count)? SortedGlobal.Count:SortedSecond.Count); i1++)
                //{
                //    if ((SortedGlobal.Count > i1) &&(!SortedSecond.Any(e => e.Name == SortedGlobal[i1].Name)))
                //    {
                //        SortedSecond.Insert(i1 + 1, SortedGlobal[i1]);
                //        DoseNotInSecend.Add(SortedGlobal[i1]);
                //    }
                //    if ((SortedSecond.Count > i1) && (!SortedGlobal.Any(e => e.Name == SortedSecond[i1].Name)))
                //    {
                //        SortedGlobal.Insert(i1 + 1, SortedSecond[i1]);
                //        DoseNotInGlobal.Add(SortedSecond[i1]);
                //    }
                //}
                #endregion

                for (int i1 = 0; i1 < SortedGlobal.Count; i1++)
                {
                    if (!SortedSecond.Any(e => e.Name == SortedGlobal[i1].Name))
                    {
                        SortedSecond.Insert(i1 + 1, SortedGlobal[i1]);
                        DoseNotInSecend.Add(SortedGlobal[i1]);
                    }
                }
                for (int i1 = 0; i1 < SortedSecond.Count; i1++)
                {
                    if (!SortedGlobal.Any(e => e.Name == SortedSecond[i1].Name))
                    {
                        SortedGlobal.Insert(i1 + 1, SortedSecond[i1]);
                        DoseNotInGlobal.Add(SortedSecond[i1]);
                    }
                }


                if (SortedGlobal.Count > SortedSecond.Count)
                {
                    for (int i = 0; i < SortedGlobal.Count; i++)
                    {
                        XmlLanguageViewModel xlm = new XmlLanguageViewModel();
                        xlm.ID = i;
                        xlm.Name = SortedGlobal[i].Name;
                        xlm.Text = SortedGlobal[i].Text;
                        xlm.GlobalID = SortedGlobal[i].ID;
                        xlm.GlobalText = SortedGlobal[i].Text;
                        xlvm.Add(xlm);
                    }
                    for (int i1 = 0; i1 < SortedSecond.Count; i1++)
                    {
                        xlvm.Where(w => w.Name == SortedSecond[i1].Name)
                            .ToList()
                            .ForEach(e => e.SecondID = SortedSecond[i1].ID);
                        xlvm.Where(w => w.Name == SortedSecond[i1].Name)
                            .ToList()
                            .ForEach(e => e.SecondText = SortedSecond[i1].Text);
                    }
                }
                else
                {
                    for (int i = 0; i < SortedSecond.Count; i++)
                    {
                        XmlLanguageViewModel xlm = new XmlLanguageViewModel();
                        xlm.ID = i;
                        xlm.Name = SortedSecond[i].Name;
                        xlm.Text = SortedSecond[i].Text;
                        xlm.SecondID = SortedSecond[i].ID;
                        xlm.SecondText = SortedSecond[i].Text;
                        xlvm.Add(xlm);
                    }
                    for (int i1 = 0; i1 < SortedGlobal.Count; i1++)
                    {
                        xlvm.Where(w => w.Name == SortedGlobal[i1].Name)
                            .ToList()
                            .ForEach(e => e.GlobalID = SortedGlobal[i1].ID);
                        xlvm.Where(w => w.Name == SortedGlobal[i1].Name)
                            .ToList()
                            .ForEach(e => e.GlobalText = SortedGlobal[i1].Text);
                    }
                }
                DataGrid_BinDefinition.ItemsSource = xlvm;
                ButtonSaveFile.IsEnabled = true;
            }
        }

        public string Name { get; set; }
        public string Value { get; set; }
        public string[] GetElementStrings(IEnumerable<string> Input)
        {
            int i = 0;
            foreach (var Lines in Input)
            {

            }

            return null;
        }

        private void ButtonSaveFile_Click(object sender, RoutedEventArgs e)
        {
            List<string> linesMain = new List<string>();
            List<string> linesSec = new List<string>();
            linesMain.Add("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
            linesMain.Add("<localizationDictionary culture=\"" + txtMain.Text+ "\">");
            linesMain.Add("\t<texts>");
              

            linesSec.Add("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
            linesSec.Add("<localizationDictionary culture=\"" + txtSecond.Text + "\">");
            linesSec.Add("\t<texts>");
            List<XmlLanguageModel> IxmlGlobal = new List<XmlLanguageModel>();
            List<XmlLanguageModel> IxmlSecond = new List<XmlLanguageModel>();
            foreach (var item in xlvm)
            {
                {
                    XmlLanguageModel xlm = new XmlLanguageModel();
                    xlm.ID = item.GlobalID;
                    xlm.Name = item.Name;
                    xlm.Text = item.GlobalText;
                    if (xlm.Text.Contains("&"))
                    {
                        xlm.Text = xlm.Text.Replace("&", "&amp;");
                    }
                    if (xlm.Text.Contains("<"))
                    {
                        xlm.Text = xlm.Text.Replace("<", "&lt;");
                    }
                    if (xlm.Text.Contains(">"))
                    {
                        xlm.Text = xlm.Text.Replace(">", "&gt;");

                    }
                    IxmlGlobal.Add(xlm);
                }
                {
                    XmlLanguageModel xlm = new XmlLanguageModel();
                    xlm.ID = item.SecondID;
                    xlm.Name = item.Name;
                    xlm.Text = item.SecondText;
                    if (xlm.Text.Contains("&"))
                    {
                        xlm.Text = xlm.Text.Replace("&", "&amp;");
                    }
                    if (xlm.Text.Contains("<"))
                    {
                        xlm.Text = xlm.Text.Replace("<", "&lt;");
                    }
                    if (xlm.Text.Contains(">"))
                    {
                        xlm.Text = xlm.Text.Replace(">", "&gt;");

                    }
                    IxmlSecond.Add(xlm);
                }
            }
            IxmlGlobal = IxmlGlobal.OrderBy(o => o.ID).ToList();
            IxmlSecond = IxmlSecond.OrderBy(o => o.ID).ToList();

            for (int i = 0; i < xlvm.Count; i++)
            {
                linesMain.Add("\t\t<text name=\""+IxmlGlobal[i].Name +"\">"+IxmlGlobal[i].Text+ "</text>");
                linesSec.Add("\t\t<text name=\"" + IxmlSecond[i].Name + "\">" + IxmlSecond[i].Text + "</text>");
            }
            linesMain.Add("\t</texts>");
            linesMain.Add("</localizationDictionary>");
            linesSec.Add("\t</texts>");
            linesSec.Add("</localizationDictionary>");
            using (StreamWriter sw = new StreamWriter(fileNameMain))
            {
                foreach (var x in linesMain)
                    sw.WriteLine(x);
                //sw.Write(linesMain.ToArray<string>().ToString());
            }
            using (StreamWriter sw = new StreamWriter(fileNameSec))
            {
                foreach (var x in linesSec)
                    sw.WriteLine(x);
                //sw.Write(linesSec.ToArray<string>().ToString());
            }
        }
    }
}
