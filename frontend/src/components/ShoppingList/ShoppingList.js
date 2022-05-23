import "./ShoppingList.css";
import PropTypes from "prop-types";
import ListItem from "./ShoppingListItem/ListItem";
import Card from "../UI/Atoms/Card/Card";
import Button from "../UI/Atoms/Button/Button";
import { TailSpin } from "react-loader-spinner";

/**
 * @classdesc
 * This is the component where the user can see the list of items they have added and it also includes a button for searching, using the items on the list. Every item is rendered as a ListItem component.
 *
 *
 * @category Home
 * @subcategory ShoppingList
 * @component
 * @hideconstructor
 *
 */
const ShoppingList = (props) => {
	/**
	 * This is just passing the removeItem event from ShoppingListItem through ShoppingList to Home
	 * 
	 * @param {number} id Id for the list item
	 * @param {string} name Name of the item
	 * @returns {void} Nothing
	 */
	const removeItemHandler = (id, name) => {
		props.onRemoveItem(id, name);
	};

		/**
	 * This is just passing the amountChanged event from ShoppingListItem through ShoppingList to Home
	 * 
	 * @param {number} id Id for the list item
	 * @returns {void} Nothing
	 */
	const decreaseAmountHandler = (id) => {
		const change = -1;
		props.onAmountChanged(id, change);
	};

			/**
	 * This is passing the amountChanged event from ShoppingListItem through ShoppingList to Home
	 * 
	 * @param {number} id Id for the list item
	 * @returns {void} Nothing
	 */
	const increaseAmountHandler = (id) => {
		const change = 1;
		props.onAmountChanged(id, change);
	};
	/**
	 * This is passing the the information about an updated list item to Home
	 * 
	 * @param {number} id Id for the list item
	 * @param {number} newAmount The new amount for the list item
	 * @param {number} unit The unit for the list item
	 * @returns {void} Nothing
	 */
	const updatedItem = (id, newAmount, unit) => {
		props.onNewUnitOrAmount(id, newAmount, unit);
	};

	/**
	 * This renders the list of items in the shopping list from the items prop.
	 */ 
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
			<div className="loader-spinner">
				{!props.post && (
					<TailSpin
						color="blue"
						height="2rem"
						width="2rem"
						ariaLabel="loading-indicator"
					/>
				)}
			</div>
		</Card>
	);
};

ShoppingList.propTypes = {
	/**
	 * The items on the shopping list is passed down from the parent component
	 */
	items: PropTypes.array.isRequired,
	/**
	 * The function that is called when the user wants to remove an item
	 */
	onRemoveItem: PropTypes.func,
	/**
	 * The function that is called when the user wants to change the amount of an item
	 */
	onAmountChanged: PropTypes.func,
	/**
	 * The function that is called when the user has changed the unit or amount of an item
	 */
	onNewUnitOrAmount: PropTypes.func,
	/**
	 * The function that is called when the user wants to search for the items on the list
	 */
	onSearch: PropTypes.func.isRequired,
	/**
	 * This prop controls when the spinner is shown
	 */

	post: PropTypes.bool,
};

export default ShoppingList;
