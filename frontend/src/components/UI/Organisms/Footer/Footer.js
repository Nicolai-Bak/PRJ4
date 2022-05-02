import React from "react";
import "./Footer.css";
import { Link } from "react-router-dom";

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
