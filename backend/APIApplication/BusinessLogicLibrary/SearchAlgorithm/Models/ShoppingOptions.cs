﻿using ApiApplication.SearchAlgorithm;
using ApiApplication.SearchAlgorithm.Models;
using BusinessLogicLibrary.SearchAlgorithm.Models.Interfaces;

namespace ApiApplication.Controllers;

public class ShoppingOptions : IShoppingOptions
{
    
    public List<StoreSearch> Cheapest { get; set; } 
    public List<StoreSearch> Nearest { get; set; }
    public List<StoreSearch> Best { get; set; }


    public ShoppingOptions(ISearchControl searchControl)
    {
        List<List<StoreSearch>> storeLists = searchControl.FindStores();
        Cheapest = storeLists[0];
        Nearest = storeLists[1];
        Best = storeLists[2];
    }
}