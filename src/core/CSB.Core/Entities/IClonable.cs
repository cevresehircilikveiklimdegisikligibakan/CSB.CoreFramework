namespace CSB.Core.Entities
{
    public interface IClonable<out T>
    {
        T Clone();
    }
}