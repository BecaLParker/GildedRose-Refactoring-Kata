namespace GildedRose.ItemHandlers;

public class AgedBrieItemHandler(IItemHandler nextHandler) : IItemHandler
{
    public IItemHandler NextHandler { get; } = nextHandler;
    public bool CanHandleItem(Item item)
    {
        return item.Name == "Aged Brie";
    }
    
    public void HandleRequest(Item item)
    {
        if (CanHandleItem(item))
        {
            UpdateQuality(item);
        }
        else
        {
            NextHandler?.HandleRequest(item);
        }
    }

    public void UpdateQuality(Item item)
    {
        if (item.Quality < 50)
        {
            item.Quality += 1;
        }
    }

    public void UpdateSellIn(Item item)
    {
        item.SellIn -= 1;
    }
}