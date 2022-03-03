import "./SearchButton.css"

const SearchButton = () => {
const searchEventHandler = () => {
    console.log(`Hello from SearchMan`);
}


    return(
 <div className="search-button">
    <button onClick={searchEventHandler}>I am Searchman</button>
 </div>

    )

}

export default SearchButton;