import Card from "../UI/Atoms/Card/Card";
import "./SearchResultsItem.css";
import { IoChevronDownSharp } from "react-icons/io5";
import Button from "../UI/Atoms/Button/Button";
import { useState } from "react";
import SearchResultsItemProduct from "../SearchResultsItemProduct/SearchResultsItemProduct";

const SearchResultsItem = (props) => {
	const [activeState, setActiveState] = useState(true);

	const toggleDetails = () => {
		setActiveState(!activeState);
	};

	let addressString = props.address
	addressString = addressString.replace(/^, DK+|, DK+$/g, '');

	let addressStringforMaps = addressString.replace(/ /g,"+");;
	console.log("eller måske her?:", addressStringforMaps);


	let GoogleMapsDirections = `https://www.google.com/maps/dir/${props.latitude},${props.longitude}/${addressStringforMaps}`;

	//https://www.google.com/maps/dir/56.1696085,10.1869934/Det+Glade+Vanvid,+Pakkerivej+2,+8000+Aarhus+C/

	return (
		<div className="search-results-item">
			<Card className="search-results-list-item">
				<div className="list-item-wrapper">
					<div className="list-item-price">Pris: <span className="total-price">{props.price/100}kr</span></div>
					<div className="list-item-store-name">{props.storeName} </div>
					<div className="list-item-address" title="vis rutevejledning"><a href={GoogleMapsDirections} target="_blank">{addressString} ({props.distance.toFixed(2)}km)</a></div>
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
