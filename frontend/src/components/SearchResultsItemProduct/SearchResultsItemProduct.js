import Card from "../UI/Atoms/Card/Card";
import "./SearchResultsItemProduct.css";

const SearchResultsItemProduct = (props) => {

	function displayImage() {
		
	}

	const displayProducts = () => {
		const newProducts = props.products.map((product) => {
			return (
				<li className="product-information">
					<p className="product-name">{product.name}, {product.brand}
						{product.organic &&
						<img className="organic-logo" src="/images/organic-logo.svg" alt={product.name} />}
						<span className="product-price"> {product.price}kr</span>
						<img className="product-image" src={product.imageUrl} alt={product.name} onClick={displayImage()}/></p> 
					<p className="product-unit">{product.amount} stk af {product.units}{product.measurement}
					</p> 
					
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
