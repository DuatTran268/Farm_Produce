import React from "react";
import Header from "../../components/user/common/Header";
import Footer from "../../components/user/common/Footer";
import "../../styles/Cart.css";
import CartTable from "../../components/user/cart/CartTable";
import CartInfor from "../../components/user/cart/CartInfor";

const Cart = () => {
  return (
    <section>
      <Header />
      <section className="container">
        <div className="cart_page">
          <div className="cart_name">Giỏ hàng của bạn</div>
          <div className="cart_content">
            <CartTable/>
            <CartInfor/>
            
          </div>
        </div>
      </section>
      <Footer />
    </section>
  );
};
export default Cart;
