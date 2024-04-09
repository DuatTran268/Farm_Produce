import { faCartShopping } from "@fortawesome/free-solid-svg-icons";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import React from "react";
import { Link } from "react-router-dom";
import "../../../styles/user/Style.css"
import { useCart } from "react-use-cart";

const CartIcon = () => {
  const {totalItems} = useCart();

  return (
    <div className="cart">
      <Link className="cart-icon" to={'/cart'}>
        <FontAwesomeIcon icon={faCartShopping} fontSize={36}/>
        <span className="bg-danger">{totalItems}</span>
      </Link>
    </div>
  );
};

export default CartIcon;
