import Card from "../../UI/Atoms/Card/Card";
import "./SearchResultItem.css";
import PropTypes from "prop-types";

/**
 * @classdesc
 * This is the component that displays the idividual shopping list items in the search result component when the user clicks on the dropdown icon.
 *
 * @category SearchResultsPage
 * @subcategory SearchResult
 * @component
 * @hideconstructor
 *
 */

const SearchResultItem = (props) => {

	/**
	 * The method that takes the individual shopping list items from the props.products array with the product information
	 * including product name, price, unit and so forth, and creates a JSX element for each item using javascript's map function.
	 * @returns {JSX}
	 */
	const displayProducts = () => {
		const newProducts = props.products.map((product) => {
			return (
				<li className="product-information">
					<p className="product-name">
						{product.name}, {product.brand}
						{product.organic && (
							<img
								className="organic-logo"
								src="/images/organic-logo.svg"
								alt={product.name}
							/>
						)}
						<span className="product-price"> {product.price / 100}kr</span>
						<img
							className="product-image"
							src={product.imageUrl}
							alt={product.name}
						/>
					</p>
					<p className="product-unit">
						{product.amount} stk af {product.units}
						{product.measurement}
					</p>
				</li>
			);
		});
		return newProducts;
	};

	return (
		<Card className="search-result-item">
			<div className="item-product">
				<ul className="product-display">{displayProducts()}</ul>
			</div>
		</Card>
	);
};

SearchResultItem.propTypes = {
	/** 
	 * The address of the store.
	*/
	products: PropTypes.array,

};

export default SearchResultItem;