using System.Collections.Generic;
using GildedRoseKata;
using NUnit.Framework;

namespace GildedRoseTests;

public class GildedRoseTest
{
    [Test]
    public void Foo()
    {
        var items = new List<Item> { new Item { Name = "foo", SellIn = 0, Quality = 0 } };
        var app = new GildedRose(items);
        app.UpdateQuality();
        Assert.AreEqual("fixme", items[0].Name);
    }
    
    //Existing behvaiour to pin based on GildedRoseRequirements.md
    //TODO: Test that Quality and sellIn are reduced on each item after one day
    //TODO: Test that Quality degrades twice as fast once the sell by date has passed
    //TODO: Test that Quality of an item is never negative
    //TODO: Test that Aged Brie increases in Quality the older it gets
    //TODO: Test that Quality of an item is never more than 50 (except Sulfuras, which is a constant 80
    //TODO: Test that Sulfuras (a legendary item) never has to be sold or decreases in Quality
    //TODO: Test that Backstage passes increases in Quality as its SellIn value approaches
    //TODO: Test that Backstage passes increases in Quality by 2 when there are 10 SellIn days or less
    //TODO: Test that Backstage passes increases in Quality by 3 when there are 5 SellIn days or less
    //TODO: Test that Backstage passes drops to 0 in Quality after the concert
    
    //New behaviour to add
    //TODO: Test that Conjured items degrade in Quality twice as fast as normal items
    
}