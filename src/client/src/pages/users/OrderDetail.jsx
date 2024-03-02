import React from "react";
import OrderInfor from "../../components/user/order/OrderInfor";
import Header from "../../components/user/common/Header";
import Footer from "../../components/user/common/Footer";


const OrderDetail = () => {
  return (
    <section>
      <Header/>
      <div className="container">
        <OrderInfor/>
      </div>
      <Footer/>
    </section>
  )
}
export default OrderDetail;