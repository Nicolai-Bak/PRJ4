import React from "react";
import "./Card.css";
import PropTypes from "prop-types";

/**
 * @classdesc
 * A generic wrapper component used for styling
 *
 *
 * @category UI
 * @subcategory Atoms
 * @component
 * @hideconstructor
 *
 */

const Card = (props) => {
	/**
	 * Variable containing the className of the Card
	 * @type {string}
	 */
	const classes = `card-container ${props.className}`;

	return <div className={classes}>{props.children}</div>;
};

Card.propTypes = {
	/**
	 * a variable used to write text to the button.
	 */
	children: PropTypes.string,
};

export default Card;
