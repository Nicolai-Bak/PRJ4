/* import SearchResults from "../components/SearchResults/SearchResults"; */
import Card from "../components/UI/Atoms/Card/Card";
import "./SearchResultsPage.css";
import Button from "../components/UI/Atoms/Button/Button";
import SearchResultsItem from "../components/SearchResultsItem/SearchResultsItem";
import { IoArrowBack } from "react-icons/io5";
import { useNavigate } from "react-router-dom";
import Dropdown from "../components/Dropdown/Dropdown";
import { useState } from "react";
import SearchResultsItemProduct from "../components/SearchResultsItemProduct/SearchResultsItemProduct";

function SearchResultsPage() {
	let navigate = useNavigate();
	const [selectedOptionState, setSelectedOptionState] = useState('best');

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
	console.log(options[0]);

	const selectedOptionHandler = (value) => {
		switch (value) {
		case "billigste":
			setSelectedOptionState('cheapest');
			break;
		case "vores anbefaling":
			setSelectedOptionState('best');
			break;
		case "nærmeste":
			setSelectedOptionState('nearest');
			break;
		
		default:
			console.log('selectedOptionHandler error');
	}
	};
	


	let displayOptions = null;

	switch (selectedOptionState) {
		case 'cheapest':
			displayOptions = options[0].map((item) => (
				<div>
				<SearchResultsItem
				price={item.totalPrice}
				distance={item.distance}
				storeName={item.brand}
				>
				</SearchResultsItem>
					{/* {item.products((product) => 
						<SearchResultsItemProduct product={product.name} />
					)
					} */}

				</div>
			));
			break;
		case 'best':
			displayOptions = options[1].map((item) => (
				<SearchResultsItem
					price={item.totalPrice}
					distance={item.distance}
					storeName={item.brand}
				>
				</SearchResultsItem>
			));
			break;
		case 'nearest':
			displayOptions = options[2].map((item) => (
				<SearchResultsItem
					price={item.totalPrice}
					distance={item.distance}
					storeName={item.brand}
				>
				</SearchResultsItem>
			));
			break;
		default:
			console.log('default');
	}

	return (
		<div className="search-results-page">
			<Card className="results-container">
				<div className="dropdown-menu-container">
					<Dropdown
						selectedOption={selectedOptionHandler}
					></Dropdown>
				</div>
				{displayOptions} 

			</Card>
			<Button className="search-results-page-button" onClick={goBack}><IoArrowBack /> til forsiden</Button>
		</div>
	);

}

export default SearchResultsPage;