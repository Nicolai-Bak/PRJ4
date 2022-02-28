import "./ShoppingOption.css"

const ShoppingOption = () => {

    return ( 
        <div className="shopping-option" >
            <div className="store">
                [Butik]
            </div>
            <div className="price_distance">
                <div className="price">
                    [Samlet pris]
                </div>
                <div className="distance">
                    [Total afstand]
                </div>
            </div>
            <div className="products">
                [Vareliste]
            </div>
        </div>
        
    )
}

export default ShoppingOption;