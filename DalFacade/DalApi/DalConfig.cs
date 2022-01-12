using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace DalApi
{
    internal class DalConfig
    {
        internal static string DalName;
        internal static Dictionary<string, string> DalPackages;
        internal static Dictionary<string, string> DalNamespaces;
        internal static Dictionary<string, string> DalClasses;
        static DalConfig() {
            XElement dalConfig = XElement.Load(@"xml\dal-config.xml");
            DalName = dalConfig.Element("dal").Value;
            DalPackages = (from pkg in dalConfig.Element("dal-packages").Elements()
                           select pkg
                           ).ToDictionary(p => "" + p.Name, p => p.Value);
            DalNamespaces = (from pkg in dalConfig.Element("dal-packages").Elements()
                             select pkg
                          ).ToDictionary(p => "" + p.Name, p => p.Attribute("namespace").Value);
            DalClasses = (from pkg in dalConfig.Element("dal-packages").Elements()
                          select pkg
                          ).ToDictionary(p => "" + p.Name, p => p.Attribute("class").Value);

        }
    }
    public class DalConfigException : Exception
    {
        public DalConfigException(string msg) : base(msg) { }
        public DalConfigException(string msg, Exception ex) : base(msg, ex) { }
    }
}
