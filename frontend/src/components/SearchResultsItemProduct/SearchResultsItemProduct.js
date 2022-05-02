import Card from "../UI/Atoms/Card/Card";
import "./SearchResultsItemProduct.css";

const SearchResultsItemProduct = (props) => {

    return (
            <Card className="search-results-item-product">
            <div className="item-product">{props.results}</div>
            </Card>
    );

};

export default SearchResultsItemProduct;