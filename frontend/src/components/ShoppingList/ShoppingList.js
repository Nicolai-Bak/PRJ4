import "./ShoppingList.css";
import ListItem from "./ShoppingListItem/ListItem";
import Card from "../UI/Atoms/Card/Card";
import Button from "../UI/Atoms/Button/Button";

const ShoppingList = (props) => {
	const removeItemHandler = (id, name) => {
		// console.log(`removeItemHandler called with id: ${id}`);
		props.onRemoveItem(id, name);
	};

	const decreaseAmountHandler = (id) => {
		// console.log(`decreaseAmountHandler called with id: ${id}`);
		const change = -1;
		props.onAmountChanged(id, change);
	};

	const increaseAmountHandler = (id) => {
		// console.log(`increaseAmountHandler called with id: ${id}`);
		const change = 1;
		props.onAmountChanged(id, change);
	};

	const updatedItem = (id, newAmount) => {
		// console.log(`updatedItem called with id: ${id} and newAmount: ${newAmount}`);
		props.onNewUnitOrAmount(id, newAmount);
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
			{/* <SearchButton onSearch={() => props.onSearch()} /> */}
			<Button onClick={() => props.onSearch()} className="search-button">
				Søg efter varer
			</Button>
		</Card>
	);
};

export default ShoppingList;
