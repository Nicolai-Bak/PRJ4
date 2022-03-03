import "./ShoppingList.css"
import SearchButton from "./SearchButton"
import NewItemForm from '../NewItem/NewItemForm';

const ShoppingList = () => {

    return(
 <div className="shopping-list">
     <NewItemForm/>
   <SearchButton/>
 </div>

    )

}

export default ShoppingList;