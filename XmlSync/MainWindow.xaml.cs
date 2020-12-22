using System;
using System.Collections.Generic;
using System.Dynamic;
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
        List<XmlLanguageModel> xlms = new List<XmlLanguageModel>();
        List<Languages> lans = new List<Languages>();
        List<Languages> fixed_lans = new List<Languages>();
        string fileNameMain = "";
        string fileNameSec = "";
        private void ButtonReadFile_Click(object sender, RoutedEventArgs e)
        {
            //List<XmlLanguageModel> IxmlGlobal = new List<XmlLanguageModel>();
            //List<XmlLanguageModel> IxmlSecond = new List<XmlLanguageModel>();
            //OpenFileDialog openFileDialog = new OpenFileDialog();
            //if (openFileDialog.ShowDialog() == true)
            //{
            //    {
            //        XmlDocument xmlDoc = new XmlDocument();
            //        xmlDoc.Load(openFileDialog.FileName);
            //        fileNameMain = openFileDialog.FileName;
            //        var cultureMain = xmlDoc.DocumentElement.SelectNodes("/localizationDictionary")[0].Attributes.GetNamedItem("culture").InnerText;
            //        txtMain.Text = cultureMain.ToString();
            //        XmlNodeList nodeList = xmlDoc.DocumentElement.SelectNodes("/localizationDictionary/texts/text");
            //        int i = 0;
            //        foreach (XmlNode node in nodeList)
            //        {
            //            XmlLanguageModel xlm = new XmlLanguageModel();
            //            xlm.ID = i;
            //            xlm.Name = node.Attributes.GetNamedItem("name").InnerText;
            //            xlm.Text = node.InnerText;
            //            IxmlGlobal.Add(xlm);
            //        }
            //    }
            //    OpenFileDialog openFileDialog1 = new OpenFileDialog();
            //    if (openFileDialog1.ShowDialog() == true)
            //    {
            //        XmlDocument xmlDoc1 = new XmlDocument();
            //        xmlDoc1.Load(openFileDialog1.FileName);
            //        fileNameSec = openFileDialog1.FileName;
            //        var cultureSec = xmlDoc1.DocumentElement.SelectNodes("/localizationDictionary")[0].Attributes.GetNamedItem("culture").InnerText;
            //        txtSecond.Text = cultureSec.ToString();
            //        XmlNodeList nodeList1 = xmlDoc1.DocumentElement.SelectNodes("/localizationDictionary/texts/text");
            //        /*xmlGlobal = new XmlLanguageModel[nodeList.Count]0;*/
            //        int i1 = 0;
            //        foreach (XmlNode node in nodeList1)
            //        {
            //            XmlLanguageModel xlm = new XmlLanguageModel();
            //            xlm.ID = i1;
            //            xlm.Name = node.Attributes.GetNamedItem("name").InnerText;
            //            xlm.Text = node.InnerText;
            //            IxmlSecond.Add(xlm);
            //        }
            //    }
            //    List<XmlLanguageModel> SortedGlobal = IxmlGlobal.OrderBy(o => o.Name).ToList();
            //    List<XmlLanguageModel> SortedSecond = IxmlSecond.OrderBy(o => o.Name).ToList();
            //    List<XmlLanguageModel> DoseNotInGlobal = new List<XmlLanguageModel>();
            //    List<XmlLanguageModel> DoseNotInSecend = new List<XmlLanguageModel>();
            //    List<XmlLanguageModel> Repeated = new List<XmlLanguageModel>();


            //    #region trash

            //    //for (int i1 = 0; i1 < ((SortedGlobal.Count > SortedSecond.Count)? SortedGlobal.Count:SortedSecond.Count); i1++)
            //    //{
            //    //    if ((SortedGlobal.Count > i1) &&(!SortedSecond.Any(e => e.Name == SortedGlobal[i1].Name)))
            //    //    {
            //    //        SortedSecond.Insert(i1 + 1, SortedGlobal[i1]);
            //    //        DoseNotInSecend.Add(SortedGlobal[i1]);
            //    //    }
            //    //    if ((SortedSecond.Count > i1) && (!SortedGlobal.Any(e => e.Name == SortedSecond[i1].Name)))
            //    //    {
            //    //        SortedGlobal.Insert(i1 + 1, SortedSecond[i1]);
            //    //        DoseNotInGlobal.Add(SortedSecond[i1]);
            //    //    }
            //    //}
            //    #endregion

            //    for (int i1 = 0; i1 < SortedGlobal.Count; i1++)
            //    {
            //        if (!SortedSecond.Any(e => e.Name == SortedGlobal[i1].Name))
            //        {
            //            SortedSecond.Insert(i1 + 1, SortedGlobal[i1]);
            //            DoseNotInSecend.Add(SortedGlobal[i1]);
            //        }
            //    }
            //    for (int i1 = 0; i1 < SortedSecond.Count; i1++)
            //    {
            //        if (!SortedGlobal.Any(e => e.Name == SortedSecond[i1].Name))
            //        {
            //            SortedGlobal.Insert(i1 + 1, SortedSecond[i1]);
            //            DoseNotInGlobal.Add(SortedSecond[i1]);
            //        }
            //    }


            //    if (SortedGlobal.Count > SortedSecond.Count)
            //    {
            //        for (int i = 0; i < SortedGlobal.Count; i++)
            //        {
            //            XmlLanguageViewModel xlm = new XmlLanguageViewModel();
            //            xlm.ID = i;
            //            xlm.Name = SortedGlobal[i].Name;
            //            xlm.Text = SortedGlobal[i].Text;
            //            xlm.GlobalID = SortedGlobal[i].ID;
            //            xlm.GlobalText = SortedGlobal[i].Text;
            //            xlvm.Add(xlm);
            //        }
            //        for (int i1 = 0; i1 < SortedSecond.Count; i1++)
            //        {
            //            xlvm.Where(w => w.Name == SortedSecond[i1].Name)
            //                .ToList()
            //                .ForEach(e => e.SecondID = SortedSecond[i1].ID);
            //            xlvm.Where(w => w.Name == SortedSecond[i1].Name)
            //                .ToList()
            //                .ForEach(e => e.SecondText = SortedSecond[i1].Text);
            //        }
            //    }
            //    else
            //    {
            //        for (int i = 0; i < SortedSecond.Count; i++)
            //        {
            //            XmlLanguageViewModel xlm = new XmlLanguageViewModel();
            //            xlm.ID = i;
            //            xlm.Name = SortedSecond[i].Name;
            //            xlm.Text = SortedSecond[i].Text;
            //            xlm.SecondID = SortedSecond[i].ID;
            //            xlm.SecondText = SortedSecond[i].Text;
            //            xlvm.Add(xlm);
            //        }
            //        for (int i1 = 0; i1 < SortedGlobal.Count; i1++)
            //        {
            //            xlvm.Where(w => w.Name == SortedGlobal[i1].Name)
            //                .ToList()
            //                .ForEach(e => e.GlobalID = SortedGlobal[i1].ID);
            //            xlvm.Where(w => w.Name == SortedGlobal[i1].Name)
            //                .ToList()
            //                .ForEach(e => e.GlobalText = SortedGlobal[i1].Text);
            //        }
            //    }
            //    DataGrid_BinDefinition.ItemsSource = xlvm;
            //    ButtonSaveFile.IsEnabled = true;
            //}
        }


        public static List<Languages> Load(List<Languages> allLanguages)
        {
            for (int i = 0; i < allLanguages.Count; i++)
            {
                allLanguages[i].SortModel();
            }
            for (int i = 0; i < allLanguages.Count - 1; i++)
            {
                var first = allLanguages[i].LanguageModel;
                var sec = allLanguages[i + 1].LanguageModel;
                for (int j = 0; j < first.Count; j++)
                {
                    if (!sec.Any(e => e.Name == first[j].Name) && i+1>=allLanguages.Count)
                    {
                        var temp = first[j];
                        temp.ID = sec.Count;
                        allLanguages[i + 1].LanguageModel.Insert(j + 1, temp);
                    }
                }
                for (int j = 0; j < sec.Count; j++)
                {
                    if (!first.Any(e => e.Name == sec[j].Name))
                    {
                        var temp = sec[j];
                        temp.ID = first.Count;
                        allLanguages[i].LanguageModel.Insert(j + 1, temp);
                    }
                }
            }
            return allLanguages;
        }

        private void ButtonSaveFile_Click(object sender, RoutedEventArgs e)
        {
            //List<string> linesMain = new List<string>();
            //List<string> linesSec = new List<string>();
            //linesMain.Add("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
            //linesMain.Add("<localizationDictionary culture=\"" + txtMain.Text + "\">");
            //linesMain.Add("\t<texts>");


            //linesSec.Add("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
            //linesSec.Add("<localizationDictionary culture=\"" + txtSecond.Text + "\">");
            //linesSec.Add("\t<texts>");
            //List<XmlLanguageModel> IxmlGlobal = new List<XmlLanguageModel>();
            //List<XmlLanguageModel> IxmlSecond = new List<XmlLanguageModel>();
            //foreach (var item in xlvm)
            //{
            //    {
            //        XmlLanguageModel xlm = new XmlLanguageModel();
            //        xlm.ID = item.GlobalID;
            //        xlm.Name = item.Name;
            //        xlm.Text = item.GlobalText;
            //        if (xlm.Text.Contains("&"))
            //        {
            //            xlm.Text = xlm.Text.Replace("&", "&amp;");
            //        }
            //        if (xlm.Text.Contains("<"))
            //        {
            //            xlm.Text = xlm.Text.Replace("<", "&lt;");
            //        }
            //        if (xlm.Text.Contains(">"))
            //        {
            //            xlm.Text = xlm.Text.Replace(">", "&gt;");

            //        }
            //        IxmlGlobal.Add(xlm);
            //    }
            //    {
            //        XmlLanguageModel xlm = new XmlLanguageModel();
            //        xlm.ID = item.SecondID;
            //        xlm.Name = item.Name;
            //        xlm.Text = item.SecondText;
            //        if (xlm.Text.Contains("&"))
            //        {
            //            xlm.Text = xlm.Text.Replace("&", "&amp;");
            //        }
            //        if (xlm.Text.Contains("<"))
            //        {
            //            xlm.Text = xlm.Text.Replace("<", "&lt;");
            //        }
            //        if (xlm.Text.Contains(">"))
            //        {
            //            xlm.Text = xlm.Text.Replace(">", "&gt;");

            //        }
            //        IxmlSecond.Add(xlm);
            //    }
            //}
            //IxmlGlobal = IxmlGlobal.OrderBy(o => o.ID).ToList();
            //IxmlSecond = IxmlSecond.OrderBy(o => o.ID).ToList();

            //for (int i = 0; i < xlvm.Count; i++)
            //{
            //    linesMain.Add("\t\t<text name=\"" + IxmlGlobal[i].Name + "\">" + IxmlGlobal[i].Text + "</text>");
            //    linesSec.Add("\t\t<text name=\"" + IxmlSecond[i].Name + "\">" + IxmlSecond[i].Text + "</text>");
            //}
            //linesMain.Add("\t</texts>");
            //linesMain.Add("</localizationDictionary>");
            //linesSec.Add("\t</texts>");
            //linesSec.Add("</localizationDictionary>");
            //using (StreamWriter sw = new StreamWriter(fileNameMain))
            //{
            //    foreach (var x in linesMain)
            //        sw.WriteLine(x);
            //    //sw.Write(linesMain.ToArray<string>().ToString());
            //}
            //using (StreamWriter sw = new StreamWriter(fileNameSec))
            //{
            //    foreach (var x in linesSec)
            //        sw.WriteLine(x);
            //    //sw.Write(linesSec.ToArray<string>().ToString());
            //}
        }
        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            Button btnRemove = (Button)sender;
            int count = Grid.GetRow(btnRemove);
            TextBox txtLabel = (TextBox)FindName("txtLabel" + count);
            TextBox txtPath = (TextBox)FindName("txtPath" + count);
            Button btnAdd = (Button)FindName("btnAdd" + count);
            btnAdd.Visibility = Visibility.Visible;
            txtPath.Visibility = Visibility.Hidden;
            btnRemove.Visibility = Visibility.Hidden;

        }
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            Button btnAdd = (Button)sender;
            int count = Grid.GetRow(btnAdd);

            TextBox txtLabel = (TextBox)FindName("txtLabel" + count);
            TextBox txtPath = (TextBox)FindName("txtPath" + count);
            Button btnRemove = (Button)FindName("txtRemove" + count);
            if (txtLabel != null && !txtLabel.Text.Equals(""))
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "XML Files|*.xml";
                openFileDialog.Title = "Please Add Language";
                openFileDialog.Multiselect = false;
                if (openFileDialog.ShowDialog() == true && openFileDialog.CheckFileExists)
                {
                    Languages temp = new Languages();
                    temp.fileID = lans.Count;
                    temp.fileName = openFileDialog.SafeFileName;
                    temp.filePath = openFileDialog.FileName;
                    temp.lanID = lans.Count;
                    temp.lanLabel = txtLabel.Text;
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.Load(temp.filePath);
                    var culture = xmlDoc.DocumentElement.SelectNodes("/localizationDictionary")[0].Attributes.GetNamedItem("culture").InnerText;
                    txtPath.Text = temp.filePath;
                    XmlNodeList nodeList = xmlDoc.DocumentElement.SelectNodes("/localizationDictionary/texts/text");
                    int i = 0;
                    foreach (XmlNode node in nodeList)
                    {
                        XmlLanguageModel xlm = new XmlLanguageModel();
                        xlm.ID = i++;
                        xlm.Name = node.Attributes.GetNamedItem("name").InnerText;
                        xlm.Text = node.InnerText;
                        temp.InsertModel(xlm);
                    }
                    lans.Add(temp);
                    btnAdd.Visibility = Visibility.Hidden;
                    txtPath.Visibility = Visibility.Visible;
                    RowDefinition rowDef = new RowDefinition();
                    rowDef.Name = "row" + lans.Count;
                    rowDef.Height = GridLength.Auto;
                    GridTop.RowDefinitions.Add(rowDef);
                    count++;
                    Button btnAddnext = new Button();
                    TextBox txtLabelnext = new TextBox();
                    TextBox txtPathnext = new TextBox();
                    TextBox txtCountnext = new TextBox();
                    Button btnRemovenext = new Button();
                    //VisualState txtLabelnext = new VisualState();
                    //txtLabelnext.SetValue(FrameworkElement..);
                    //btnAddnext = btnAdd;
                    //txtLabelnext = txtLabel;
                    //txtPathnext = txtPath;
                    //btnRemovenext = btnRemove;
                    btnAddnext.Name = "btnAdd" + count;
                    txtLabelnext.Name = "txtLabel" + count;
                    txtPathnext.Name = "txtPath" + count;
                    txtCountnext.Name = "txtCount" + count;
                    this.RegisterName("btnAdd" + count, btnAddnext);
                    this.RegisterName("txtLabel" + count, txtLabelnext);
                    this.RegisterName("txtPath" + count, txtPathnext);
                    this.RegisterName("txtCount" + count, txtCountnext);
                    btnAddnext.SetValue(FrameworkElement.NameProperty, "btnAdd" + count);
                    txtLabelnext.SetValue(FrameworkElement.NameProperty, "txtLabel" + count);
                    txtPathnext.SetValue(FrameworkElement.NameProperty, "txtPath" + count);
                    txtCountnext.SetValue(FrameworkElement.NameProperty, "txtCount" + count);

                    txtCountnext.Text = count.ToString();
                    txtPathnext.Visibility = Visibility.Hidden;
                    btnAddnext.Content = "Add Another Language";
                    btnAddnext.AddHandler(Button.ClickEvent, new RoutedEventHandler(btnAdd_Click));

                    RowDefinition rowDef1 = new RowDefinition();
                    GridTop.RowDefinitions.Add(rowDef1);

                    Grid.SetRow(txtCountnext, count);
                    Grid.SetRow(btnAddnext, count);
                    Grid.SetRow(txtLabelnext, count);
                    Grid.SetRow(txtPathnext, count);
                    Grid.SetColumn(txtCountnext, 0);
                    Grid.SetColumn(btnAddnext, 2);
                    Grid.SetColumn(txtLabelnext, 1);
                    Grid.SetColumn(txtPathnext, 2);
                    this.GridTop.Children.Add(txtCountnext);
                    this.GridTop.Children.Add(btnAddnext);
                    this.GridTop.Children.Add(txtLabelnext);
                    this.GridTop.Children.Add(txtPathnext);
                    //btnRemovenext.SetValue(Grid.RowProperty, count);
                    //GridTop.Children.Add(btnAddnext);
                    //GridTop.Children.Add(txtLabelnext);
                    //GridTop.Children.Add(txtPathnext);
                    //GridTop.Children.Add(btnRemovenext);
                    //btnRemove.Visibility = Visibility.Visible;
                }
            }
            else
            {
                MessageBox.Show("Please define a name for this Language");
            }
        }

        public static void UpdateGrid()
        {
            DataGrid dg = new DataGrid();



            DataGridTextColumn textColumn = new DataGridTextColumn();
            textColumn.Header = "First Name";
            textColumn.Binding = new Binding("FirstName");
            //dgLans.Columns.Add(textColumn);
        }

        public class Field
        {
            public Field(string name, Type type)
            {
                this.FieldName = name;
                this.FieldType = type;
            }

            public string FieldName;

            public Type FieldType;
        }

        public class DynamicClass : DynamicObject
        {
            private Dictionary<string, KeyValuePair<Type, object>> _fields;

            public DynamicClass(List<Field> fields)
            {
                _fields = new Dictionary<string, KeyValuePair<Type, object>>();
                fields.ForEach(x => _fields.Add(x.FieldName,
                    new KeyValuePair<Type, object>(x.FieldType, null)));
            }

            public override bool TrySetMember(SetMemberBinder binder, object value)
            {
                if (_fields.ContainsKey(binder.Name))
                {
                    var type = _fields[binder.Name].Key;
                    if (value.GetType() == type)
                    {
                        _fields[binder.Name] = new KeyValuePair<Type, object>(type, value);
                        return true;
                    }
                    else throw new Exception("Value " + value + " is not of type " + type.Name);
                }
                return false;
            }

            public override bool TryGetMember(GetMemberBinder binder, out object result)
            {
                result = _fields[binder.Name].Value;
                return true;
            }
        }
        public static void AddProperty(ExpandoObject expando, string propertyName, object propertyValue)
        {
            // ExpandoObject supports IDictionary so we can extend it like this
            var expandoDict = expando as IDictionary<string, object>;
            if (expandoDict.ContainsKey(propertyName))
                expandoDict[propertyName] = propertyValue;
            else
                expandoDict.Add(propertyName, propertyValue);
        }
        List<XmlMultiLanguageViewModel> xmlvm = new List<XmlMultiLanguageViewModel>();
        private List<dynamic> dataList;
        private void btnLoadFile_Click(object sender, RoutedEventArgs e)
        {
            DataGrid dg = new DataGrid();
            dg.AutoGenerateColumns = false;
            dg.CanUserAddRows = false;
            dg.CanUserDeleteRows = false;
            dg.CanUserReorderColumns = false;
            dg.CanUserResizeColumns = true;
            dg.CanUserResizeRows = true;
            dg.CanUserSortColumns = false;
            dg.HorizontalContentAlignment = HorizontalAlignment.Center;
            Grid.SetRow(dg, 1);
            Grid.SetColumn(dg, 0);
            Grid.SetColumnSpan(dg, 2);
            this.GridMain.Children.Add(dg);
            fixed_lans = Load(lans);

            string[] lansName = new string[fixed_lans.Count];
            for (int i = 0; i < fixed_lans.Count; i++)
            {
                lansName[i] = fixed_lans[i].lanLabel;
            }

            //List<string> lansName = new List<string>();
            //for (int i = 0; i < fixed_lans.Count; i++)
            //{
            //    lansName.Add(fixed_lans[i].lanLabel);
            //}
            var fields = new List<Field>();
            fields.Add(new Field("Name", typeof(string)));
            for (int i = 0; i < fixed_lans.Count; i++)
            {
                fields.Add(new Field("ID" + fixed_lans[i].lanLabel, typeof(int)));
                fields.Add(new Field(""+fixed_lans[i].lanLabel, typeof(string)));
            }

            dynamic obj = new DynamicClass(fields);

            List<dynamic> expando = new List<dynamic>();

            for (int i = 0; i < fixed_lans.Count; i++)
            {
                for (int j = 0; j < fixed_lans[i].LanguageModel.Count; j++)
                {
                    ExpandoObject eo = new ExpandoObject();
                    if (i == 0)
                    {
                        AddProperty(eo, "Name", fixed_lans[i].LanguageModel[j].Name);
                    }
                    AddProperty(eo, "ID" + fixed_lans[i].lanLabel, fixed_lans[i].LanguageModel[j].ID);
                    AddProperty(eo, "" + fixed_lans[i].lanLabel, fixed_lans[i].LanguageModel[j].Name);
                    if (i+1<fixed_lans.Count)
                    {
                        AddProperty(eo, "ID" + fixed_lans[i + 1].lanLabel, fixed_lans[i].LanguageModel[j].ID);
                        AddProperty(eo, "" + fixed_lans[i + 1].lanLabel, fixed_lans[i].LanguageModel[j].Name);
                    }
                    expando.Add(eo);
                }
            }
            //dataList = new List<dynamic>()
            //{
            //    "en",
            //    "fa"
            //};

            //dynamic EN = new ExpandoObject();
            //EN.Text = fixed_lans[0].LanguageModel;
            //dynamic FA = new ExpandoObject();
            //FA.Text = fixed_lans[1].LanguageModel;
            //
            //dynamic thelist = new ExpandoObject();
            //thelist.enText = fixed_lans[0].LanguageModel;
            //thelist.faText = fixed_lans[1].LanguageModel;
            //
            //var dd = new List<dynamic>();
            //
            //
            //dd.Add(thelist);
            //dataList.Add(FA);

            //dataList.Add(new { V = lansName[0],fixed_lans[0].LanguageModel});
            //dataList.Add(new { V = lansName[1], fixed_lans[1].LanguageModel });

            //dataList.

            //DataGridTextColumn[] Columns = new DataGridTextColumn[10]();
            //var temp = fixed_lans.Join(fixed_lans, st =>
            //    fixed_lans.IndexOf(st), et =>
            //    fixed_lans.IndexOf(et), (st, et) =>
            //    new { newst = st, newet = et }).ToList();
            bool isNameAdded = false;
            foreach (var data in fixed_lans)
            {
                if (!isNameAdded)
                {
                    DataGridTextColumn ColumnName = new DataGridTextColumn();
                    ColumnName.Header = "Name";
                    ColumnName.Binding = new Binding("Name");
                    dg.Columns.Add(ColumnName);
                    isNameAdded = true;
                }
                DataGridTextColumn Column = new DataGridTextColumn();
                Column.Header = data.lanLabel;
                Column.Binding = new Binding(data.lanLabel);
                dg.Columns.Add(Column);
                //xlvm.Add();
            }

            //dg.ItemsSource = fixed_lans;
            dg.ItemsSource = expando;

            //Load(lans);
        }
    }

}
