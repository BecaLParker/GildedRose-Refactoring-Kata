namespace GildedRose.ItemHandlers;

public class BackstagePassesItemHandler(IItemHandler nextHandler) : IItemHandler
{
    public IItemHandler NextHandler { get; } = nextHandler;
    public bool CanHandleItem(Item item)
    {
        return item.Name == "Backstage passes to a TAFKAL80ETC concert";
    }

    public void UpdateQuality(Item item)
    {
        throw new System.NotImplementedException();
    }

    public void UpdateSellIn(Item item)
    {
        throw new System.NotImplementedException();
    }
}