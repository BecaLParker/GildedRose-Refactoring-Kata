using System.Collections.Generic;
using GildedRose;
using NUnit.Framework;

namespace GildedRoseTests;

public class GildedRoseTest
{
    //Existing behvaiour to pin based on GildedRoseRequirements.md
    [Test]
    public void QualityAndSellIn_ReduceOnNormalItems_AfterOneDay()
    {
        var items = new List<Item> { 
            new() { Name = "foo", SellIn = 1, Quality = 1 }, 
            new() { Name = "bar", SellIn = 2, Quality = 2 } 
        };
        var app = new GildedRose.GildedRose(items);
        app.UpdateQuality();
        Assert.AreEqual(0, items[0].SellIn);
        Assert.AreEqual(0, items[0].Quality);
        Assert.AreEqual(1, items[1].SellIn);
        Assert.AreEqual(1, items[1].Quality);
    }
    
    [Test]
    public void Quality_DegradesTwiceAsFast_AfterSellByDate()
    {
        var items = new List<Item> { 
            new() { Name = "foo", SellIn = 0, Quality = 2 }, 
            new() { Name = "bar", SellIn = 0, Quality = 4 } 
        };
        var app = new GildedRose.GildedRose(items);
        app.UpdateQuality();
        Assert.AreEqual(-1, items[0].SellIn);
        Assert.AreEqual(0, items[0].Quality);
        Assert.AreEqual(-1, items[1].SellIn);
        Assert.AreEqual(2, items[1].Quality);
    }
    
    [Test]
    public void Quality_IsNeverNegative()
    {
        var items = new List<Item> { 
            new() { Name = "foo", SellIn = 0, Quality = 0 }
        };
        var app = new GildedRose.GildedRose(items);
        app.UpdateQuality();
        Assert.AreEqual(-1, items[0].SellIn);
        Assert.AreEqual(0, items[0].Quality);
        app.UpdateQuality();
        Assert.AreEqual(-2, items[0].SellIn);
        Assert.AreEqual(0, items[0].Quality);
    }
    
    [Test]
    public void AgedBrie_IncreasesInQuality_TheOlderItGets()
    {
        var items = new List<Item> { 
            new() { Name = "Aged Brie", SellIn = 2, Quality = 0 }
        };
        var app = new GildedRose.GildedRose(items);
        app.UpdateQuality();
        Assert.AreEqual(1, items[0].SellIn);
        Assert.AreEqual(1, items[0].Quality);
        app.UpdateQuality();
        Assert.AreEqual(0, items[0].SellIn);
        Assert.AreEqual(2, items[0].Quality);
        app.UpdateQuality();
        Assert.AreEqual(-1, items[0].SellIn);
        Assert.AreEqual(4, items[0].Quality);
    }
    
    [TestCase("Aged Brie", 50)]
    [TestCase("Backstage passes to a TAFKAL80ETC concert", 50)]
    [TestCase("Sulfuras, Hand of Ragnaros", 80)]
    public void Quality_IsNeverExceedsRelevantMax(string name, int expectedMax)
    {
        var items = new List<Item> { 
            new() { Name = name, SellIn = 2, Quality = expectedMax},
        };
        var app = new GildedRose.GildedRose(items);
        app.UpdateQuality();
        Assert.AreEqual(expectedMax, items[0].Quality);
        app.UpdateQuality();
        Assert.AreEqual(expectedMax, items[0].Quality);
    }
    
    [Test]
    public void Sulfuras_NeverHasToBeSoldOrDecreasesInQuality()
    {
        var items = new List<Item> { 
            new() { Name = "Sulfuras, Hand of Ragnaros", SellIn = 2, Quality = 80},
        };
        //TODO: make SellIn a nullable int to represent that it never has to be sold
        
        var app = new GildedRose.GildedRose(items);
        app.UpdateQuality();
        Assert.AreEqual(2, items[0].SellIn);
        Assert.AreEqual(80, items[0].Quality);
        app.UpdateQuality();
        Assert.AreEqual(2, items[0].SellIn);
        Assert.AreEqual(80, items[0].Quality);
    }

    [Test]
    public void BackstagePasses_IncreasesInQuality_AsSellInValueApproaches()
    {
        var items = new List<Item> { 
            new() { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 15, Quality = 20},
        };
        var app = new GildedRose.GildedRose(items);
        app.UpdateQuality();
        Assert.AreEqual(14, items[0].SellIn);
        Assert.AreEqual(21, items[0].Quality);
        app.UpdateQuality();
        Assert.AreEqual(13, items[0].SellIn);
        Assert.AreEqual(22, items[0].Quality);
    }
   
    [Test]
    public void BackstagePasses_IncreasesInQualityBy2_When10SellInDaysOrLess()
    {
        var items = new List<Item> { 
            new() { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 10, Quality = 20},
        };
        var app = new GildedRose.GildedRose(items);
        app.UpdateQuality();
        Assert.AreEqual(9, items[0].SellIn);
        Assert.AreEqual(22, items[0].Quality);
        app.UpdateQuality();
        Assert.AreEqual(8, items[0].SellIn);
        Assert.AreEqual(24, items[0].Quality);
    }
    
    [Test]
    public void BackstagePasses_IncreasesInQualityBy3_When5SellInDaysOrLess()
    {
        var items = new List<Item> { 
            new() { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 5, Quality = 20},
        };
        var app = new GildedRose.GildedRose(items);
        app.UpdateQuality();
        Assert.AreEqual(4, items[0].SellIn);
        Assert.AreEqual(23, items[0].Quality);
        app.UpdateQuality();
        Assert.AreEqual(3, items[0].SellIn);
        Assert.AreEqual(26, items[0].Quality);
    }
    
    [Test]
    public void BackstagePasses_QualityDropsTo0_AfterConcert()
    {
        var items = new List<Item> { 
            new() { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 0, Quality = 20},
        };
        var app = new GildedRose.GildedRose(items);
        app.UpdateQuality();
        Assert.AreEqual(-1, items[0].SellIn);
        Assert.AreEqual(0, items[0].Quality);
        app.UpdateQuality();
        Assert.AreEqual(-2, items[0].SellIn);
        Assert.AreEqual(0, items[0].Quality);
    }
    
    //New behaviour to add
    //TODO: Test that Conjured items degrade in Quality twice as fast as normal items
    
}