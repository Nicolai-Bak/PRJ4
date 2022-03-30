import "./SearchButton.css"
import ReactDOM from "react-dom";
import {useNavigate} from "react-router-dom"
import SearchResults from "../SearchResults/SearchResults"

function SearchButton(props){

    let navigate = useNavigate();

    const searchHandler = (event) => {
        console.log("searching...")
        props.onSearch();
        navigate("/SearchResults")
    }

    return(
        <div className="searchButton">
            <button id="search-button" onClick={searchHandler}> 
                Søgeninja</button>
        </div>

    )

}

export default SearchButton;