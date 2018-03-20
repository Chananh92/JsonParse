using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    public class Tree
    {
        private Dictionary<int, RequestElement> _flatElements = new Dictionary<int, RequestElement>();

        private RequestElement _rootElement;
        public RequestElement RootElement
        {
            get { return _rootElement; }
            private set
            {
                _rootElement = value;
            }
        }
        public void AddRoot(int id, string name)
        {
            RootElement = new RequestNode { ID = id, Name = name };
            AddElement(id, RootElement);
        }

        public void AddRoot(int id, string name, object value)
        {
            RootElement = new RequestLeaf { ID = id, Name = name, Value = value };
            AddElement(id, RootElement);
        }

        public void AddNode(int id, int parentID, string name)
        {
            var node = new RequestNode { ID = id, Name = name};
            AddElement(id, node, parentID);
        }

        public void AddLeaf(int id, int parentID, string name, object value)
        {
            var leaf = new RequestLeaf { ID = id, Name = name, Value = value };
            AddElement(id, leaf, parentID);
        }

        private void AddElement(int id, RequestElement element, int? parentID = null)
        {
            if (parentID.HasValue)
            {
                var parentElement = _flatElements[parentID.Value] as RequestNode;

                parentElement.Children.Add(element);
            }

            _flatElements.Add(id, element);
        }
    }

    public abstract class RequestElement
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }

    public class RequestLeaf : RequestElement
    {
        public object Value { get; set; }
    }

    public class RequestNode : RequestElement
    {
        public IList<RequestElement> Children { get; set; }

        public RequestNode()
        {
            Children = new List<RequestElement>();
        }
    }
}
