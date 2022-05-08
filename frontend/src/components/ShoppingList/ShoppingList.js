import "./ShoppingList.css";
import ListItem from "./ShoppingListItem/ListItem";
import Card from "../UI/Atoms/Card/Card";
import Button from "../UI/Atoms/Button/Button";

const ShoppingList = (props) => {
	//Propdrilling: When the 'slet' is pressed
	const removeItemHandler = (id, name) => {
		props.onRemoveItem(id, name);
	};

	//Propdrilling: When the '-' is pressed
	const decreaseAmountHandler = (id) => {
		const change = -1;
		props.onAmountChanged(id, change);
	};

	//Propdrilling: When the '+' is pressed
	const increaseAmountHandler = (id) => {
		const change = 1;
		props.onAmountChanged(id, change);
	};

	//Propdrilling: When the user has changed the span element that shows unit and amount
	const updatedItem = (id, newAmount, unit) => {
		props.onNewUnitOrAmount(id, newAmount, unit);
	};

	const itemsList = props.items.map((item) => (
		<ListItem
			onRemoveItem={removeItemHandler}
			onDecreaseAmount={decreaseAmountHandler}
			onIncreaseAmount={increaseAmountHandler}
			newUnitOrAmount={updatedItem}
			id={item.id}
			name={item.name}
			amount={item.amount}
			unit={item.unit === "l" ? item.unit.toUpperCase() : item.unit}
			key={item.id}
			organic={item.organic}
		/>
	));

	return (
		<Card className="shopping-list">
			<ul>{itemsList}</ul>
			<Button onClick={() => props.onSearch()} className="search-button">
				Søg efter varer
			</Button>
		</Card>
	);
};

export default ShoppingList;
