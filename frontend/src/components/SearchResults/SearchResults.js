import "./SearchResults.css";
import ShoppingOption from "../ShoppingOption/ShoppingOption";
import React from "react";

const SearchResults = () => {
	const option1 = GetShoppingOptions();
	const options = tempOptions();

	return (
		<div className="search-results">
			<div className="search-results__container">
				<ShoppingOption option={options.Best} />
				<ShoppingOption option={option1} />
				<ShoppingOption option={options.Nearest} />
			</div>
		</div>
	);
};

const GetShoppingOptions = () => {
	const options = JSON.parse(localStorage.getItem("SearchResults"));
	// console.log(options);
	let cheapest = null;
	let best = null;
	let nearest = null;

	if (options.cheapest !== null) {
		cheapest = {
			StoreName: options.cheapest.storeName,
			TotalPrice: options.cheapest.totalPrice,
			TotalDistance: options.cheapest.totalDistance,
			Products: options.cheapest.products,
		};
	}

	if (options.best !== null) {
		best = {
			StoreName: options.best.StoreName,
			TotalPrice: options.best.TotalPrice,
			TotalDistance: options.best.TotalDistance,
			Products: options.best.Products,
		};
		return;
	}

	if (options.nearest !== null) {
		nearest = {
			StoreName: options.nNearest.StoreName,
			TotalPrice: options.nearest.TotalPrice,
			TotalDistance: options.nearest.TotalDistance,
			Products: options.nearest.Products,
		};
	}

	console.log("Cheapest: " + JSON.stringify(cheapest));
	return cheapest;
};

const tempOptions = () => {
	const options = require("../ShoppingOption/ShoppingOptionsTest.json");
	return options;
};

export default SearchResults;
