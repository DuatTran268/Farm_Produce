import React from "react";
import "../../../styles/user/Cart.css";
import { Table } from "react-bootstrap";

const CartTable = () => {
  return (
    <section>
      <Table striped hover>
        <thead>
          <tr>
            <th>Hình ảnh</th>
            <th>Sản phẩm</th>
            <th>Giá</th>
            <th>Số lượng</th>
            <th>Tạm tính</th>
            <th>Xoá</th>
          </tr>
        </thead>
        <tbody>
          <tr>
            <td>ảnh</td>
            <td>Dâu tây Đà Lạt</td>
            <td>100.000đ</td>
            <td>2</td>
            <td>200.000</td>
            <td>Icon xoá</td>
          </tr>
        </tbody>
      </Table>
    </section>
  );
};
export default CartTable;
