import "./Dropdown.css";
import Button from "../UI/Atoms/Button/Button";
import {useState} from "react";

const items = [
    {
        id: 0,
        content: "vores anbefaling"
    },
    {
        id: 1,
        content: "billigste",
    },
    {
        id: 2,
        content: "nærmeste",
    },
];

const Dropdown = (props) => {

    const [isOpen, setIsOpen] = useState(false);
    
    const [title, setTitle] = useState(items[0].content);


    const toggle = (event) => {
        setIsOpen(!isOpen);
    };

    const handleItemClick = (value) => {

        setIsOpen(!isOpen);
        // setTitle(items[id].content);
        setTitle(value);
        props.selectedOption(value);
    }

    return ( 
    <div>
        <Button className="dropdown-toggle" onClick={toggle}>søg efter: {title}</Button>
        {isOpen && (
            <ul className="dropdown-menu">
            {items.map(item => (
                <li className="dropdown-menu-item" onClick={e => handleItemClick(e.target.getAttribute('value'))} value={item.content} key={item.id} >
               
                    {item.content}

                </li>
            ))}
            </ul>
        )}
    </div>
    );
};

export default Dropdown;