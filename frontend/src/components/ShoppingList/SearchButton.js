import "./SearchButton.css";
import { useNavigate } from "react-router-dom";

function SearchButton(props) {
	

	const searchHandler = (event) => {
		console.log("searching...");
		props.onSearch();
	};

	return (
		<div className="searchButton">
			<button id="search-button" onClick={searchHandler}>
				Søgeninja
			</button>
		</div>
	);
}

export default SearchButton;
