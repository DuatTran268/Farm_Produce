import React from "react";
import Header from "../../components/user/common/Header";
import Footer from "../../components/user/common/Footer";
import "../../styles/Checkout.css";
import { Table } from "react-bootstrap";
const Checkout = () => {
  return (
    <>
      <Header />
      <section>
        <div className="checkout_section container">
          <div className="checkout_title_heading">Thanh Toán</div>
          <div className="checkout_content">
            <div className="checkout_row row">
              <div className="checkout_col col-6">
                <div className="checkout_title">Thông tin thanh toán</div>
                <div className="checkout_fullname">
                  <div className="checkout_field_fullname">
                    <div className="checkout_input_title">Họ</div>
                    <input
                      type="text"
                      name=""
                      id=""
                      className="checkout_input"
                    />
                  </div>
                  <div className="checkout_field_fullname">
                    <div className="checkout_input_title">Tên</div>
                    <input
                      type="text"
                      name=""
                      id=""
                      className="checkout_input"
                    />
                  </div>
                </div>
                <div className="checkout_field">
                  <div className="checkout_input_title">Địa chỉ</div>
                  <input type="text" name="" id="" className="checkout_input" />
                </div>
                <div className="checkout_field">
                  <div className="checkout_input_title">Số điện thoại</div>
                  <input type="text" name="" id="" className="checkout_input" />
                </div>
                <div className="checkout_field">
                  <div className="checkout_input_title">Email</div>
                  <input type="text" name="" id="" className="checkout_input" />
                </div>
              </div>
              <div className="checkout_col col-6">
                <div className="checkout_title">Ghi chú đơn hàng</div>
                <div className="checkout_input_title">Ghi chú</div>
                <textarea
                  rows="4"
                  placeholder="Ghi chú thêm về đơn hàng của bạn"
                  className="checkout_input"
                ></textarea>
              </div>
            </div>

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
              <div className="button_checkout btn btn-success">Đặt hàng ngay</div>
            </div>
          </div>
        </div>
      </section>
      <Footer />
    </>
  );
};
export default Checkout;
