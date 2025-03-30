namespace LibraryManagement.Core.Common;

public abstract class BaseEntity
{
    protected BaseEntity()
    {
        CreatedAt = DateTime.UtcNow;
    }

    public int Id { get; }
    public DateTime CreatedAt { get; }

    // Override do método Equals para comparar entidades com base no Id
    public override bool Equals(object? obj)
    {
        if (obj is not BaseEntity other)
            return false;

        return ReferenceEquals(this, other) || Id.Equals(other.Id);
    }

    // Override do GetHashCode utilizando o Id
    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }

    // Sobrecarga dos operadores de igualdade
    public static bool operator ==(BaseEntity? a, BaseEntity? b)
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
