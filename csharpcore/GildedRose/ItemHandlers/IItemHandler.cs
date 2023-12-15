namespace GildedRose.ItemHandlers
{
    public interface IItemHandler
    {
        IItemHandler NextHandler { get; }

        void HandleRequest(Item item)
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
    
        bool CanHandleItem(Item item);
    
        void UpdateQuality(Item item);
    
        void UpdateSellIn(Item item);
    
    }
}