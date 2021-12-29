namespace CSB.Core.Services
{
    public interface IEncoder
    {
        byte[] GetBytes<T>(T data);
        T GetData<T>(byte[] bytes);
    }
}