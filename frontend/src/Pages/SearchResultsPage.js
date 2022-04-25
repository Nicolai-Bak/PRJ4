/* import SearchResults from "../components/SearchResults/SearchResults"; */
import Card from "../components/UI/Atoms/Card/Card";
import "./SearchResultsPage.css";
import Button from "../components/UI/Atoms/Button/Button";
import SearchResultsItem from "../components/SearchResultsItem/SearchResultsItem";
import {IoArrowBack} from "react-icons/io5";
import { useNavigate } from "react-router-dom";
import Dropdown from "../components/Dropdown/Dropdown";
 
function SearchResultsPage() {
	let navigate = useNavigate();
	const goBack = () => {
		navigate("/");
	};


	return (
		<div className="search-results-page">
			<Card className="results-container">
				<div className="dropdown-menu-container">
				<Dropdown></Dropdown>
				</div>
				<SearchResultsItem 
					price="Pris: 417kr" 
					stores="1 butik" 
					distance="2km" 
					results="REMA1000">
					</SearchResultsItem>
			</Card>
			<Button className="search-results-page-button" onClick={goBack}><IoArrowBack/> til forsiden</Button>
			</div>
	);
}

export default SearchResultsPage;