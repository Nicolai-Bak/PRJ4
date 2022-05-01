import React, { useState } from "react";
import "./ListItem.css";
import Button from "../../UI/Atoms/Button/Button";

const ListItem = (props) => {
	const [displayAmount, setDisplayAmount] = useState(true);

	const unitAmountHandler = () => {
		if (props.amount < 1 && props.unit === "kg") {
			return props.amount * 1000 + "g";
		} else if (props.amount < 1 && props.unit === "l") {
			return props.amount * 1000 + "ml";
		} else {
			return props.amount + props.unit;
		}
	};

	const changeAmountAndUnit = (event) => {
		if (event.type !== "keydown") {
			setDisplayAmount(!displayAmount);
			console.log(event.type);
		}
		console.log(event);

		if (event.key === "Enter") {
			console.log(event.target.value);
			props.newUnitOrAmount(props.id, event.target.value);
			setDisplayAmount(!displayAmount);
		}
	};

	const amountChanged = () => {
		// setDisplayAmount(true);
	};

	const amountAndUnitDisplay = () => {
		if (displayAmount) {
			return (
				<span className="unit-amount" onClick={changeAmountAndUnit}>
					{unitAmountHandler()}
				</span>
			);
		} else {
			return (
				<span className="unit-amount">
					<input
						autoFocus
						placeholder={props.amount + props.unit}
						className="unit-amount-input"
						type="text"
						// onInput={amountChanged}
						onKeyDown={changeAmountAndUnit}
					/>
				</span>
			);
		}
	};

	return (
		<div className="list-item">
			<span className="list-item__left">{props.name}</span>
			<span className="list-item__right">
				<span className="unit-amount">{amountAndUnitDisplay()}</span>
				<span className="item-buttons">
					<Button
						className="adjust-amount-button"
						onClick={() => props.onDecreaseAmount(props.id)}
					>
						-
					</Button>
					<Button
						className="adjust-amount-button"
						onClick={() => props.onIncreaseAmount(props.id)}
					>
						+
					</Button>
				</span>
				<Button
					className="remove-button"
					onClick={() => props.onRemoveItem(props.id, props.name)}
				>
					Slet
				</Button>
			</span>
		</div>
	);
};

export default ListItem;
