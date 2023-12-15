namespace GildedRose.ItemHandlers;

public class SulfurasItemHandler(IItemHandler nextHandler) : IItemHandler
{
    public IItemHandler NextHandler { get; } = nextHandler;
    public bool CanHandleItem(Item item)
    {
        return item.Name == "Sulfuras, Hand of Ragnaros";
    }

    public void UpdateQuality(Item item)
    { }

    public void UpdateSellIn(Item item)
    { }
}