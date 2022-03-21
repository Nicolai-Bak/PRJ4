import React from 'react'
import './NavBar.css';

function NavBar() {
    return (
        <div className='navBar'>
            <div className='leftSide__container'>
                <img id="ninja__logo" src="/images/ninja-desk.svg"/>
                PRISNINJA
            </div>
            <div className='rightSide__container'>
                <img id="shopping__cart" src="/images/shopping-cart.svg"/>
            </div>

        </div>
    )
}

export default NavBar