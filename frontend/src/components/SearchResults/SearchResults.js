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
	console.log(options);

	// iterate over options object and save all non null attributes to new object
	const newOptions = {};
	for (const key in options) {
		if (options[key] !== null) {
			newOptions[key] = options[key];
		}
	}

	console.log(newOptions);
		const cheapest = {
			StoreName: options.cheapest.storeName,
			TotalPrice: options.cheapest.totalPrice,
			TotalDistance: options.cheapest.totalDistance,
			Products: options.cheapest.products,
		};

	if (Object.keys(options.best).length !== 0) {
		const best = {
			StoreName: options.Best.StoreName,
			TotalPrice: options.Best.TotalPrice,
			TotalDistance: options.Best.TotalDistance,
			Products: options.Best.Products,
		};
	}

	if (options.nearest != null) {
		const Nearest = {
			StoreName: options.Nearest.StoreName,
			TotalPrice: options.Nearest.TotalPrice,
			TotalDistance: options.Nearest.TotalDistance,
			Products: options.Nearest.Products,
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
