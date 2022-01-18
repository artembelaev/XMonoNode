namespace XMonoNode
{
    public interface IFlowNode : INode
    {
        void Flow(NodePort flowPort);
        void Stop();
        void TriggerFlow();
    }
}