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
	return (
		<Dialog open={props.open} onClose={props.onCancel}>
			<DialogTitle>Duplikeret vare</DialogTitle>
			<DialogContent>
				<DialogContentText>
					Du har tilsyneladende allerede {props.existingAmount}
					{props.unit} {props.itemName} på din indkøbsliste. Vil du tilføje
					yderligere {props.amount}?
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
