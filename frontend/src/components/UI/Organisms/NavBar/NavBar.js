import React from "react";
import "./NavBar.css";
import { IoMenu, IoClose} from "react-icons/io5";
import { useState} from "react";

function NavBar(props) {

    const [showMenu, setShowMenu] = useState(false);    
    
    const toggleMenu = () => setShowMenu(!showMenu);
    
    return (
        <div className='navbar'>
            <div className='left-side__container'>
                <img id="ninja__logo" src="/images/ninja-desk.svg" />
                {props.companyName}
            </div>
            <div className='right-side__container'>
                <div className="navbar-menu">
                <div className={`navbar-links ${!showMenu ? 'hide' : ''}`}>Kontakt os</div>
                <div className={`navbar-links ${!showMenu ? 'hide' : ''}`}>Hvem er vi?</div>
                </div>
                <img id="shopping__cart" src="/images/shopping-cart.svg"/>

                <IoMenu onClick={toggleMenu} className={`hamburger-menu__icon ${showMenu ? 'hide' : ''}`}/>
                <IoClose onClick={toggleMenu} className={`hamburger-menu__icon ${showMenu ? '' : 'hide'}`}/>
            </div>
        </div>
    )
}

export default NavBar

