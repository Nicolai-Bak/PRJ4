import Card from "../UI/Atoms/Card/Card";
import "./SearchResultsItem.css";
import { IoChevronDownSharp, IoLocation } from "react-icons/io5";
import Button from "../UI/Atoms/Button/Button";
import { useState } from "react";
import SearchResultsItemProduct from "../SearchResultsItemProduct/SearchResultsItemProduct";
import { display } from "@mui/system";

const SearchResultsItem = (props) => {
	const [activeState, setActiveState] = useState(true);

	const toggleDetails = () => {
		setActiveState(!activeState);
	};

	let addressString = props.address
	addressString = addressString.replace(/^, DK+|, DK+$/g, '');

	let addressStringforMaps = addressString.replace(/ /g,"+");;
	console.log("eller måske her?:", addressStringforMaps);

	let displayStoreName = null;

	switch(props.storeName) {
		case "foetex":
			displayStoreName = 
				<div className="list-item-store-name">føtex</div>
			break;
		case "Fakta":
		case "Dagli'Brugsen":
		case "SuperBrugsen":
		case "Kvickly":
			displayStoreName = 
				<div className="list-item-store-name store-name-red">{props.storeName}</div>
			break;
		case "Coop365":
			displayStoreName = 
				<div className="list-item-store-name store-name-green">{props.storeName}</div>
			break;
		default:
			displayStoreName = 
				<div className="list-item-store-name">{props.storeName}</div>
			break;
	}


	let GoogleMapsDirections = `https://www.google.com/maps/dir/${props.latitude},${props.longitude}/${addressStringforMaps}`;

	//https://www.google.com/maps/dir/56.1696085,10.1869934/Det+Glade+Vanvid,+Pakkerivej+2,+8000+Aarhus+C/

	return (
		<div className="search-results-item">
			<Card className="search-results-list-item">
				<div className="list-item-wrapper">
					<div className="list-item-price">Pris: <span className="total-price">{props.price/100}kr</span></div>
					{displayStoreName}
					<div className="list-item-address" title="vis rutevejledning"><a href={GoogleMapsDirections} target="_blank">
						<IoLocation/>{addressString} ({props.distance.toFixed(2)}km)</a></div>
					<Button className="chevron-button" onClick={toggleDetails}>
						<IoChevronDownSharp
							className={`chevron-icon ${!activeState ? "rotate" : ""}`}
						/>
					</Button>
				</div>
				<div className={`search-results-details ${!activeState ? "show" : ""}`}>
					{/* {products.map(product => (
                    <SearchResultsItemProduct results={product.name}/>
            ))} */}
					<SearchResultsItemProduct  products={props.products} />
				</div>
			</Card>
		</div>
	);
};

export default SearchResultsItem;
