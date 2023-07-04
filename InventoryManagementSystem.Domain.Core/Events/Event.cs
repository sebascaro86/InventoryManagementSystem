namespace InventoryManagementSystem.Domain.Core.Events
{
    public abstract class Event
    {
        public DateTime Tiemestamp { get; protected set; }

        protected Event()
        {
            Tiemestamp = DateTime.Now;
        }
    }
}
