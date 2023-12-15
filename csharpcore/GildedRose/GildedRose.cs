using System.Collections.Generic;
using GildedRose.ItemHandlers;

namespace GildedRose;

public class GildedRose(IEnumerable<Item> items)
{
    public void Update()
    {
   
        var itemHandlerChain = 
            new AgedBrieItemHandler(
                new BackstagePassesItemHandler(
                    new SulfurasItemHandler(
                        new NormalItemHandler())));
        
        foreach (var item in items)
        {
            if (item.Name == null) continue;
            itemHandlerChain.HandleRequest(item);
        }
    }

    private static void UpdateQuality(Item item)
    {
            if (item.Name != "Aged Brie" && item.Name != "Backstage passes to a TAFKAL80ETC concert")
            {
                if (item.Quality > 0)
                {
                    if (item.Name != "Sulfuras, Hand of Ragnaros")
                    {
                        item.Quality -= 1;
                    }
                }
            }
            else
            {
                if (item.Quality < 50)
                {
                    item.Quality += 1;

                    if (item.Name == "Backstage passes to a TAFKAL80ETC concert")
                    {
                        if (item.SellIn < 11)
                        {
                            if (item.Quality < 50)
                            {
                                item.Quality += 1;
                            }
                        }

                        if (item.SellIn < 6)
                        {
                            if (item.Quality < 50)
                            {
                                item.Quality += 1;
                            }
                        }
                    }
                }
            }
            
            UpdateSellIn(item);

            if (item.SellIn >= 0) return;
            if (item.Name != "Aged Brie")
            {
                if (item.Name != "Backstage passes to a TAFKAL80ETC concert")
                {
                    if (item.Quality <= 0) return;
                    if (item.Name != "Sulfuras, Hand of Ragnaros")
                    {
                        item.Quality -= 1;
                    }
                }
                else
                {
                    item.Quality -= item.Quality;
                }
            }
            else
            {
                if (item.Quality < 50)
                {
                    item.Quality += 1;
                }
            }
    }
    private static void UpdateSellIn(Item item)
    {
        if (item.Name != "Sulfuras, Hand of Ragnaros")
        {
            item.SellIn -= 1;
        }
    }
}