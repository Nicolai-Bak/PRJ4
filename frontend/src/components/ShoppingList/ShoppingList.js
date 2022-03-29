import "./ShoppingList.css";
import SearchButton from "./SearchButton";
import ListItem from "./ShoppingListItem/ListItem";

const ShoppingList = (props) => {
	const removeItemHandler = (id) => {
		console.log(`removeItemHandler called with id: ${id}`);
		props.onRemoveItem(id);
	};
	console.log("ShoppingList is called with these props: " + props.items);

	const decreaseAmountHandler = (id) => {
		console.log(`decreaseAmountHandler called with id: ${id}`);
		const change = -1;
		props.onAmountChanged(id, change);
	};
	const increaseAmountHandler = (amount) => {
		console.log(`increaseAmountHandler called with id: ${amount}`);
		const change = 1;
		props.onAmountChanged(amount, change);
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
