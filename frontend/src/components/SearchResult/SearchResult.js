import Card from "../UI/Atoms/Card/Card";
import "./SearchResult.css";
import { IoChevronDownSharp, IoLocation } from "react-icons/io5";
import Button from "../UI/Atoms/Button/Button";
import { useState } from "react";
import SearchResultItem from "./SearchResultItem/SearchResultItem";
import PropTypes from "prop-types";

/**
 * @classdesc
 * This is the component that displays the individual shopping options based on the shoppinglist items and user location.
 *
 * @category SearchResultsPage
 * @subcategory SearchResult
 * @component
 * @hideconstructor
 *
 */
const SearchResult = (props) => {
	/**
	 * The state of the dropdown button.
	 * @const activeState
	 * @type {useState}
	 * @memberof SearchResult
	 */
	const [activeState, setActiveState] = useState(true);

	/**
	 * When a user clicks the button, the state of the button is toggled.
	 *
	 * @returns {void}
	 */
	const toggleDetails = () => {
		setActiveState(!activeState);
	};

	/**
	 * variable containing the address of the store, to be filtered using regex.
	 * @type {let}
	 */	
	let addressString = props.address;

	addressString = addressString.replace(/^, DK+|, DK+$/g, "");

	/**
	 * variable that takes the address string and formats it to be used for the google maps link.
	 * @type {let}
	 */	
	let addressStringforMaps = addressString.replace(/ /g, "+");

	/**
	 * Variable that gets assigned the code to display the store's name.
	 * @type {let}
	 */
	let displayStoreName = null;

	/**
	 * switch statement that checks the store name and sets the displayStoreName variable to the correct store name.
	 */
	switch (props.storeName) {
		case "foetex":
			displayStoreName = <div className="list-item-store-name">føtex</div>;
			break;
		case "Fakta":
		case "Dagli'Brugsen":
		case "SuperBrugsen":
		case "Kvickly":
			displayStoreName = (
				<div className="list-item-store-name store-name-red">
					{props.storeName}
				</div>
			);
			break;
		case "Coop365":
			displayStoreName = (
				<div className="list-item-store-name store-name-green">
					{props.storeName}
				</div>
			);
			break;
		default:
			displayStoreName = (
				<div className="list-item-store-name">{props.storeName}</div>
			);
			break;
	}

	/**
	 * Variable that contains the google maps link.
	 * @type {let}
	 */
	let GoogleMapsDirections = `https://www.google.com/maps/dir/${props.latitude},${props.longitude}/${addressStringforMaps}`;

	return (
		<div className="search-result">
			<Card className="search-result-list-item">
				<div className="list-item-wrapper">
					<div className="list-item-price">
						Pris: <span className="total-price">{props.price / 100}kr</span>
					</div>
					{displayStoreName}
					<div className="list-item-address" title="vis rutevejledning">
						<a href={GoogleMapsDirections} target="_blank">
							<IoLocation />
							{addressString} ({props.distance.toFixed(2)}km)
						</a>
					</div>
					<Button className="chevron-button" onClick={toggleDetails}>
						<IoChevronDownSharp
							className={`chevron-icon ${!activeState ? "rotate" : ""}`}
						/>
					</Button>
				</div>
				<div className={`search-result-details ${!activeState ? "show" : ""}`}>
					<SearchResultItem products={props.products} />
				</div>
			</Card>
		</div>
	);
};

SearchResult.propTypes = {
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

export default SearchResult;
