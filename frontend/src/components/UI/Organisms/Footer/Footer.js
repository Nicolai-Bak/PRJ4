import React from "react";
import "./Footer.css";
import { Link, useNavigate } from "react-router-dom";

function Footer(props) {
	const { pageLinks, companyName } = props;

	const linkRefs = () => {
		const links = pageLinks.map((link) => {
			return <Link to={link.link}>{link.text}</Link>;
		});
		console.log(pageLinks);
		return links;
	};

	return (
		<div className="footer">
			<h4 className="footerLink">{linkRefs()}</h4>
			<p className="footerText">
				&copy;{new Date().getFullYear()} {companyName}{" "}
			</p>
		</div>
	);
}

export default Footer;
