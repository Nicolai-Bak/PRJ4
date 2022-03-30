import "./SearchButton.css"
import ReactDOM from "react-dom";
import {useNavigate} from "react-router-dom"
import SearchResults from "../SearchResults/SearchResults"


/*const SearchButton = () => {
    const searchEventHandler = () => {
        ReactDOM.render(
            <React.StrictMode>
                <SearchResults/>
            </React.StrictMode>,
            document.getElementById('root')
        );
    }*/

function SearchButton(){

    let navigate = useNavigate();

    return(
        <div className="searchButton">
            <button id="search-button" onClick={()=> {navigate("/SearchResults")}}> 
                Søgeninja</button>
        </div>

    )

}

export default SearchButton;