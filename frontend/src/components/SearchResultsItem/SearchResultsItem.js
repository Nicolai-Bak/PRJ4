import Card from "../UI/Atoms/Card/Card";
import "./SearchResultsItem.css";
import { IoChevronDownSharp } from "react-icons/io5";
import Button from "../UI/Atoms/Button/Button";
import {useState} from "react";

const SearchResultsItem = (props) => {
    const [activeState, setActiveState] = useState(true);

    const toggleDetails = () => {
        setActiveState(!activeState);
    }

    return (
        <div className="search-results-item">
            <Card className="search-results-list-item">
            <div className="list-item-wrapper">
             <div className="list-item-price">{props.price}</div>
             <div className="list-item-stores">{props.stores}</div>
             <div className="list-item-distance">{props.distance}</div>
             <Button className="chevron-button" onClick={toggleDetails}>
                <IoChevronDownSharp className={`chevron-icon ${!activeState ? "rotate" : ""}`}/>
            </Button>
            </div>
            <div className={`search-results-details ${!activeState ? "show" : ""}`}>{props.results}</div>
            </Card>
        </div>
    );

};

export default SearchResultsItem;