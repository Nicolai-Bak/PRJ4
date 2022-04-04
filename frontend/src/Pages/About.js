import "./About.css"
import React from "react";

function About() {

	return (
		<div className="about-page">
			<div className="about-page__container">
				<div className="description">
			    <h1>Hvem er vi?</h1>
				Pris Ninja har eksisteret siden 2022 og er en virksomhed i fuld fremdrift. 
				Vi har udelukkende et mål: At give DIG de bedste handlemuligheder, så du
				slipper for at tage stilling til, hvor netop du skal handle. 
				Vi sætter dine behov først og sammensætter en optimeret løsning, som sparer
				dig tid og krafter.
				<h2>Teamet</h2>
				Vi sidder i øjeblikket 7 softwareingeniører i virksomheden. Lukas er vores API guru.
				Nicolai er vores mensa-geni. Aksel er vores Rider-ekspert. Simon er vores udemy-ekspert.
				Anton er vores database-mand. Peter er Scrum-daddy. Nikolaj kan stadig ikke komme på edu-roam. 
				<h2>Hvor holder vi til?</h2>
				Vores hovedkontor ligger Aarhus' IT-hjerte: i kælderen i Nygaard. Her finder
				du inspirerende lokaler og gode vibes.
				</div>
					<div className="image__container">
						<img id="ninja-about" src="/images/ninja-about.svg"></img> 
					</div>
			</div>
		</div>
	);
}

export default About;