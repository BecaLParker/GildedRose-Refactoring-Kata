namespace GildedRose.ItemHandlers;

public class NormalItemHandler() : IItemHandler
{
    public IItemHandler NextHandler { get; } = null;

    public bool CanHandleItem(Item item)
    {
        return item.Name != null;
    }

    public void UpdateQuality(Item item)
    {
        if (item.Quality > 0)
        {
            item.Quality -= 1;
        }
    }

    public void UpdateSellIn(Item item)
    {
        item.SellIn -= 1;
    }
}