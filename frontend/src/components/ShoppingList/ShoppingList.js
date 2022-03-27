import "./ShoppingList.css";
import SearchButton from "./SearchButton";
import ListItem from "./ShoppingListItem/ListItem";

const ShoppingList = (props) => {
	const removeItemHandler = (id) => {
		console.log(`removeItemHandler called with id: ${id}`);
		props.onRemoveItem(id);
	};

	const decreaseAmountHandler = (id) => {
		console.log(`decreaseAmountHandler called with id: ${id}`);
		props.onAmountChanged(id, -1);
	};
	const increaseAmountHandler = (id) => {
		console.log(`decreaseAmountHandler called with id: ${id}`);
		props.onAmountChanged(id, 1);
	};

	const itemsList = props.items.map((item) => (
		<ListItem
			onRemoveItem={removeItemHandler}
			onDecreaseAmount={decreaseAmountHandler}
			onIncreaseAmount={increaseAmountHandler}
			id={item.id}
			name={item.name}
			amount={item.amount}
			unit={item.unit}
		/>
	));

	return (
		<div className="shopping-list">
			<ul>{itemsList}</ul>
			<SearchButton />
		</div>
	);
};

export default ShoppingList;
