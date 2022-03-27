import React from 'react';
import './ListItem.css';

// component list item with name, amount, unit, and id and buttons to remove and edit
const ListItem = (props) => {
    const removeItemHandler = () => {
        console.log(`removeItemHandler called with id: ${props.id}`);
        props.onRemoveItem(props.id);
    };

    return (
        <div className="List__item">
            <div><h3>Hello</h3></div>
            <span>{props.amount}</span>
            <span>{props.unit}</span>
            {/* button to decrease amount*/}
            <button onClick={() => props.onDecreaseAmount(props.id)}>-</button>
            {/* button to increase amount*/}
            <button onClick={() => props.onIncreaseAmount(props.id)}>+</button>
            <button onClick={removeItemHandler}>Remove</button>
        </div>
    );
}

export default ListItem;