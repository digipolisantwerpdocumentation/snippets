using System;

namespace OutputGeneratorExample.models
{
    public class DocumentRedirectResult
    {
        public string uploadUri { get; set; }
        public string name { get; set; }
        public string id { get; set; }
        public string cephId { get; set; }
        public DateTime creationDate { get; set; }
        
        public string status { get; set; }
        public string statusAsEnum { get; set; }
        public string elementPackage { get; set; }
        public string elementName { get; set; }

        public Creator creator { get; set; }
    }
}