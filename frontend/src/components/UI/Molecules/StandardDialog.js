import {
	Dialog,
	DialogActions,
	DialogContent,
	DialogContentText,
	DialogTitle,
} from "@mui/material";
import React, { useState } from "react";
import Button from "../Atoms/Button/Button";
import PropTypes from "prop-types";


const StandardDialog = (props) => {
	console.log(props);
	/**
	 * Method that returns an array of button objects with text and onClick function.
	 * @param {const} 
	 * @returns {JSX}
	 */
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

StandardDialog.propTypes = {
	/** 
	 * a variable used to write text to the button.
	*/
	buttons: PropTypes.string,
	/** 
	 * a variable used to write text to the button.
	*/
	open: PropTypes.string,
	/** 
	 * function that is called when the user closes the dialog.
	*/
	onCancel: PropTypes.func,
	/** 
	 * a variable used to set the title of the dialog.
	*/
	title: PropTypes.string,
	/** 
	 * a variable used to write text to the body of the dialog.
	*/
	bodyText: PropTypes.string,
};

export default StandardDialog;
