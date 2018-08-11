namespace Clifton.AgentSystem.Lib
{
    public interface IAgent
    {
        string Context { get; }
        string DataType { get; }

        void Call(IProcessor processor, dynamic data);
    }
}
