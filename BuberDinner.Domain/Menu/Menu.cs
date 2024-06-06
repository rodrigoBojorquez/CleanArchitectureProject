using BuberDinner.Domain.Common.Models;
using BuberDinner.Domain.Dinner.ValueObjects;
using BuberDinner.Domain.Host.ValueObjects;
using BuberDinner.Domain.Menu.Entities;
using BuberDinner.Domain.Menu.ValueObjects;
using BuberDinner.Domain.MenuReview.ValueObjects;

namespace BuberDinner.Domain.Menu;

public sealed class Menu : AggregateRoot<MenuId>
{
    /* 
        Las entidades o los otros aggregates
        no pueden ser modificados directamente
        por lo que hay que crear otras propiedades
        para acceder a ellos
    */
    private readonly List<MenuSection> _menuSections = new();
    private readonly List<DinnerId> _dinnerIds = new();
    private readonly List<MenuReviewId> _menuReviewIds = new();


    public string? Name { get;}
    public string? Description { get; }
    public float AverageRating { get; }
    public HostId? HostId { get; }
    public DateTime CreatedAt { get; }
    public DateTime UpdatedAt { get; }

    // Aqui se accede
    public IReadOnlyList<MenuSection> MenuSections => _menuSections.AsReadOnly();
    public IReadOnlyList<DinnerId> DinnerIds => _dinnerIds.AsReadOnly();
    public IReadOnlyList<MenuReviewId> MenuReviewIds => _menuReviewIds.AsReadOnly();

    private Menu (
        MenuId id,
        string name,
        string description,
        HostId hostId,
        DateTime createdAt,
        DateTime updatedAt
    ) : base(id)
    {
        Name = name;
        Description = description;
        HostId = hostId;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }
    
    public static Menu Create(
        string name,
        string description,
        HostId hostId
    )
    {
        return new Menu(
            MenuId.CreateUnique(),
            name,
            description,
            hostId,
            DateTime.UtcNow,
            DateTime.UtcNow
        );
    }
}