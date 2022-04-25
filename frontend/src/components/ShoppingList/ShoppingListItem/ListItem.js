import React from "react";
import "./ListItem.css";
import Button from "../../UI/Atoms/Button/Button";

const ListItem = (props) => {
	const unitAmountHandler = () => {
		if (props.amount < 1 && props.unit === "kg") {
			return props.amount * 1000 + "g";
		} else if (props.amount < 1 && props.unit === "l") {
			return props.amount * 1000 + "ml";
		} else {
			return props.amount + props.unit;
		}
	};

	return (
		<div className="list-item">
			<span className="list-item__left">{props.name}</span>
			<span className="list-item__right">
				<span className="unit-amount">{unitAmountHandler()}</span>
				<span className="item-buttons">
					<Button className="adjust-amount-button" onClick={() => props.onDecreaseAmount(props.id)}>-</Button>
					<Button className="adjust-amount-button" onClick={() => props.onIncreaseAmount(props.id)}>+</Button>
				</span>
				<Button className="remove-button" onClick={() => props.onRemoveItem(props.id, props.name)}>Slet</Button>
			</span>
		</div>
	);
};

export default ListItem;
