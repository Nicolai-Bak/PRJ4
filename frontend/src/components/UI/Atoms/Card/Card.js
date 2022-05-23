import React from "react";
import "./Card.css";
import PropTypes from "prop-types";


/**
 * @classdesc
 * This is the form where the user can add a new item to the list. This is comprised of two other components, specifically the UnitBox and SearchField.
 * The UnitBox is w	here the user can select the unit of the new item they're adding. The SearchField is where the user can search for an item to add to the list.
 *
 *
 * @category UI
 * @subcategory Card
 * @component
 * @hideconstructor
 *
 */

const Card = (props) => {
	/**
	 * Variable containing the className of the Card
	 * @const activeState
	 * @memberof Card
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
