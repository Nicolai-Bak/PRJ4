import Card from "../../UI/Atoms/Card/Card";
import "./SearchResultItem.css";

const SearchResultItem = (props) => {
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

export default SearchResultItem;
