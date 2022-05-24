import React from "react";
import "./Button.css";
import PropTypes from "prop-types";

/**
 * @classdesc
 * A generic button component that is used to keep the UI consistent.
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
	 * @type {string}
	 * @memberof Button
	 */
	const button = `button-container ${props.className}`;

	return (
		<button onClick={props.onClick} className={button}>
			{props.children}
		</button>
		// can't get normal onClick event to work for some reason, so added a custom event :/
	);
};

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

export default Button;
