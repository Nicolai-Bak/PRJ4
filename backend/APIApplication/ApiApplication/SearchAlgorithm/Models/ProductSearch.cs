﻿using ApiApplication.Database.Models;

namespace ApiApplication.SearchAlgorithm.Models;

public class ProductSearch
{
    public string Name { get; set; }
    public string Brand { get; set; }
    public double Unit { get; set; }
    public string Measurement { get; set; }
    public double Price { get; set; }
    public int Amount { get; set; }

    public ProductSearch(Product p, double price, int amount)
    {
        Name = p.Name;
        Brand = p.Brand;
        Unit = p.Unit;
        Measurement = p.Measurement;
        Price = price;
        Amount = amount;
    }
}