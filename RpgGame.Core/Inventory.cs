using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace RpgGame.Core
{
    public class Inventory
    {
        private readonly List<Weapon> _weapons = new List<Weapon>();
        private readonly List<Guard> _guards = new List<Guard>();
        private readonly List<Potion> _potions = new List<Potion>();

        public Inventory()
        {
            Weapons = new ReadOnlyCollection<Weapon>(_weapons);
            Guards = new ReadOnlyCollection<Guard>(_guards);
            Potions = new ReadOnlyCollection<Potion>(_potions);
        }

        public ReadOnlyCollection<Weapon> Weapons { get; }
        public ReadOnlyCollection<Potion> Potions { get; }
        public ReadOnlyCollection<Guard> Guards { get; }

        public void AddItems(List<Item> items)
        {
            foreach (var item in items)
            {
                AddItem(item);
            }
        }

        public void AddItem(Item item)
        {
            switch (item)
            {
                case Weapon w:
                    if (_weapons.Count == Consntants.MaxWeaponsOrGuards)
                    {
                        throw new ValidationException(Messages.MaxLimitWeaponsOrGuards);
                    }
                    _weapons.Add(w);
                    break;
                case Potion p:
                    if (_weapons.Count == Consntants.MaxPotions)
                    {
                        throw new ValidationException(Messages.MaxLimitPotions);
                    }
                    _potions.Add(p);
                    break;
                case Guard g:
                    if (_weapons.Count == Consntants.MaxWeaponsOrGuards)
                    {
                        throw new ValidationException(Messages.MaxLimitWeaponsOrGuards);
                    }
                    _guards.Add(g);
                    break;
            }
        }

        public void RemoveItems(List<Item> items)
        {
            foreach (var item in items)
            {
                RemoveItem(item);
            }
        }

        public void RemoveItem(Item item)
        {
            switch (item)
            {
                case Weapon w:
                    _weapons.Remove(w);
                    break;
                case Potion p:
                    _potions.Remove(p);
                    break;
                case Guard g:
                    _guards.Remove(g);
                    break;
            }
        }
    }
}