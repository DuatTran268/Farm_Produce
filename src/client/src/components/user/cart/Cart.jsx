import { faCartShopping } from "@fortawesome/free-solid-svg-icons";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import React from "react";
import { Link } from "react-router-dom";
import "../../../styles/Style.css"

const Cart = () => {
  return (
    <div className="cart">
      <Link to={"/cart"}>
        <FontAwesomeIcon icon={faCartShopping} fontSize={30} color="white"/>
      </Link>
    </div>
  );
};

export default Cart;
