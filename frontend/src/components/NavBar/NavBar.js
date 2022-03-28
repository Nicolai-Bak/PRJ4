import React from 'react'
import './NavBar.css';
import {useNavigate} from "react-router-dom";

function NavBar() {
    let navigate = useNavigate();
    return (
        <div className='navBar'>
            <div className='leftSide__container'>
                <img id="ninja__logo" src="/images/ninja-desk.svg" onClick={() => navigate("/")}/>
                PRISNINJA
            </div>
            <div className='rightSide__container'>
                <div className='navbarLinks'>Kontakt Os</div>
                <div className='navbarLinks' onClick={() => navigate("/About")}>Hvem er vi?</div>
                <img id="shopping__cart" src="/images/shopping-cart.svg"/>
            </div>

        </div>
    )
}

export default NavBar