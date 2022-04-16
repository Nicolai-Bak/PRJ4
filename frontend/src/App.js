import "./App.css";
import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import NavBar from "./components/UI/Organisms/NavBar/NavBar";
import Home from "./Pages/Home";
import About from "./Pages/About";
import PageNotFound from "./Pages/PageNotFound";
import SearchResultsPage from "./Pages/SearchResultsPage";
import Footer from "./components/UI/Organisms/Footer/Footer";

function App() {
	const footerLinks = [
		{ text: "Kontakt os - ", link: "/PageNotFound" },
		{ text: "Hvem er vi? - ", link: "/About" },
		{ text: "Betingelser", link: "/PageNotFound" },
	];

	return (
		<div className="page__container">
			<Router>
				<NavBar companyName="PRISNINJA" />
				<Routes>
					<Route path="/" element={<Home />} />
					<Route path="/about" element={<About />} />
					<Route path="*" element={<PageNotFound />} />
					<Route path="/searchResults" element={<SearchResultsPage />} />
				</Routes>
				<Footer pageLinks={footerLinks} companyName="PRIS NINJA INC" />
			</Router>
		</div>
	);
}

export default App;
