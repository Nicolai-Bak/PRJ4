import React from "react";
import "./Footer.css";

function Footer(props) {
	const { pageLinks, companyName } = props;


	const linkRefs = () => {
		const links = pageLinks.map((link) => {
			return (
				<a href={link.link}>{link.text}</a>
			);
		});
		console.log(links);
		return links;
		};

		return (
    	<div className='footer'>
        	<h4 className='footer-links'>{linkRefs()}</h4>
        	<p className='footer-text'>&copy;{new Date().getFullYear()} {companyName} </p>
    	</div>
  		)
}

export default Footer;