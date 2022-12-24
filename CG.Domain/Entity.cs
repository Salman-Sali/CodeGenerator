namespace CG.Domain
{
    public class Entity<T>
    {
        public T CreatedBy { get; private set; }
        public DateTime CreatedOn { get; private set; }
        public T UpdatedBy { get; private set; }
        public DateTime UpdatedOn { get; private set; }
    }
}
