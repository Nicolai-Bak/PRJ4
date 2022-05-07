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

	return (
		<div className="search-results-item">
			<Card className="search-results-list-item">
				<div className="list-item-wrapper">
					<div className="list-item-price">Pris: {props.price}kr</div>
					<div className="list-item-store-name">Butik: {props.storeName}</div>
					<div className="list-item-distance">Afstand: {props.distance}km</div>
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
