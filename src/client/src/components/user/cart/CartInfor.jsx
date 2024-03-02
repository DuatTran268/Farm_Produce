import React from "react";
import "../../../styles/Cart.css";
import { Table } from "react-bootstrap";
import { Link } from "react-router-dom";

const CartInfor = () => {
  return (
    <section>
      <div className=" btn btn-success cart_update">Cập nhật giỏ hàng</div>
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
              <td className="cart-lable w-25">Tạm tính</td>
              <td className="cart-value w-25">200.000đ</td>
            </tr>
            <tr>
              <td className="cart-lable w-25">Giao hàng</td>
              <td className="cart-value w-25">Giao hàng tận nhà</td>
            </tr>
            <tr>
              <td className="cart-lable w-25">Tổng</td>
              <td className="cart-value w-25">200.000đ</td>
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
