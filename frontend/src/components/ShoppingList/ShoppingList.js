import "./ShoppingList.css";
import SearchButton from "./SearchButton";
import ListItem from "./ShoppingListItem/ListItem";
import Card from "../UI/Atoms/Card/Card";


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

	const itemsList = props.items.map((item) => (
		<ListItem
			onRemoveItem={removeItemHandler}
			onDecreaseAmount={decreaseAmountHandler}
			onIncreaseAmount={increaseAmountHandler}
			// onDecreaseAmount={() => props.onAmountChanged(item.id, -1)} // this is the same as the one above
			// onIncreaseAmount={() => props.onAmountHandler(item.id, 1)}
			id={item.id}
			name={item.name}
			amount={item.amount}
			unit={item.unit}
			key={item.id}
		/>
	));

	return (
		<Card className="shopping-list">
			<ul>{itemsList}</ul>
			<SearchButton onSearch={() => props.onSearch()} />
		</Card>
	);
};

export default ShoppingList;
