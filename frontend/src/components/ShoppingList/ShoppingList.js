import "./ShoppingList.css";
import SearchButton from "./SearchButton";
import ListItem from "./ShoppingListItem/ListItem";

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
			id={item.id}
			name={item.name}
			amount={item.amount}
			unit={item.unit}
			key={item.id}
		/>
	));

	return (
		<div className="shopping-list">
			<ul>{itemsList}</ul>
			<SearchButton onSearch={() => props.onSearch()} />
		</div>
	);
};

export default ShoppingList;
