import "./SearchButton.css"
import ReactDOM from "react-dom";
import React from "react";
import SearchResults from "../SearchResults/SearchResults"

const SearchButton = () => {
    const searchEventHandler = () => {
        ReactDOM.render(
            <React.StrictMode>
                <SearchResults/>
            </React.StrictMode>,
            document.getElementById('root')
        );
    }


    return(
        <div className="search-button">
            <button onClick={searchEventHandler}>I am Searchman</button>
        </div>

    )

}

export default SearchButton;