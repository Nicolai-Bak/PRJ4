import Card from "../components/UI/Atoms/Card/Card";
import "./SearchResultsPage.css";
import Button from "../components/UI/Atoms/Button/Button";
import SearchResult from "../components/SearchResult/SearchResult";
import { IoArrowBack } from "react-icons/io5";
import { useNavigate } from "react-router-dom";
import Dropdown from "../components/Dropdown/Dropdown";
import { useState } from "react";

function SearchResultsPage(props) {
	let navigate = useNavigate();
	const [selectedOptionState, setSelectedOptionState] = useState("best");

	const goBack = () => {
		navigate("/");
	};

	const GetShoppingOptions = () => {
		const options = JSON.parse(localStorage.getItem("SearchResults"));
		let cheapest = null;
		let best = null;
		let nearest = null;

		if (options.cheapest !== null) {
			cheapest = Array.from(options.cheapest);
		}
		if (options.best !== null) {
			best = Array.from(options.best);
		}
		if (options.nearest !== null) {
			nearest = Array.from(options.nearest);
		}
		const allOptions = [cheapest, best, nearest];

		return allOptions;
	};
	const options = GetShoppingOptions();
	console.log("options0: ", options[0]);

	const selectedOptionHandler = (value) => {
		switch (value) {
			case "billigste":
				setSelectedOptionState("cheapest");
				break;
			case "vores anbefaling":
				setSelectedOptionState("best");
				break;
			case "nærmeste":
				setSelectedOptionState("nearest");
				break;

			default:
				console.log("selectedOptionHandler error");
		}
	};

	let displayOptions = null;

	switch (selectedOptionState) {
		case "cheapest":
			displayOptions = options[0].map((item) => (
				<div>
					<SearchResult
						latitude={props.latitude}
						longitude={props.longitude}
						products={item.products}
						price={item.totalPrice}
						distance={item.distance}
						storeName={item.brand}
						address={item.address}
					></SearchResult>
				</div>
			));
			break;
		case "best":
			displayOptions = options[1].map((item) => (
				<SearchResult
					latitude={props.latitude}
					longitude={props.longitude}
					products={item.products}
					price={item.totalPrice}
					distance={item.distance}
					storeName={item.brand}
					address={item.address}
				></SearchResult>
			));
			break;
		case "nearest":
			displayOptions = options[2].map((item) => (
				<SearchResult
					latitude={props.latitude}
					longitude={props.longitude}
					products={item.products}
					price={item.totalPrice}
					distance={item.distance}
					storeName={item.brand}
					address={item.address}
				></SearchResult>
			));
			break;
		default:
			console.log("default");
	}
	const [showError, setShowError] = useState(
		options[0].length < 1 ? true : false
	);

	return (
		<div className="search-results-page">
			{!showError && (
				<Card className="results-container">
					<div className="dropdown-menu-container">
						<Dropdown
							className="results-page-dropdown"
							selectedOption={selectedOptionHandler}
						></Dropdown>
					</div>
					{displayOptions}
				</Card>
			)}
			{!showError && (
				<div className="search-results-page-text-box">
					Fandt du ikke det, du søgte? <a onClick={goBack}>Klik her</a>, for at
					gå tilbage og foretage en ny søgning.
				</div>
			)}
			{showError && (
				<div className="search-results-not-found">
					<div className="image-wrapper">
						<img
							src="https://svgshare.com/i/h6r.svg"
							alt="ninja holding an apple"
						></img>
					</div>
					<div className="text-wrapper">
						<h1>Hovsa!</h1>
						<h2>Noget gik galt ved din søgning.</h2>
						<h3>
							Dette kan skyldes, at du har søgt efter nogle varer, som ikke kan
							findes i samme butik. Vi arbejder på en løsning! I mellemtiden kan
							du <a onClick={goBack}>gå tilbage her</a> og prøve igen.
						</h3>
					</div>
				</div>
			)}
		</div>
	);
}

export default SearchResultsPage;
