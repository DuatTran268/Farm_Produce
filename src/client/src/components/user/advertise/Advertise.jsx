import React from "react";
import fresh from "../../../assets/fresh.png";
import deliver from "../../../assets/deliver.png";
import price from "../../../assets/price.png";
import support from "../../../assets/support.png";
import { Image } from "react-bootstrap";
import "../../../styles/user/Advertise.css";

const Advertise = () => {
  return (
    <section>
      <div className="advertise">
        <div className="container">
          <div className="row">
            <div className="col-md-6 col-lg-3 adver_item">
              <div className="adver_item_wrapper">
                <div className="adver_icon">
                  <Image src={fresh} />
                </div>
                <div className="adver_content">
                  <div className="adver_title">Tươi ngon</div>
                  <div className="adver_description">Sản phẩm tươi ngon</div>
                </div>
              </div>
            </div>
            <div className="col-md-6 col-lg-3 adver_item">
              <div className="adver_item_wrapper">
                <div className="adver_icon">
                  <Image src={deliver} />
                </div>
                <div className="adver_content">
                  <div className="adver_title">Giao hàng nhanh</div>
                  <div className="adver_description">Tiện lợi đến tận nhà</div>
                </div>
              </div>
            </div>
            <div className="col-md-6 col-lg-3 adver_item">
              <div className="adver_item_wrapper">
                <div className="adver_icon">
                  <Image src={price} />
                </div>
                <div className="adver_content">
                  <div className="adver_title">Giá tốt mỗi ngày</div>
                  <div className="adver_description">Khuyến mãi hàng ngày</div>
                </div>
              </div>
            </div>
            <div className="col-md-6 col-lg-3 adver_item">
              <div className="adver_item_wrapper">
                <div className="adver_icon">
                  <Image src={support} />
                </div>
                <div className="adver_content">
                  <div className="adver_title">Hỗ trợ khách hàng</div>
                  <div className="adver_description">Nhân viên hỗ trợ 24/7</div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </section>
  );
};
export default Advertise;
