import "./App.css";
import {BrowserRouter as Router, Routes, Route} from "react-router-dom";
import NavBar from "./components/NavBar/NavBar"
import Home from "./Pages/Home";
import About from "./Pages/About";
import PageNotFound from "./Pages/PageNotFound";
import SearchResultsPage from "./Pages/SearchResultsPage";
import Footer from "./components/Footer/Footer";

function App() {

	const removeItemHandler = (id) => {
		console.log(`removeItemHandler called with id: ${id}`);
		setShoppingList((prevShoppingList) => {
			return prevShoppingList.filter((item) => item.id !== id);
		});
	};

	const changeAmountHandler = (id, change) => {
		console.log(`changeAmountHandler called with id: ${id} and change: ${change}`);
		setShoppingList((prevShoppingList) => {
			return prevShoppingList.map((item) => {
				if (item.id === id) {
					let oldAmount = +item.amount;
					return {
						...item,
						amount: (oldAmount + change).toFixed(2),
					};
				} else return item;
			});
		});
	};

	return (
		<div className="page__container">
		<div className="content-wrapper">
		<Router>
			<NavBar/>
		<Routes>
			<Route path='/' element={<Home/>}/>
			<Route path="/about" element={<About />} />
			<Route path="*" element={<PageNotFound />} />
			<Route path="/searchResults" element={<SearchResultsPage />} />
		</Routes>
		</Router>
		</div>
		<Footer/>
		</div>
	);
}

export default App;
