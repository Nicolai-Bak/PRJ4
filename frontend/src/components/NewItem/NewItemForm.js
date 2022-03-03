import './NewItemForm.css'
import {useState} from 'react'

const NewItemForm = () => {
    let input;
    const [addItem, setAddItem] = useState([]);

    const submitItemHandler = (event) => {
        setAddItem(input);
        event.preventDefault();
        console.log(`You just tried to add ${input}`)
        setAddItem("");
    }

    const inputChangeHandler = (event) => {
        setAddItem(event.target.value);
    }

    // const labelContainer = () => {
    //     return (
    //     )
    // }

    return(
    <form onSubmit={submitItemHandler}>
        <input type="text" value={input} onChange={inputChangeHandler} placeholder='Tilføj vare her'></input>
        <button type="submit" >Tilføj</button>
        <br></br>
        <label>{addItem}</label>
    </form>

    )
}


export default NewItemForm;