using System;
using System.Collections;
using System.Collections.Generic;
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
}
