import React from 'react'
import "./Footer.css"

function Footer(props) {
	// const companyName = (name) => {
	// 	return props.name;
	// }

/* 	const pageLinks = ([...links]) => {
		const pageLinks = links.map((link) => {
			return `${link} `
		})
	return props.pageLinks(pageLinks);
	}
 */

  return (
    <div className='footer'>
        <h4 className='footerLinks'>{props.pageLinks}</h4>
        <p className='footerText'>&copy;{new Date().getFullYear()} {props.companyName} </p>
    </div>
  )
}

export default Footer