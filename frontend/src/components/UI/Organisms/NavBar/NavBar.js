import React from "react";
import "./NavBar.css";
import { IoMenu, IoClose} from "react-icons/io5";
import { useState} from "react";
import { Link } from "react-router-dom";


function NavBar(props) {

    const [showMenu, setShowMenu] = useState(false);    
    const toggleMenu = () => setShowMenu(!showMenu);
    const { pageLinks, companyName } = props;


    const linkRefs = () => {
		const links = pageLinks.map((link) => {
			return <span className="link"><Link to={link.link}>{link.text}</Link></span>;
		});
		console.log(pageLinks);
		return links;
	};



    return (
        <div className='navbar'>
            <div className='left-side__container'>
                <img id="ninja__logo" src="/images/ninja-desk.svg"/>
{companyName}
            </div>
            <div className='right-side__container'>
                <div className="navbar-menu">
                <div className={`navbar-links ${!showMenu ? 'hide' : ''}`}>{linkRefs()}</div>
                
                </div>
                <img id="shopping__cart" src="/images/shopping-cart.svg"/>

                <IoMenu onClick={toggleMenu} className={`hamburger-menu__icon ${showMenu ? 'hide' : ''}`}/>
                <IoClose onClick={toggleMenu} className={`hamburger-menu__icon ${showMenu ? '' : 'hide'}`}/>
            </div>
        </div>
    )
}

export default NavBar;

