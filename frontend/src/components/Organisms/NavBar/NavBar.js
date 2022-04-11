import React from 'react'
import './NavBar.css';
import {useNavigate} from "react-router-dom";
import ShoppingList from '../../ShoppingList/ShoppingList';
import {useEffect} from "react"

// const calculateTotal = () => {
//     const total = JSON.parse(localStorage.getItem("shoppingList"));
//     const cartTotal = total.length;
//     return cartTotal;
// }


function NavBar() {

    // useEffect(() => {
    //     calculateTotal();
    // }, [ShoppingList]);

    let navigate = useNavigate();
    return (
        <div className='navBar'>
            <div className='leftSide__container'>
                <img id="ninja__logo" src="/images/ninja-desk.svg" onClick={() => navigate("/")}/>
                PRISNINJA
            </div>
            <div className='rightSide__container'>
                <div className='navbarLinks'>Kontakt os</div>
                <div className='navbarLinks' onClick={() => navigate("/About")}>Hvem er vi?</div>
                <img id="shopping__cart" src="/images/shopping-cart.svg"/>
                <div id="cart-total"></div>
            </div>

        </div>
    )
}

export default NavBar