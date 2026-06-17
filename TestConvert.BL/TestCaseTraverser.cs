// Copyright 2021 GeneXus S.A.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

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
