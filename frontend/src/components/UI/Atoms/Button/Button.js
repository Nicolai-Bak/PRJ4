import React from 'react'
import "./Button.css"
import PropTypes from "prop-types";


/**
 * @classdesc
 * This is the form where the user can add a new item to the list. This is comprised of two other components, specifically the UnitBox and SearchField.
 * The UnitBox is w	here the user can select the unit of the new item they're adding. The SearchField is where the user can search for an item to add to the list.
 *
 *
 * @category UI
 * @subcategory Atoms
 * @component
 * @hideconstructor
 *
 */

const Button = (props) => {
	/**
	 * Variable containing the className of the button.
	 * @const activeState
	 * @memberof Button
	 */
	const button = `button-container ${props.className}`;

	return (
		<button onClick={props.onClick} className={button}>{props.children}</button>
		// can't get normal onClick event to work for some reason, so added a custom event :/
	)
}

Button.propTypes = {
	/** 
	 * a function that is called when the button is clicked.
	*/
	onClick: PropTypes.func,
	/** 
	 * a variable used to write text to the button.
	*/
	children: PropTypes.string,
};

export default Button