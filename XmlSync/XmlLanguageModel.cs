using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XmlSync
{
    public class XmlLanguageModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
    }

    public class XmlLanguageViewModel : XmlLanguageModel
    {
        public int GlobalID { get; set; }
        public string GlobalText { get; set; }
        public int SecondID { get; set; }
        public string SecondText { get; set; }
    }
    public class XmlMultiLanguageViewModel
    {
        public int ID { get; set; }
        public int[] IDNames { get; set; }
        public string[] Names { get; set; }
        public string[] Texts { get; set; }
    }
    public class Files
    {
        public int fileID { get; set; }
        public string fileName { get; set; }
        public string filePath { get; set; }
    }
    public class Languages : Files
    {
        public Languages()
        {
            isList = false;
        }
        public int lanID { get; set; }
        public string lanLabel { get; set; }
        //public List<XmlLanguageModel> LanguageModel { get; set; }
        public List<XmlLanguageModel> LanguageModel;
        private bool isList { get; set; }
        private void CreateLanguageModel()
        {
            if (!isList)
            {
                LanguageModel = new List<XmlLanguageModel>();
                isList = true;
            }
        }
        public void InsertModel(XmlLanguageModel xmlLanguageModel)
        {
            CreateLanguageModel();
            LanguageModel.Add(xmlLanguageModel);
        }
        public void SortModel()
        {
            LanguageModel = LanguageModel.OrderBy(o => o.Name).ToList();
        }
        public int LengthModel()
        {
            return LanguageModel.Count;
        }
        public List<XmlLanguageModel> GetModel()
        {
            return LanguageModel;
        }
    }
}
