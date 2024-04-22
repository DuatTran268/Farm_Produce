import React from "react";
import "./Cart.css";
import { Table } from "react-bootstrap";
import { Link } from "react-router-dom";
import { useCart } from "react-use-cart";

const CartInfor = () => {
  const { cartTotal } = useCart();
  const formatCurrency = (number) => {
    return number.toLocaleString("vi-VN", {
      style: "currency",
      currency: "VND",
    });
  };


  return (
    <section>
      {/* <div className=" btn btn-success cart_update">Cập nhật giỏ hàng</div> */}
      <div className="cart_bottom">
        <div className="cart_table">
          <div className="cart_coupon col-7">
            <div className="cart_coupon_title">Áp dụng mã giảm giá</div>
            <input
              className="cart_coupon_text"
              placeholder="Mã giảm giá"
            ></input>
            <div className="btn btn-success mx-3">Áp dụng</div>
          </div>
          <Table className="">
            <div className="cart_bottom_title">Cộng giỏ hàng</div>
            <tr>
              <td className="cart-lable w-25">Tổng tiền</td>
              <td className="cart-value w-25 text_total_price">{formatCurrency(cartTotal)}</td>
            </tr>
            <Link
              className=" btn btn-success cart_update mt-3"
              to={"/checkout"}
            >
              Tiến hành thanh toán
            </Link>
          </Table>
        </div>
      </div>
    </section>
  );
};
export default CartInfor;
