using System;

namespace XMonoNode
{
    public interface INodeGraph
    {
        int NodesCount
        {
            get;
        }
        void MoveNodeToTop(INode node);
        INode AddNode(Type type);
        void Clear();
        INodeGraph Copy();
        INode CopyNode(INode original);
        INode[] GetNodes();
        void RemoveNode(INode node);

        System.Type getNodeType();
    }
}