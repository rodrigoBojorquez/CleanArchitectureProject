using BuberDinner.Domain.Common.Models;
using BuberDinner.Domain.Menu.ValueObjects;

namespace BuberDinner.Domain.Menu.Entities;

public sealed class MenuSection : Entity<MenuSectionId>
{
    private readonly List<MenuItem> _items = new();
    public string Name { get; }
    public string Description { get; }
    
    /* 
        Tambien puede usarse el metodo
        ToList() para permitir la modificacion
        de la lista de items.

        Esto de acuerdo a la logica de negocio
    */

    public IReadOnlyList<MenuItem> Items => _items.AsReadOnly();

    private MenuSection(MenuSectionId id, string name, string description) : base(id)
    {
        Name = name;
        Description = description;
    }

    public static MenuSection Create(string name, string description)
    {
        return new MenuSection(MenuSectionId.CreateUnique(), name, description);
    }
}   