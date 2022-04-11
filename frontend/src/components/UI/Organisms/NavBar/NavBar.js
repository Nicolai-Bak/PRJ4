import React from "react";
import "./NavBar.css";

function NavBar(props) {

    return (
        <div className='navBar'>
            <div className='leftSide__container'>
                <img id="ninja__logo" src="/images/ninja-desk.svg" />
                {props.companyName}
            </div>
            <div className='rightSide__container'>
                <div className='navbarLinks'>Kontakt os</div>
                <div className='navbarLinks'>Hvem er vi?</div>
                <img id="shopping__cart" src="/images/shopping-cart.svg"/>
            </div>
        </div>
    )
}

export default NavBar