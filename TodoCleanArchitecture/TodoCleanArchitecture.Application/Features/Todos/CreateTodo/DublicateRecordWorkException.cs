namespace TodoCleanArchitecture.Application.Features.Todos.CreateTodo;
internal class DublicateRecordWorkException : Exception
{
    public DublicateRecordWorkException() : base("This record already exsist")
    {

    }
}
