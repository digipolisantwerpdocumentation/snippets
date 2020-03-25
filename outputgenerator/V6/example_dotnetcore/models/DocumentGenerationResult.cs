using System.Collections.Generic;

namespace OutputGeneratorExample.models
{
    public class DocumentGenerationResult
    {
        public List<object> messages { get; set; }
        public bool success { get; set; }
        public Value value { get; set; }
    }

    public class Value
    {
        public string creationDate { get; set; }
        public Creator creator { get; set; }
        public string elementName { get; set; }
        public string elementPackage { get; set; }
        public int id { get; set; }
        public string name { get; set; }
        public string uploadUri { get; set; }
    }
}