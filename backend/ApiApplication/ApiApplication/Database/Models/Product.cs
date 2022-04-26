﻿namespace ApiApplication.Database.Models;

public class Product
{
    public Int64 EAN { get; set; }
    public string Name { get; set; }
    public string Brand { get; set; }
    public double Unit { get; set; }
    public string Measurement { get; set; }
    public List<ProductStore> ProductStores { get; set; }
}
