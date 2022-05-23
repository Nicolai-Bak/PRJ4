import "./Home.css";
import ShoppingList from "../components/ShoppingList/ShoppingList";
import PropTypes from "prop-types";
import NewItemForm from "../components/NewItem/NewItemForm";
import { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import Banner from "../components/Banner/Banner";
import StandardDialog from "../components/UI/Molecules/StandardDialog";
/**
 * @classdesc
 * This is the main component on the home page and it is comprised of the ShoppingList component and the NewItemForm component. This is where the user can add items to the list, see the list of items, and search for shopping options for the list items.
 *
 *
 * @category Home
 * @component
 * @hideconstructor
 *
 */
function Home(props) {
	const initialShoppingList = localStorage.hasOwnProperty("shoppingList")
		? JSON.parse(localStorage.getItem("shoppingList"))
		: [];

	let navigate = useNavigate();
	/**
	 * This state controls whether a dialog window that tells the user that an item already exists on the shopping list is open or not.
	 * @const duplicateItem
	 * @type {useState}
	 * @memberof Home
	 */
	const [duplicateItemOpen, setDuplicateItemOpen] = useState(false);

	/**
	 * This state controls whether a dialog window that tells the user that they can't search using an empty shopping list.
	 * @const emptyListOpen
	 * @type {useState}
	 * @memberof Home
	 */
	const [emptyListOpen, setEmptyListOpen] = useState(false);

	/**
	 * This state controls whether a dialog window that tells the user that the item they're trying to add doesn't exist.
	 * @const noItemFoundOpen
	 * @type {useState}
	 * @memberof Home
	 */
	const [noItemFoundOpen, setNoItemFoundOpen] = useState(false);

	/**
	 * This holds the original amount of an item in order for a user to add additional amount of an item in the duplicate dialog wiindow.
	 * @const amountToChange
	 * @type {useState}
	 * @memberof Home
	 */
	const [amountToChange, setAmountToChange] = useState(null);

	/**
	 * Holds the current information for an existing item that might be changed in a duplicate dialog window.
	 * @const existingItem
	 * @type {useState}
	 * @memberof Home
	 */
	const [existingItem, setExistingItem] = useState({
		name: "",
		amount: 0,
		unit: "",
		id: "",
		organic: false,
	});

	/**
	 * Holds all items that are currently on the shopping list.
	 * @const shoppingList
	 * @type {useState}
	 * @memberof Home
	 */
	const [shoppingList, setShoppingList] = useState(initialShoppingList);

	/**
	 * Buttons that are to be sent as props to the duplicate dialog window
	 */
	const duplicateDialogButtons = [
		{ text: "Tilføj Mængde", onClick: "addAmount" },
		{ text: "Afbryd", onClick: "onCancel" },
	];
	/**
	 * Text that is to be sent as props to the duplicate dialog window
	 */
	const duplicateDialogText = `Du har tilsyneladende allerede ${existingItem.amount}
	${existingItem.unit} ${existingItem.name} på din indkøbsliste. Vil du tilføje
	yderligere ${amountToChange}?`;
	/**
	 * Buttons that are to be sent as props to the empty list dialog window
	 */
	const emptyListDialogButtons = [{ text: "OK", onClick: "onCancel" }];
	/**
	 * Text that is to be sent as props to the empty list dialog window
	 */
	const emptyListDialogText = `Der er ingen varer på din indkøbsliste.`;
	/**
	 * Buttons that are to be sent as props to the no item found dialog window
	 */
	const noItemFoundDialogButtons = [{ text: "OK", onClick: "onCancel" }];
	/**
	 * Text that is to be sent as props to the no item found dialog window
	 */
	const noItemFoundDialogText = `Varen kan ikke findes i vores database`;

	/**
	 * Updates localStorage whenever the state of the shoppingList changes for the shopping list to persist between sessions.
	 * @function useEffect
	 * @memberof NewItemForm
	 */
	useEffect(() => {
		console.log("updating localStorage...");
		localStorage.setItem("shoppingList", JSON.stringify(shoppingList));
	}, [shoppingList]);

	/**
	 * Handles updating the shoppingList state after a user changes a list item through the list item input field.
	 *
	 * @param {string} id Id of the item that is to be updated
	 * @param {string} amount Amount of the item that is to be updated
	 * @param {string} unit Unit of the item that is to be updated
	 * @param {boolean} adding Whether or not a new unit has been added
	 */
	const handleItemUpdate = (id, amount, unit, adding) => {
		setShoppingList((prevShoppingList) => {
			return prevShoppingList.map((item) => {
				if (item.id !== id || amount < 0) return item;
				if (unit && !adding) {
					return {
						...item,
						unit: unit,
						amount: amount,
					};
				} else if (!adding) {
					return {
						...item,
						amount: amount,
					};
				} else {
					return {
						...item,
						amount: amount,
					};
				}
			});
		});
	};

	/**
	 * Handles when a user wants to add an additional amount of an item through the duplicate dialog window.
	 *
	 * @param {string} itemName Name of the item that is to be added to
	 * @param {string} amount The that is to be added to an existing item
	 * @returns {void} Nothing
	 */
	const handleDialogAdd = (itemName, amount) => {
		//find shoppinglist item with itemName
		const item = shoppingList.find((item) => item.name === itemName);
		if (item) {
			handleItemUpdate(item.id, +item.amount + +amount, item.unit, true);
		}
		handleClose();
	};

	/**
	 * Handles closing of the various dialog windows.
	 *
	 * @returns {void} Nothing
	 */
	const handleClose = () => {
		setDuplicateItemOpen(false);
		setEmptyListOpen(false);
		setNoItemFoundOpen(false);
	};

	/**
	 * Updates the state of shoppingList whenever a new item is added.
	 *
	 * @param {string} name Name of the item that is added
	 * @param {string} amount Amount of the item that is added
	 * @param {string} unit Unit of the item that is added
	 * @param {string} id Id of the item that is added
	 * @param {boolean} organic Whether or not the item is organic
	 * @returns {void} An updated shopping list
	 */

	const newItemHandler = async (name, amount, unit, id, organic) => {
		// If an item with the same name exists on the shopping list already

		const existingItem = shoppingList.find(
			(item) => item.name.toLowerCase() === name.toLowerCase()
		);
		if (existingItem) {
			// console.log("Item already exists on the shopping list : ", existingItem);
			setExistingItem(existingItem);
			setAmountToChange(amount);
			setDuplicateItemOpen(true);
			return;
		}

		// if the item doesn't exists in the database
		if (!(await ValidateItem(name, unit))) {
			console.log("item not found in database");
			setNoItemFoundOpen(true);
			return;
		}

		setShoppingList((prevShoppingList) => {
			return [
				...prevShoppingList,
				{
					name: name,
					amount: amount,
					unit: unit,
					id: id, //uuid()
					key: id,
					organic: organic,
				},
			];
		});
	};

	/**
	 * This updates the state of shoppingList after the user has chosen to remove an item from the list.
	 *
	 * @param {string} id Id of the item that is to be removed
	 * @param {string} name Name of the item that is to be removed
	 * @returns {void}
	 */
	const removeItemHandler = (id, name) => {
		setShoppingList((prevShoppingList) => {
			return prevShoppingList.filter((item) => item.id !== id);
		});
	};

	/**
	 * Handles how much of the amount is changed when clicking on the +/- buttons. It depends whether on how many decimals the amount is.
	 *
	 * @param {string} amount The amount that is to be changed
	 * @param {string} change The amount that is to be added or subtracted
	 * @returns {number} The amount after change and formatted correctly.
	 */
	const decimalController = (amount, change) => {
		amount = +amount;

		if (amount % 1 === 0) {
			return amount + change;
		}
		if (amount < 1) {
			return Number(+amount.toFixed(2) + change * 0.01).toFixed(2);
		}
		if (amount == amount.toFixed(1)) {
			console.log("1 decimal");
			return Number(+amount.toFixed(1) + +change / 10).toFixed(1);
		}
		return Number(amount + change / 100).toFixed(2);
	};
	/**
	 * Helper function that changes how the amount is displayed in the shopping list depending on the amount and unit chosen, so less than 1 litre is displayed in mililiters and less than 1 kg is displayed in grams
	 *
	 * @param {string} id Id of the item that is to be changed
	 * @param {number} change The amount that is to be added or subtracted
	 */
	const changeAmountHandler = (id, change) => {
		setShoppingList((prevShoppingList) => {
			return prevShoppingList.map((item) => {
				if (item.id !== id || item.amount + change < 0) return item; //change can be positive or negative
				let oldAmount = +item.amount;

				if (item.unit !== "stk" && Math.round(item.amount) !== item.amount) {
					return {
						...item,
						amount: decimalController(oldAmount, +change),
					};
				} else {
					return {
						...item,
						amount: decimalController(oldAmount, +change),
					};
				}
			});
		});
	};

	//GEOLOCATION
	/**
	 * Saves the users latitudinal position.
	 * @const latitude
	 * @type {useState}
	 * @memberof Home
	 */
	const [latitude, setLatitude] = useState(null);
	/**
	 * Saves the users longitudinal position.
	 * @const longitude
	 * @type {useState}
	 * @memberof Home
	 */
	const [longitude, setLongitude] = useState(null);
	/**
	 * This updates the longitude and latitude states whenever user position changes
	 * @function useEffect
	 * @memberof NewItemForm
	 */
	useEffect(() => {
		if (!navigator.geolocation) {
			console.log("Fejl i geolokation");
		} else {
			navigator.geolocation.getCurrentPosition((position) => {
				setLatitude(position.coords.latitude);
				setLongitude(position.coords.longitude);
				// console.log(`latitude: ${latitude}, longitude: ${longitude}`);
			});
		}
	}, [longitude, latitude]);

	/**
	 * Handler function that is to be passed to App
	 * @returns {void} Nothing
	 */
	function geolocationHandler() {
		props.onSendLocation(latitude, longitude);
	}

	const [post, setPost] = useState(true);

	/**
	 * This function handles everything related to search. It formats every list item to the format expected by the database and sends them also including location and desired range. When and if the database responds with a list of items, it navigates to the search results page.
	 *
	 * @returns {void} Nothing
	 */
	const searchHandler = async () => {
		console.log(
			`searchHandler called with list: ${JSON.stringify(shoppingList)}`
		);
		if (shoppingList.length < 1) {
			setEmptyListOpen(true);
			return;
		}
		// const searchList = shoppingList.map((item) => item);
		// console.log("searchlist: " + JSON.stringify(searchList));
		const searchList = [];

		// Sending the item information that is relevant to the database with the naming scheme that they are working with
		shoppingList.forEach((item) => {
			console.log(item);
			const itemDTO = {
				Name: item.name,
				Unit: item.amount,
				Measurement: item.unit,
				Organic: item.organic,
			};
			// console.log(itemDTO);
			searchList.push(itemDTO);
		});
		//send coordinates to- 'searchresultspage in order to get google maps directions
		geolocationHandler();

		// searchList.forEach((item) => console.log(item));
		let request = false;
		try {
			setPost(request);
			request = await fetch(
				"https://prisninjawebapi.azurewebsites.net/options/ ",
				{
					method: "POST",
					headers: {
						Accept: "application/json",
						"Content-Type": "application/json",
					},
					body: JSON.stringify({
						products: searchList,
						Range: 50,
						y: latitude,
						x: longitude,
					}),
				}
			);
		} catch (error) {
			console.log(error);
			return;
		}

		console.log("request received: " + request);

		const data = await request.json();

		console.log("Data received from database: " + JSON.stringify(data));
		localStorage.setItem("SearchResults", JSON.stringify(data));

		navigate("/SearchResults");
	};

	/**
	 *
	 * @param {event} onClick Handles when the user clicks on "Tilføj Mængde" button in the duplicate dialog window
	 * @returns {void} Nothing
	 */
	const onAddDialog = (event) => {
		setDuplicateItemOpen(false);
		handleDialogAdd(existingItem.name, amountToChange);
	};

	return (
		<div className="home">
			<Banner />
			<div className="home-shopping-list-wrapper-upper">
				<div className="instructions-step-one">
					<div className="instructions-step-one-text">
						Trin 1: Søg efter din varer her!
					</div>
					<br />
					<img
						src="/images/arrow.svg"
						alt="arrow"
						className="instructions-arrow-one"
					/>
				</div>
				<NewItemForm
					className="home-new-item-form"
					onItemAdded={newItemHandler}
				/>
				<StandardDialog
					title="Duplikeret vare"
					bodyText={duplicateDialogText}
					buttons={duplicateDialogButtons}
					onCancel={handleClose}
					addAmount={onAddDialog}
					open={duplicateItemOpen}
				/>
				<StandardDialog
					title="Tom indkøbsliste"
					bodyText={emptyListDialogText}
					buttons={emptyListDialogButtons}
					onCancel={handleClose}
					addAmount={onAddDialog}
					open={emptyListOpen}
				/>
				<StandardDialog
					title="Varen kan ikke findes"
					bodyText={noItemFoundDialogText}
					buttons={noItemFoundDialogButtons}
					onCancel={handleClose}
					addAmount={onAddDialog}
					open={noItemFoundOpen}
				/>
				<div className="filler" />
			</div>
			<div className="home-shopping-list-wrapper-lower">
				<div className="instructions-step-three">
					<div className="instructions-step-three-wrapper">
						<div className="instructions-step-three-text">
							Trin 3: Tryk 'søg' og <br />
							se hvor du skal handle!
						</div>
						<br />
						<img
							src="/images/arrow.svg"
							alt="arrow"
							className="instructions-arrow-three"
						/>
					</div>
				</div>

				<ShoppingList
					className="home-shopping-list"
					items={shoppingList}
					onSearch={searchHandler}
					onRemoveItem={removeItemHandler}
					onAmountChanged={changeAmountHandler}
					onNewUnitOrAmount={handleItemUpdate}
					post={post}
				/>
				<div className="instructions-step-two">
					<div className="instructions-step-two-text">
						Trin 2: Dine varer tilføjes <br />
						herefter til din indkøbsliste
					</div>
					<br />
					<img
						src="/images/arrow.svg"
						alt="arrow"
						className="instructions-arrow-two"
					/>
				</div>
			</div>
		</div>
	);

	/**
	 * Helper function that validates the item being added by checking if the array of item names contains the name of item being added.
	 *
	 * @param {string} name of the item
	 * @param {string} unit of the item
	 * @returns {boolean} true if the name of the item can be found
	 */
	async function ValidateItem(name, unit) {
		const itemNames = JSON.parse(localStorage.getItem("itemNames"));

		let matchFound = false;
		let foundItems = [];
		if (itemNames.length < 1) {
			console.log("No items in localStorage");
			return;
		}

		itemNames.forEach((item) => {
			if (item.toLowerCase().includes(name.toLowerCase())) {
				// this needs to be uncommented when a unit is received

				// if (item.unit !== unit) {
				// 	console.log(`unit not found, received: ${item.unit}`);
				// 	return;
				// }
				foundItems.push(item);
				console.log("unit found");
			}
		});
		if (foundItems.length > 0) {
			matchFound = true;
		}
		return matchFound;
	}
}
Home.propTypes = {
	/**
	 * An event that fires, telling the parent component, what the current location is
	 */
	onSendLocation: PropTypes.func,
};
export default Home;
