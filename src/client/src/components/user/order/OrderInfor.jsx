import React from "react";
import YourOrder from "./YourOrder";
import { Link } from "react-router-dom";
import "./Order.css";

const OrderInfor = () => {
  return (
    <section>
      <div className="order_infor_wrapper">
        <div className="order_infor_heading">
          Thông tin chi tiết đơn hàng của bạn
        </div>
        {/* infor detail */}
        <div className="order_infor_wrapper">
          <div className="order_infor_thanks">
            Cảm ơn bạn đã đặt đơn hàng của chúng tôi 
          </div>
          <div className="order_infor_detail">
            <ul>
              {/* create form in footer allow customer import ID to get infor order */}
              <li className="order_detail_li">
                Mã số đơn hàng: <span className="order_detail_span">12</span>
              </li>
              <li className="order_detail_li">
                Ngày đặt hàng: <span className="order_detail_span">01/03/2024</span>
              </li>
              <li className="order_detail_li">
                Tổng tiền đơn hàng: <span className="order_detail_span">200.000đ</span>
              </li>
              <li className="order_detail_li">
                Phương thức thanh toán <span className="order_detail_span">Đưa tiền mặt khi giao hàng</span>
              </li>
            </ul>
          </div>
        </div>

        {/* <YourOrder /> */}
      </div>
      <div className="order_infor_done">
        <Link className=" btn btn-success" to={"/"}>
          Hoàn thành
        </Link>
      </div>
    </section>
  );
};
export default OrderInfor;
