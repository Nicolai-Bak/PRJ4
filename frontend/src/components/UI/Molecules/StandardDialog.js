import {
	Dialog,
	DialogActions,
	DialogContent,
	DialogContentText,
	DialogTitle,
} from "@mui/material";
import React, { useState } from "react";
import Button from "../Atoms/Button/Button";

const StandardDialog = (props) => {
	console.log(props);
	// an array of button objects with text and onClick function
	const buttonHandler = () => {
		const buttons = props.buttons.map((button) => {
			console.log(button);
			return (
				/*eslint-disable */
				<Button
					key={button.onClick}
					// eval returns js code from string - bad practice
					onClick={eval("props." + eval("button.onClick"))}
				>
					{button.text}
				</Button>
				/*eslint-enable */
			);
		});
		return buttons;
	};

	return (
		<Dialog open={props.open} onClose={props.onCancel}>
			<DialogTitle>
				{props.title ? props.title : "There's no title props set :)"}
			</DialogTitle>
			<DialogContent>
				<DialogContentText>
					{props.bodyText ? props.bodyText : "No bodyText prop has been set :)"}
				</DialogContentText>
				<DialogActions>{buttonHandler()}</DialogActions>
			</DialogContent>
		</Dialog>
	);
};

export default StandardDialog;
