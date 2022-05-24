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

/**
 * @classdesc
 * This is a generic dialog component that can be used in different situations. It receives a title, bodyText, and an array of button objects.
 *
 *
 * @category UI
 * @subcategory Molecules
 * @component
 * @hideconstructor
 *
 */

const StandardDialog = (props) => {
	console.log(props);
	/**
	 * Method that returns an array of button objects with text and onClick function.
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
	 * An array of objects each consisting of the text for the button and name of the function to be called when the button is clicked. Both are strings but the onClick function is interpreted as JavaScript using eval.
	 */
	buttons: PropTypes.array,
	/**
	 * Prop that determines if the dialog is open or not.
	 */
	open: PropTypes.string,
	/**
	 * Function that is called when the user closes the dialog.
	 */
	onCancel: PropTypes.func,
	/**
	 * Sets the title of the dialog.
	 */
	title: PropTypes.string,
	/**
	 * Determines the main text of the dialog.
	 */
	bodyText: PropTypes.string,
};

export default StandardDialog;
