import "./App.css";
import {BrowserRouter as Router, Routes, Route} from "react-router-dom";
import NavBar from "./components/NavBar/NavBar"
import Home from "./Pages/Home";
import About from "./Pages/About";
import PageNotFound from "./Pages/PageNotFound";
import SearchResultsPage from "./Pages/SearchResultsPage";
import Footer from "./components/Footer/Footer";

function App() {


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
