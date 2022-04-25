using System.Collections.Generic;
using ApiApplication.SearchAlgorithm.Models;
using BusinessLogicLibrary.SearchAlgorithm;
using NUnit.Framework;

namespace BusinessLogicLibraryTests;

public class Tests
{
    private CheapestStoreSelecter _uut;
    [SetUp]
    public void Setup()
    {
        _uut = new CheapestStoreSelecter();
    }

    [Test]
    public void SelectStores_()
    {
        //Arrange
        List<StoreSearch> expected = new List<StoreSearch>
        {
            new StoreSearch {TotalPrice = 0},
            new StoreSearch {TotalPrice = 1},
            new StoreSearch {TotalPrice = 2},
            new StoreSearch {TotalPrice = 3},
            new StoreSearch {TotalPrice = 4},
        };

        List<StoreSearch> input = new List<StoreSearch>
        {
            new StoreSearch {TotalPrice = 4},
            new StoreSearch {TotalPrice = 2},
            new StoreSearch {TotalPrice = 0},
            new StoreSearch {TotalPrice = 3},
            new StoreSearch {TotalPrice = 1},
        };
        
        //Act
        List<StoreSearch> actual = _uut.SelectStores(input);
        
        //Assert
        for (int i = 0; i < actual.Count; i++)
        {
            Assert.That(actual[i], Is.EqualTo(expected[i]));
        }
        Assert.That(actual.Count, Is.EqualTo(expected.Count));
    }
}