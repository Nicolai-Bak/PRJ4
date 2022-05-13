import "./App.css";
import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import NavBar from "./components/UI/Organisms/NavBar/NavBar";
import Home from "./Pages/Home";
import About from "./Pages/About";
import PageNotFound from "./Pages/PageNotFound";
import SearchResultsPage from "./Pages/SearchResultsPage";
import Footer from "./components/UI/Organisms/Footer/Footer";
import { useState } from "react";

function App() {
	const footerLinks = [{text: "Kontakt os - ", link: "/PageNotFound"}, {text: "Hvem er vi? - ", link: "/About"}, {text: "Forsiden", link: "/"}];
	const navbarLinks = [{text: "Kontakt os", link: "/PageNotFound"}, {text: "Hvem er vi", link: "/About"}];

	const [latitude, setLatitude] = useState("");
	const [longitude, setLongitude] = useState("");

	const geolocationHandler = (lat, long) => {
		setLatitude(lat);
		setLongitude(long);
	}

	return (
		<div className="page__container">
			<div className="content__wrapper">
				<Router>
					<NavBar 
						pageLinks={navbarLinks}
						companyName="PRISNINJA"/>
					<Routes>
						<Route path="/" element={<Home onSendLocation={geolocationHandler}/>} />
						<Route path="/about" element={<About />} />
						<Route path="*" element={<PageNotFound />} />
						<Route path="/searchResults" element={<SearchResultsPage latitude={latitude} longitude={longitude} />} />
					</Routes>
			<Footer 
			className="footer"
				pageLinks={footerLinks}
				companyName="PRIS NINJA INC"
				/>
			</Router>
			</div>
		</div>
	);
}

export default App;
