namespace AppCore.Interfaces
{
    public interface IBaseEntity<T>
    {
        T Id { get; set; }
    }
}