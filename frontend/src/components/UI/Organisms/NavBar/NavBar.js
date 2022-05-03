import React from "react";
import "./NavBar.css";
import { IoMenu, IoClose } from "react-icons/io5";
import { useState } from "react";
import { Link, useNavigate } from "react-router-dom";

function NavBar(props) {
	const [showMenu, setShowMenu] = useState(false);
	const toggleMenu = () => setShowMenu(!showMenu);
	const { pageLinks, companyName } = props;
	let navigate = useNavigate();

	const linkRefs = () => {
		const links = pageLinks.map((link) => {
			return (
				<span className="link" key={(Math.random() * 20).toFixed(4)}>
					<Link to={link.link}>{link.text}</Link>
				</span>
			);
		});
		// console.log(pageLinks);
		return links;
	};

	const iconClicked = () => {
		navigate("/");
	};

	return (
		<div className="navbar">
			<div className="left-side__container">
				<img
					id="ninja__logo"
					src="/images/ninja-desk.svg"
					onClick={iconClicked}
					alt=""
				/>
				{companyName}
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
		</div>
	);
}

export default NavBar;
