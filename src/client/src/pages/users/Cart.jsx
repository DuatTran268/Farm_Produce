import React from "react";
import "../../components/user/cart/Cart.css";
import CartTable from "../../components/user/cart/CartTable";
import CartInfor from "../../components/user/cart/CartInfor";
import LayoutClient from "../../components/user/common/LayoutClient";

const Cart = () => {
  
  return (
    <LayoutClient>
      <div className="cart_page">
        <div className="cart_name">Giỏ hàng của bạn</div>
        <div className="cart_content">
          <CartTable />
        </div>
      </div>
    </LayoutClient>
  );
};
export default Cart;
