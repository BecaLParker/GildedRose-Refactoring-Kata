namespace GildedRose;

public class Item
{
    public string Name { get; set; }
    public int SellIn { get; set; } //denotes the number of days we have to sell the item
    public int Quality { get; set; } //denotes how valuable the item is
}