import React from "react";
import "../../../styles/Checkout.css";
import { Table } from "react-bootstrap";
import "../../../styles/Order.css"

const YourOrder = () => {
  return (
    <>
      <div className="checkout_order">
        <div className="checkout_title">Đơn hàng của bạn</div>
        <div className="checkout_order_content">
          <Table>
            <thead>
              <tr>
                <th>Sản phẩm</th>
                <th></th>
              </tr>
            </thead>
            <tbody>
              <tr>
                <td>Dâu tây Đà Lạt</td>
                <td>200.000đ</td>
              </tr>
              <tr>
                <td>Tạm tính</td>
                <td>200.000đ</td>
              </tr>
              <tr>
                <td>Giao hàng</td>
                <td>Giao tận nơi</td>
              </tr>
              <tr>
                <td>Tổng</td>
                <td>200.000đ</td>
              </tr>
              <tr>
                <td>Phương thức thanh toán</td>
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
