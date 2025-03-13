namespace LibraryManagementSystem.Core.Entities;

public abstract class BaseEntity
{
    protected BaseEntity()
    {
        Id = Guid.NewGuid();
        CreatedAt = DateTime.UtcNow;
    }

    public Guid Id { get; protected set; }
    public DateTime CreatedAt { get; protected set; }

    // Override do método Equals para comparar entidades com base no Id
    public override bool Equals(object? obj)
    {
        if (obj == null || obj is not BaseEntity)
            return false;

        if (ReferenceEquals(this, obj))
            return true;

        var other = (BaseEntity)obj;
        return Id.Equals(other.Id);
    }

    // Override do GetHashCode utilizando o Id
    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }

    // Sobrecarga dos operadores de igualdade
    public static bool operator ==(BaseEntity a, BaseEntity b)
    {
        if (a is null && b is null)
            return true;

        if (a is null || b is null)
            return false;

        return a.Equals(b);
    }

    public static bool operator !=(BaseEntity a, BaseEntity b)
    {
        return !(a == b);
    }

    // Override do ToString para facilitar a visualização
    public override string ToString()
    {
        return $"{GetType().Name} [Id={Id}, CreatedAt={CreatedAt}]";
    }
}
