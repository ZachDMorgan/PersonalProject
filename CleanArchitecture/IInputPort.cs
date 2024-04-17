namespace CleanArchitecture
{

    public interface IInputPort<out TOutputPort> where TOutputPort : IOutputPort { }

}
