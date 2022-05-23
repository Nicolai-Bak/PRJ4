import React from "react";
import "./Footer.css";
import { Link } from "react-router-dom";

/**
 * @classdesc
 * This is the footer of the website. It contains links to the different pages and the company name.
 *
 * @category UI
 * @subcategory Organisms
 * @component
 * @hideconstructor
 *
 */

function Footer(props) {
	const { pageLinks, companyName } = props;

	const linkRefs = () => {
		const links = pageLinks.map((link) => {
			return (
				<Link key={(Math.random() * 20).toFixed(4)} to={link.link}>
					{link.text}
				</Link>
			);
		});
		console.log(pageLinks);
		return links;
	};

	return (
		<div className="footer">
			<h4 className="footer-links">{linkRefs()}</h4>
			<p className="footer-text">
				&copy;{new Date().getFullYear()} {companyName}{" "}
			</p>
		</div>
	);
}

export default Footer;
