namespace CleanArchitecture.Domain.Abstractions;

public abstract class Entity 
{
    private readonly List<IDomainEvent> _domainEvents = new();

    public Guid Id {get; init;}
    
    public IReadOnlyList<IDomainEvent> GetDomainEvents() => _domainEvents.AsReadOnly();

    public void ClearDomainEvents() 
    {
        _domainEvents.Clear();
    }
    
    protected void RaiseDomainEvent(IDomainEvent domainEvent) {
        _domainEvents.Add(domainEvent);
    }

    protected Entity(Guid id) 
    {
        Id = id;
    }
}