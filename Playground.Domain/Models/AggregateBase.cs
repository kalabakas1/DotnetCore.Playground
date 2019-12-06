namespace Playground.Domain.Models
{
    public abstract class AggregateBase<T> where T : struct
    {
        public T Id { get; set; }
    }
}