import "./PageNotFound.css";

function PageNotFound() {
	return (
        <div className="page-not-found">
            <div className="textbox">
                <img id="ninja" src="/images/ninja-not-found.png"></img>
                <div id="error-number"> 404</div>
                Page Not Found
                <p>Siden du leder efter findes desværre ikke!</p>
            </div>
        </div>
	);
}

export default PageNotFound;