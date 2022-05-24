import "./Dropdown.css";
import Button from "../UI/Atoms/Button/Button";
import { useState } from "react";

const items = [
	{
		id: 0,
		content: "vores anbefaling",
	},
	{
		id: 1,
		content: "billigste",
	},
	{
		id: 2,
		content: "nærmeste",
	},
];

/**
 * @classdesc
 * This is a dropdown menu that displays the categories in which the shopping options are ordered by. It makes use of two useStates; one for the state of the dropdown menu and one for the state of the selected option.
 *
 * @category SearchResultsPage
 * @subcategory Dropdown
 * @component
 * @hideconstructor
 *
 */
const Dropdown = (props) => {
	/**
	 * The state that manages if the dropdown menu is open or not.
	 * @const isOpen
	 * @type {useState}
	 * @memberof Dropdown
	 */
	const [isOpen, setIsOpen] = useState(false);
	/**
	 * The state of the selected option. Sets the text of the dropdown button.
	 * @const title
	 * @type {useState}
	 * @memberof Dropdown
	 */
	const [title, setTitle] = useState(items[0].content);

	/**
	 * toggles the state of the dropdown menu. If the dropdown menu is open, it closes it. If it is closed, it opens it.
	 * @returns {void}
	 */
	const toggle = (event) => {
		setIsOpen(!isOpen);
	};
	/**
	 * Method that handles the selection of an option. Sets the state of the selected option and closes the dropdown menu by toggling the state of isOpen.
	 * @returns {void}
	 */
	const handleItemClick = (value) => {
		setIsOpen(!isOpen);
		// setTitle(items[id].content);
		setTitle(value);
		props.selectedOption(value);
	};

	return (
		<div>
			<Button className="dropdown-toggle" onClick={toggle}>
				søg efter: {title}
			</Button>
			{isOpen && (
				<ul className="dropdown-menu">
					{items.map((item) => (
						<li
							className="dropdown-menu-item"
							onClick={(e) => handleItemClick(e.target.getAttribute("value"))}
							value={item.content}
							key={item.id}
						>
							{item.content}
						</li>
					))}
				</ul>
			)}
		</div>
	);
};

export default Dropdown;
