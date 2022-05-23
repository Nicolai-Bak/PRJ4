import React from "react";
import "./NavBar.css";
import { IoMenu, IoClose } from "react-icons/io5";
import { useState } from "react";
import { Link, useNavigate } from "react-router-dom";
import PropTypes from "prop-types";

/**
 * @classdesc
 * This is the navbar of the website. It contains links to the different pages and the company name.
 *
 * @category UI
 * @subcategory Organisms
 * @component
 * @hideconstructor
 *
 */


function NavBar(props) {
	/**
	 * state management of the hamburger menu
	 * @const activeState
	 * @type {useState}
	 * @memberof NavBar
	 */
	const [showMenu, setShowMenu] = useState(false);
	/**
	 * method that toggles the hamburger menu
	 * @const activeState
	 */
	const toggleMenu = () => setShowMenu(!showMenu);
	/**
	 * state management of shoppingCart to display amount of items in cart
	 * @const activeState
	 * @type {useState}
	 @memberof NavBar
	 */
	const [shoppingCart, setShoppingCart] = useState(false);
	/**
	 * method that toggles the shoppingCart
	 * @const activeState
	 */
	const toggleShoppingCart = () => setShoppingCart(!shoppingCart);

	const { pageLinks, companyName } = props;

	/**
	 * useNavigate hook that is used to navigate to the home page.
	 * @type {useNavigate}
	 * @memberof SearchResultsPage
	 */
	let navigate = useNavigate();

	/**
	 * Function that returns the links to the different pages
	 * @param {const} 
	 * @returns {JSX}
	 */
	const linkRefs = () => {
		const links = pageLinks.map((link) => {
			return (
				<div className="link" key={(Math.random() * 20).toFixed(4)}>
					<Link to={link.link}>{link.text}</Link>
				</div>
			);
		});
		// console.log(pageLinks);
		return links;
	};

	/**
	 * Redirects the user to the home page.
	 * @returns {void}
	 */
	const returnToHomePage = () => {
		navigate("/");
	};

	return (
		<div className="navbar">
			<div className="left-side__container">
				<img
					id="ninja__logo"
					src="/images/ninja-desk.svg"
					onClick={returnToHomePage}
					alt={companyName}
				/>
				<span className="navbar-company-name" onClick={returnToHomePage}>
					{companyName}
				</span>
			</div>
			<div className="right-side__container">
				<div className="navbar-menu">
					<div className={`navbar-links ${!showMenu ? "hide" : ""}`}>
						{linkRefs()}
					</div>
				</div>
				<img
					id="shopping__cart"
					src="/images/shopping-cart.svg"
					alt="indkøbsvogn"
					onMouseEnter={toggleShoppingCart}
					onMouseLeave={toggleShoppingCart}
				/>
				<IoMenu
					onClick={toggleMenu}
					className={`hamburger-menu__icon ${showMenu ? "hide" : ""}`}
				/>
				<IoClose
					onClick={toggleMenu}
					className={`hamburger-menu__icon ${showMenu ? "" : "hide"}`}
				/>
			</div>
			{shoppingCart && (
				<div className="shopping-cart-toast">
					Der er tilføjet{" "}
					{JSON.parse(localStorage.getItem("shoppingList")).length} varer til
					indkøbslisten.
				</div>
			)}
		</div>
	);
}

export default NavBar;
