import React from "react";
import Header from "../../components/user/common/Header";
import Footer from "../../components/user/common/Footer";
import "../../styles/Checkout.css";
import YourOrder from "../../components/user/order/YourOrder";
import FormOrder from "../../components/user/order/FormOrder";
import { Link } from "react-router-dom";

const Checkout = () => {
  return (
    <>
      <Header />
      <section>
        <div className="checkout_section container">
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
        </div>
      </section>
      <Footer />
    </>
  );
};
export default Checkout;
