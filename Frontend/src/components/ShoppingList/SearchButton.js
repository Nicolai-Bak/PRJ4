
import "./ShoppingList.css"
const SearchButton = () => {
const searchEventHandler = () => {
    console.log(`Hello from SearchMan`);
}


    return(
    <button className="search-button" onClick={searchEventHandler}>Søg efter varer</button>

    )

}

export default SearchButton;