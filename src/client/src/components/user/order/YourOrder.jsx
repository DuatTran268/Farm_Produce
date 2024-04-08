import React from "react";
import "./Checkout.css";
import { Button, Table } from "react-bootstrap";
import "./Order.css";
import { useCart } from "react-use-cart";
import { Link } from "react-router-dom";
import BtnSuccess from "../../common/BtnSuccess"
import { faCheck } from "@fortawesome/free-solid-svg-icons";

const YourOrder = () => {
  const {
    isEmpty,
    items,
    cartTotal,
  } = useCart();

  if (isEmpty) {
    return (
      <section>
        <h3 className="text-center mt-3 mb-3">
          Bạn phải có sản phẩm trong giỏ hàng
        </h3>
        <div className="text-center">
          <Link to={`/home`}>
            <Button className="btn btn-success mt-3 mb-3 text-center">
              Đi mua sản phẩm thôi
            </Button>
          </Link>
        </div>
      </section>
    );
  }

  return (
    <>
      <div className="checkout_order">
        <div className="checkout_title">Đơn hàng của bạn</div>
        <div className="checkout_order_content">
          <Table>
            <thead>
              <tr>
                <th>Sản phẩm</th>
                <th>Đơn giá</th>
                <th>Số lượng</th>
                <th>Tổng tiền</th>
              </tr>
            </thead>
            <tbody>
              <tr>
                <td>
                  {items.map((item, index) => {
                    return <div key={index}>{item.name}</div>;
                  })}
                </td>
                <td>
                  {items.map((item, index) => {
                    return <div key={index}>{item.price} VNĐ</div>;
                  })}
                </td>
                <td>
                  {items.map((item, index) => {
                    return <div key={index}>{item.quantity}</div>;
                  })}
                </td>
                <td>{items.map((item, index) => {
                    return <div key={index}>{item.price * item.quantity} VNĐ</div>;
                  })}</td>
              </tr>
              <tr>
                <td colSpan={3}>Tạm tính</td>
                <td>{cartTotal} VNĐ</td>
              </tr>
              <tr>
                <td colSpan={3}>Giao hàng</td>
                <td>Giao tận nơi</td>
              </tr>
              <tr>
                <td colSpan={3}>Tổng</td>
                <td>{cartTotal} VNĐ</td>
              </tr>
              <tr>
                <td colSpan={3}>Phương thức thanh toán</td>
                <td>Tiền mặt</td>
              </tr>
            </tbody>
          </Table>
          
        </div>
      </div>
    </>
  );
};
export default YourOrder;
