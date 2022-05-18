import Card from "../UI/Atoms/Card/Card";
import "./SearchResult.css";
import { IoChevronDownSharp, IoLocation } from "react-icons/io5";
import Button from "../UI/Atoms/Button/Button";
import { useState } from "react";
import SearchResultItem from "./SearchResultItem/SearchResultItem";

const SearchResult = (props) => {
	const [activeState, setActiveState] = useState(true);

	const toggleDetails = () => {
		setActiveState(!activeState);
	};

	let addressString = props.address;
	addressString = addressString.replace(/^, DK+|, DK+$/g, "");

	let addressStringforMaps = addressString.replace(/ /g, "+");

	let displayStoreName = null;

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

export default SearchResult;
