import "./SearchResults.css"
import ShoppingOption from "./ShoppingOption";
import React from "react";

const SearchResults = () => {

    const options = GetShoppingOptions();

    return (
        <div className="search-results" >
            <ShoppingOption option={options.Best} />
            <ShoppingOption option={options.Cheapest} />
            <ShoppingOption option={options.Nearest} />
        </div>
    )
}

const GetShoppingOptions = () => {
    const options= require('./ShoppingOptionsTest.json');
    return options;
}

export default SearchResults;