import {
	Dialog,
	DialogActions,
	DialogContent,
	DialogContentText,
	DialogTitle,
} from "@mui/material";
import React, { useState } from "react";
import Button from "../UI/Atoms/Button/Button";

const DuplicateDialog = (props) => {
	// const handleDialogAdd = (itemName, amount) => {
	// 	console.log("handleDialogAdd: ", itemName, amount);
	// 	//find shoppinglist item with itemName
	// 	const item = shoppingList.find((item) => item.name === itemName);
	// 	//if item exists, add amount to item.amount
	// 	if (item) {
	// 		console.log("item exists", item);
	// 		// handleItemUpdate(item.id, item.amount + +amount, item.unit, true);
	// 	}
	// 	handleClose();
	// };

	const handleClose = (action) => {};

	return (
		<Dialog open={props.open} onClose={props.onCancel}>
			<DialogTitle>Duplikeret vare</DialogTitle>
			<DialogContent>
				<DialogContentText>
					{props.itemName} er tilsyneladende allerede på indkøbslisten. Vil du tilføje
					{props.amount} til den eksisterende vare?
				</DialogContentText>
				<DialogActions>
					<Button onClick={props.addAmount}>Tilføj mængde</Button>
					<Button onClick={props.onCancel}>Afbryd</Button>
				</DialogActions>
			</DialogContent>
		</Dialog>
	);
};

export default DuplicateDialog;
