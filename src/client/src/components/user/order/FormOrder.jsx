import React from "react";
import "./Order.css"


const FormOrder = () => {
  return (
      <section className="checkout_row row">
        <div className="checkout_col col-6">
          <div className="checkout_title">Thông tin thanh toán</div>
          <div className="checkout_field checkout_fullname">
            <div className="checkout_field_fullname">
              <input type="text" name="" id="" className="checkout_input" placeholder="Họ" />
            </div>
            <div className="checkout_field_fullname">
              <input type="text" name="" id="" className="checkout_input" placeholder="Tên" />
            </div>
          </div>
          <div className="checkout_field">
            <input type="text" name="" id="" className="checkout_input" placeholder="Địa chỉ" />
          </div>
          <div className="checkout_field">
            <input type="text" name="" id="" className="checkout_input" placeholder="Số điện thoại" />
          </div>
          <div className="checkout_field">
            <input type="text" name="" id="" className="checkout_input" placeholder="Email" />
          </div>
        </div>
        <div className="checkout_col col-6">
          <div className="checkout_title">Ghi chú đơn hàng</div>
          <textarea
            rows="4"
            placeholder="Ghi chú thêm về đơn hàng của bạn"
            className="checkout_input"
          ></textarea>
        </div>
      </section>
  );
};
export default FormOrder;
