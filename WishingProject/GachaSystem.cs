using System;
using System.Collections.Generic;
using System.Linq;

public class GachaSystem
{
    private List<Item> itemList;
    private Random random;
    private int pityCounter;

    public GachaSystem()
    {
        itemList = new List<Item>();
        random = new Random();
        pityCounter = 0;

        // Populate itemList with different items and their rarities
        // Adjust the probabilities and item details based on your game design
        itemList.Add(new Item("Gương ", Rarity.Common, 80));
        itemList.Add(new Item("Bút chì ", Rarity.Common, 80));
        itemList.Add(new Item("Figure Cùi ", Rarity.Rare, 15));
        itemList.Add(new Item("Figure Gundam ", Rarity.Epic, 4));
        itemList.Add(new Item("Chị Chủ Nhiệm", Rarity.Legendary, 1));
    }

    public (Item, Rarity) Pull()
    {
        pityCounter++;

        // Check if the pity counter has reached a certain threshold to guarantee a higher rarity item
        if (pityCounter >= 10)
        {
            pityCounter = 0;
            return (GetRandomItemWithMinimumRarity(Rarity.Rare), Rarity.Rare);
        }

        Item obtainedItem = GetRandomItem();
        return (obtainedItem, obtainedItem.ItemRarity);
    }



    private Item GetRandomItem()
    {
        int totalWeight = itemList.Sum(item => item.Probability);
        int randomNumber = random.Next(0, totalWeight);

        foreach (var item in itemList)
        {
            if (randomNumber < item.Probability)
            {
                return item;
            }

            randomNumber -= item.Probability;
        }

        // This should not happen under normal circumstances
        throw new InvalidOperationException("Unable to select a random item.");
    }

    private Item GetRandomItemWithMinimumRarity(Rarity minRarity)
    {
        var eligibleItems = itemList.Where(item => item.ItemRarity >= minRarity).ToList();
        return GetRandomItemFromList(eligibleItems);
    }

    private Item GetRandomItemFromList(List<Item> items)
    {
        int totalWeight = items.Sum(item => item.Probability);
        int randomNumber = random.Next(0, totalWeight);

        foreach (var item in items)
        {
            if (randomNumber < item.Probability)
            {
                return item;
            }

            randomNumber -= item.Probability;
        }

        // This should not happen under normal circumstances
        throw new InvalidOperationException("Unable to select a random item from the specified list.");
    }
}

public class Item
{
    public string Name { get; }
    public Rarity ItemRarity { get; }
    public int Probability { get; }

    public Item(string name, Rarity rarity, int probability)
    {
        Name = name;
        ItemRarity = rarity;
        Probability = probability;
    }
}

public enum Rarity
{
    Common,
    Rare,
    Epic,
    Legendary
}
