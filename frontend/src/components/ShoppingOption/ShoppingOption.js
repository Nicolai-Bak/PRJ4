import "./ShoppingOption.css";

const ShoppingOption = (props) => {
	// console.log("ShoppingOptions");
	return (
		<div className="shopping-option">
			<div className="store">{props.option.StoreName}</div>
			<div className="price_distance">
				<div className="price">{props.option.TotalPrice}</div>
				<div className="distance">{props.option.TotalDistance}</div>
			</div>
			<div className="products">
				<h2>Varer</h2>
				{FormatProducts()}
			</div>
		</div>
	);

	function FormatProducts() {
		return (
			<div>
				{props.option.Products.map((product) => (
					<div className="product-line">
						<div className="product-name">
							{product.Name}
							<br />
							<div className="product-name2">{product.Name2}</div>
						</div>
						<div className="product-price">{product.Price} ,-</div>
					</div>
				))}
			</div>
		);
	}
};

export default ShoppingOption;
