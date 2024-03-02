import React from "react";

const FormOrder = () => {
  return (
      <section className="checkout_row row">
        <div className="checkout_col col-6">
          <div className="checkout_title">Thông tin thanh toán</div>
          <div className="checkout_fullname">
            <div className="checkout_field_fullname">
              <div className="checkout_input_title">Họ</div>
              <input type="text" name="" id="" className="checkout_input" />
            </div>
            <div className="checkout_field_fullname">
              <div className="checkout_input_title">Tên</div>
              <input type="text" name="" id="" className="checkout_input" />
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
      </section>
  );
};
export default FormOrder;
