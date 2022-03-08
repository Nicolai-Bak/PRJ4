import "./NewItemForm.css";
import { useState } from "react";

const NewItemForm = (props) => {
	const [newItem, setNewItem] = useState("");
	const [amount, setAmount] = useState(1);

	const submitItemHandler = (event) => {
		event.preventDefault();
		console.log(`You just tried to add ${amount} ${newItem}'s`);

		if (validInput(newItem, amount)) props.onItemAdded(newItem, amount);

		setNewItem("");
		setAmount(1);
	};

	const itemChangeHandler = (event) => {
		setNewItem(event.target.value);
	};

	const amountChangeHandler = (event) => {
		const added = event.target.value;
		added < 0
			? console.log(`Item amount too small, was: ${added}`)
			: setAmount(added);
	};

	const validInput = (newItem, amount) => {
		if (!newItem.length > 0) {
			console.log("No item was detected in the input field");
			return false;
		}
		if (!amount > 0) {
			console.log("amount too small");
			return false;
		}

		return true;
	};

	return (
		<form onSubmit={submitItemHandler} className="add-item-form">
			<div className="item-inputs">
				<input
					className="item-name"
					type="text"
					value={newItem}
					onChange={itemChangeHandler}
					placeholder="Tilføj vare her"
				></input>
				<input
					className="item-amount"
					required
					type="number"
					value={amount}
					onChange={amountChangeHandler}
				></input>
			</div>
			<button type="submit" className="add-item-button">
				Tilføj Vare
			</button>
			<br></br>
		</form>
	);
};

export default NewItemForm;
