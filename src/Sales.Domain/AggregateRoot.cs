namespace Sales.Domain
{
  public abstract class AggregateRoot : IDependencySetter
  {
    protected bool Equals(AggregateRoot other)
    {
      return string.Equals(_id, other._id);
    }

    public override bool Equals(object obj)
    {
      if (ReferenceEquals(null, obj)) return false;
      if (ReferenceEquals(this, obj)) return true;
      if (obj.GetType() != this.GetType()) return false;
      return Equals((AggregateRoot) obj);
    }

    public override int GetHashCode()
    {
      return (_id != null ? _id.GetHashCode() : 0);
    }

    public enum AggregateStatus
    {
      ACTIVE, ARCHIVE
    }

    private string _id;
    public string Id => _id;

    private int _version;

    private AggregateStatus _aggregateStatus = AggregateStatus.ACTIVE;
    private IEventBus _eventBus;

    protected IEventBus EventPublisher => _eventBus;

    public AggregateRoot(string id)
    {
      _id = id;
    }

    public void IncreaseVersion()
    {
      _version++;
    }

    public void MarkAsRemoved()
    {
      _aggregateStatus = AggregateStatus.ARCHIVE;
    }

    void IDependencySetter.SetEventPublisher(IEventBus eventBus)
    {
      _eventBus = eventBus;
    }

    public bool IsRemoved()
    {
      return _aggregateStatus == AggregateStatus.ARCHIVE;
    }

    protected void DomainError(string message)
    {
      throw new DomainOperationException(Id, message);
    }
  }
}