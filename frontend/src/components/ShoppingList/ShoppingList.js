import "./ShoppingList.css"
import SearchButton from "./SearchButton"

const ShoppingList = (props) => {

    return(
      <div className="shopping-list">
        <ul className="list-items">
          {props.items.map((item) => (
            <li key={item.id}>
              {item.name} - {item.amount}
            </li>
          ))}
        </ul>
   <SearchButton/>
 </div>

    )

}

export default ShoppingList;