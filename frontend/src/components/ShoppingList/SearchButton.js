import "./SearchButton.css";
import { useNavigate } from "react-router-dom";

function SearchButton(props) {
	let navigate = useNavigate();

	const searchHandler = (event) => {
		console.log("searching...");
		props.onSearch();
		navigate("/SearchResults");
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
