using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    class Program
    {
        public static int idCounter = 1;

        static void Main(string[] args)
        {
            string jsonText = System.IO.File.ReadAllText(@"json.txt");

            var requestElements = JsonParser.GetDictionary(jsonText);

            var rootElement = GetRequestRootElement(requestElements);
        }

        private static RequestElement GetRequestRootElement(IDictionary<string, object> requestElements)
        {
            List<RequestElement> elements = new List<RequestElement>();

            int rootId = idCounter;

            foreach (var item in requestElements)
            {
                var innerElements = GetElements(item);

                if (innerElements == null || innerElements.Count < 1)
                    continue;

                elements.AddRange(innerElements);
            }

            return CreateRequestElement(rootId, string.Empty, elements);
        }

        private static List<RequestElement> GetElements(KeyValuePair<string, object> dictValue)
        {
            List<RequestElement> requestElements = new List<RequestElement>();

            if (dictValue.Value is IDictionary<string, object>)
            {
                idCounter++;
                int tempIdCounter = idCounter;

                var dictValueDict = dictValue.Value as IDictionary<string, object>;

                var childrenRequestElements = new List<RequestElement>();

                foreach (var item in dictValueDict)
                {
                    childrenRequestElements.AddRange(GetElements(item));
                }
                var node = CreateRequestElement(tempIdCounter, dictValue.Key, childrenRequestElements);

                requestElements.Add(node);
            }
            else if (dictValue.Value is IEnumerable<object>)
            {
                var dictValueList = dictValue.Value as IEnumerable<object>;

                foreach (var item in dictValueList)
                {
                    var tempKeyvaluePair = new KeyValuePair<string, object>(dictValue.Key, item);

                    var childrenRequestElements = GetElements(tempKeyvaluePair);

                    requestElements.AddRange(childrenRequestElements);
                }
            }
            else
            {
                idCounter++;
                int tempIdCounter = idCounter;

                var leaf = new RequestLeaf()
                {
                    ID = tempIdCounter,
                    Name = dictValue.Key,
                    Value = dictValue.Value
                };

                requestElements.Add(leaf);
            }

            return requestElements;
        }

        private static RequestElement CreateRequestElement(int id, string name, IList<RequestElement> children)
        {
            RequestElement requestElement = null;

            if (children.Count > 1)
            {
                requestElement = new RequestNode()
                {
                    ID = id,
                    Name = name,
                    Children = children
                };
            }
            else
            {
                requestElement = new RequestLeaf()
                {
                    ID = id,
                    Name = name,
                    Value = children[0]
                };
            }

            return requestElement;
        }
    }
}
