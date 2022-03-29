import "./SearchButton.css"
import ReactDOM from "react-dom";
import {useNavigate} from "react-router-dom"
import SearchResults from "../SearchResults/SearchResults"

function SearchButton(){

    let navigate = useNavigate();

    return(
        <div className="search-button">
            <button onClick={()=> {navigate("/SearchResults")}}> 
                Søgeninja</button>
        </div>

    )

}

export default SearchButton;