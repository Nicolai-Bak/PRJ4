import "./NewItemForm.css";
import { useState } from "react";

const NewItemForm = () => {
	let input;
	const [addItem, setAddItem] = useState([]);

	const submitItemHandler = (event) => {
		setAddItem(input);
		event.preventDefault();
		console.log(`You just tried to add ${input}`);
		setAddItem("");
	};

	const inputChangeHandler = (event) => {
		setAddItem(event.target.value);
	};

	return (
		<form className="new-item-form" onSubmit={submitItemHandler}>
			<input
				required
				type="text"
				value={input}
				onChange={inputChangeHandler}
				placeholder="Tilføj vare her"
			></input>
			<button type="submit" className="form-button">
				Tilføj
			</button>
			<label>{addItem}</label>
		</form>
	);
};

export default NewItemForm;
