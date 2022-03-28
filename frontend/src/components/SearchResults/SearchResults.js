import "./SearchResults.css"
import ShoppingOption from "../ShoppingOption/ShoppingOption"
import React from "react";

const SearchResults = () => {

    const options = GetShoppingOptions();

    return (
        <div className="search-results" >
            <div className="search-results__container">
            <ShoppingOption option={options.Best} />
            <ShoppingOption option={options.Cheapest} />
            <ShoppingOption option={options.Nearest} />
            </div>
        </div>
    )
}

const GetShoppingOptions = () => {
    const options= require('../ShoppingOption/ShoppingOptionsTest.json');
    return options;
}

export default SearchResults;