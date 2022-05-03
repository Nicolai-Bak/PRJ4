import Card from "../UI/Atoms/Card/Card";
import "./SearchResultsItemProduct.css";

const SearchResultsItemProduct = (props) => {
	console.log("hej - ", props.products);

	const displayProducts = () => {
		console.log("props1337", props.products);
		const newProducts = props.products.map((product) => {
			return (
				<li>
					{product.name} - {product.price}kr
				</li>
			);
		});
		return newProducts;
	};

	return (
		<Card className="search-results-item-product">
			<div className="item-product">
				<ul className="product-display">{displayProducts()}</ul>
			</div>
		</Card>
	);
};

export default SearchResultsItemProduct;
