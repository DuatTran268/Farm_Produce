import React from "react";
import "../../styles/user/Checkout.css";
import YourOrder from "../../components/user/order/YourOrder";
import FormOrder from "../../components/user/order/FormOrder";
import { Link } from "react-router-dom";
import LayoutClient from "../../components/user/common/LayoutClient";
import { useCart } from "react-use-cart";

const Checkout = () => {
  const {
    isEmpty,
  } = useCart();

  return (
    <LayoutClient>
      <div className="checkout_title_heading">Thanh Toán</div>
      <div className="checkout_content">
        <FormOrder />
        <YourOrder />
        <div className="button_checkout">
          <Link className=" btn btn-success" to={"/checkout/orderinfor"}>
            Đặt hàng ngay
          </Link>
        </div>
      </div>
    </LayoutClient>
  );
};
export default Checkout;
