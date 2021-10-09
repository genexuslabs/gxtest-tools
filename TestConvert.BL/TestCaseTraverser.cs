using GeneXus.GXtest.Tools.TestConvert.BL.v3;
using System.Collections.Generic;

namespace GeneXus.GXtest.Tools.TestConvert.BL
{
    internal class TestCaseTraverser
    {
        public static IEnumerable<Element> GetElements(TestCase testCase, bool allowDuplicateNodes = false)
        {
            var traverser = new TestCaseTraverser(true, true, allowDuplicateNodes);
            return traverser.GetElements(testCase);
        }

        public static IEnumerable<Element> GetNodes(TestCase testCase, bool allowDuplicateNodes = false)
        {
            var traverser = new TestCaseTraverser(true, false, allowDuplicateNodes);
            return traverser.GetElements(testCase);
        }

        public static IEnumerable<Element> GetEdges(TestCase testCase)
        {
            var traverser = new TestCaseTraverser(false, true, false);
            return traverser.GetElements(testCase);
        }

        private readonly bool returnNodes = true;
        private readonly bool returnEdges = true;
        private readonly bool allowDuplicateNodes = false;

        private TestCaseTraverser(bool returnNodes, bool returnEdges, bool allowDuplicateNodes)
        {
            this.returnNodes = returnNodes;
            this.returnEdges = returnEdges;
            this.allowDuplicateNodes = allowDuplicateNodes;
        }

        private IEnumerable<Element> GetElements(TestCase testCase)
        {
            string startId = testCase.GeneralData.StartId;
            Element startElement = testCase.GetElement(startId);
            if (startElement == null)
                throw new System.Exception($"Could not find start element with Id='{startId}'.");

            Node node = startElement as Node
                        ?? throw new System.Exception($"Start element should be a Node, but found {startElement}.");

            var edgeEnumeratorByNodeId = new Dictionary<string, IEnumerator<Edge>>();
            foreach (Element element in TraverseTestCaseFromNode(edgeEnumeratorByNodeId, node))
                yield return element;
        }

        private IEnumerable<Element> TraverseTestCaseFromNode(Dictionary<string, IEnumerator<Edge>> edgeEnumerators, Node node)
        {
            // First time on this node?
            bool firstTimeOnNode = !edgeEnumerators.TryGetValue(node.Id, out var enumerator);
            if (firstTimeOnNode)
            {
                // get an edge enumerator to be used on this and later visits (if any)
                enumerator = node.OutboundEdges.GetEnumerator();

                // registering the enumerator also serves as indication that we have already visited this node
                edgeEnumerators.Add(node.Id, enumerator);
            }

            // return this node if needed
            if (returnNodes && (firstTimeOnNode || allowDuplicateNodes))
                yield return node;

            // After each visit, take a different edge out, until there is no more ways out
            if (enumerator.MoveNext())
            {
                // return the edge itself
                if (returnEdges)
                    yield return enumerator.Current;

                // and keep traversing from its target node on
                foreach (Element element in TraverseTestCaseFromNode(edgeEnumerators, enumerator.Current.TargetNode))
                    yield return element;
            }
        }
    }
}
