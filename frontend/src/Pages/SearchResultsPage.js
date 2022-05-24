import Card from "../components/UI/Atoms/Card/Card";
import "./SearchResultsPage.css";
import Button from "../components/UI/Atoms/Button/Button";
import SearchResult from "../components/SearchResult/SearchResult";
import { IoArrowBack } from "react-icons/io5";
import { useNavigate } from "react-router-dom";
import Dropdown from "../components/Dropdown/Dropdown";
import { useState } from "react";
import PropTypes from "prop-types";

/**
 * @classdesc
 * This is the search results page. It displays the search results based on the user's search query and location.
 *
 * @category SearchResultsPage
 * @subcategory SearchResultsPage
 * @component
 * @hideconstructor
 *
 */

function SearchResultsPage(props) {
	/**
	 * useNavigate hook that is used to navigate to the home page.
	 * @type {useNavigate}
	 * @memberof SearchResultsPage
	 */
	let navigate = useNavigate();
	/**
	 * The state of the dropdown. Used to keep track of the selected options so that the options are displayed according to the user's preference.
	 * @type {useState}
	 * @memberof SearchResultsPage
	 */
	const [selectedOptionState, setSelectedOptionState] = useState("best");
	/**
	 * Redirects the user to the home page.
	 * @returns {void}
	 */
	const goBack = () => {
		navigate("/");
	};
	/**
	 * Method that inserts the cheapest, best and nearest options into and array and returns it.
	 * @returns {array}
	 */
	const GetShoppingOptions = () => {
		const options = JSON.parse(localStorage.getItem("SearchResults"));
		let cheapest = null;
		let best = null;
		let nearest = null;

		if (options.cheapest !== null) {
			cheapest = Array.from(options.cheapest);
		}
		if (options.best !== null) {
			best = Array.from(options.best);
		}
		if (options.nearest !== null) {
			nearest = Array.from(options.nearest);
		}
		const allOptions = [cheapest, best, nearest];

		return allOptions;
	};
	const options = GetShoppingOptions();

	/**
	 * Method that sets the selected option state to the selected option.
	 * @param {string} value - The option that the user has selected in the dropdown.
	 * @returns {void}
	 */
	const selectedOptionHandler = (value) => {
		switch (value) {
			case "billigste":
				setSelectedOptionState("cheapest");
				break;
			case "vores anbefaling":
				setSelectedOptionState("best");
				break;
			case "nærmeste":
				setSelectedOptionState("nearest");
				break;

			default:
				console.log("selectedOptionHandler error");
		}
	};

	let displayOptions = null;

	/**
	 * Method that renders the search results based on the selectedOptionState.
	 * @param {string} selectedOptionState - The state of the dropdown used to determine which options to display.
	 * @returns {JSX}
	 */
	switch (selectedOptionState) {
		case "cheapest":
			displayOptions = options[0].map((item) => (
				<div>
					<SearchResult
						latitude={props.latitude}
						longitude={props.longitude}
						products={item.products}
						price={item.totalPrice}
						distance={item.distance}
						storeName={item.brand}
						address={item.address}
					></SearchResult>
				</div>
			));
			break;
		case "best":
			displayOptions = options[1].map((item) => (
				<SearchResult
					latitude={props.latitude}
					longitude={props.longitude}
					products={item.products}
					price={item.totalPrice}
					distance={item.distance}
					storeName={item.brand}
					address={item.address}
				></SearchResult>
			));
			break;
		case "nearest":
			displayOptions = options[2].map((item) => (
				<SearchResult
					latitude={props.latitude}
					longitude={props.longitude}
					products={item.products}
					price={item.totalPrice}
					distance={item.distance}
					storeName={item.brand}
					address={item.address}
				></SearchResult>
			));
			break;
		default:
			console.log("default");
	}

	/**
	 * useState that checks if the options array is empty and if so renders an error message instead of the search results.
	 * @const activeState
	 * @type {useState}
	 * @memberof SearchResultsPage
	 */
	const [showError, setShowError] = useState(
		options[0].length < 1 ? true : false
	);

	return (
		<div className="search-results-page">
			{!showError && (
				<Card className="results-container">
					<div className="dropdown-menu-container">
						<Dropdown
							className="results-page-dropdown"
							selectedOption={selectedOptionHandler}
						></Dropdown>
					</div>
					{displayOptions}
				</Card>
			)}
			{!showError && (
				<div className="search-results-page-text-box">
					Fandt du ikke det, du søgte? <a onClick={goBack}>Klik her</a>, for at
					gå tilbage og foretage en ny søgning.
				</div>
			)}
			{showError && (
				<div className="search-results-not-found">
					<div className="image-wrapper">
						<img
							src="https://svgshare.com/i/h6r.svg"
							alt="ninja holding an apple"
						></img>
					</div>
					<div className="text-wrapper">
						<h1>Hovsa!</h1>
						<h2>Noget gik galt ved din søgning.</h2>
						<h3>
							Dette kan skyldes, at du har søgt efter nogle varer, som ikke kan
							findes i samme butik. Vi arbejder på en løsning! I mellemtiden kan
							du <a onClick={goBack}>gå tilbage her</a> og prøve igen.
						</h3>
					</div>
				</div>
			)}
		</div>
	);
}

SearchResultsPage.propTypes = {
	/**
	 * The address of the store.
	 */
	address: PropTypes.string,
	/**
	 * The name of the store.
	 */
	storeName: PropTypes.string,
	/**
	 * The total price of all the products in the shopping list.
	 */
	price: PropTypes.number,
	/**
	 * The distance between the user and the store.
	 */
	distance: PropTypes.number,
	/**
	 * contains all the products in the shopping list, to be passed in to the searchResultItem component.
	 */
	products: PropTypes.array,
	/**
	 * latitude coordinate of the user.
	 */
	latitude: PropTypes.number,
	/**
	 * longitude coordinate of the user.
	 */
	longitude: PropTypes.number,
};

export default SearchResultsPage;
