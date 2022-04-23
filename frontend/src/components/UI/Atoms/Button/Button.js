import React from 'react'
import "./Button.css"


const Button = (props) => {

	const button = `button-container ${props.className}`;

	return (
		<button onClick={props.onClick} className={button}>{props.children}</button>
		// can't get normal onClick event to work for some reason, so added a custom event :/
	)
}

export default Button