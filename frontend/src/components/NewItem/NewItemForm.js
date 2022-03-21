import "./NewItemForm.css";
import UnitBox from "./UnitBox"
import { useState } from "react";

const NewItemForm = (props) => {
	const [newItem, setNewItem] = useState("");
	const [amount, setAmount] = useState(1);
	const [unit, setUnit] = useState("kg");

	const submitItemHandler = (event) => {
		event.preventDefault();
		console.log(`You just tried to add ${amount} ${unit} ${newItem}'s`);

		if (validInput(newItem, amount)) {
			props.onItemAdded(newItem, amount, unit);
			return;
		}
		
		setNewItem("");
		setAmount(1);
		setUnit("kg");
	};

	const itemChangeHandler = (event) => {
		setNewItem(event.target.value);
	};

	const amountChangeHandler = (event) => {
		const added = event;
		console.log(added + "hello");
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

	const unitChangeHandler = (event) => {
		console.log(event);
		setUnit(event);
	}

	return (
		<form onSubmit={submitItemHandler} className="add-item-form">
			<div className="item-inputs">
				<input
					className="item-name"
					type="text"
					value={newItem}
					onChange={itemChangeHandler}
					placeholder="Tilføj varer her"
				></input>
			</div>
			<UnitBox onUnitSelected={unitChangeHandler}
				onAmountChanged={ amountChangeHandler}
			/>
			<button type="submit" className="add-item-button">
				Tilføj Vare
			</button>
			<br></br>
		</form>
	);
};

export default NewItemForm;
