import React from "react";
import { Image } from "react-bootstrap";
import adBaner from "../../../assets/dalavi.png";
import "../../../styles/Advertise.css";
import iconCarot from "../../../assets/icon-carrot.png";
import iconFresh from "../../../assets/icon-fresh.png";
import iconQuality from "../../../assets/icon-quality.png";

import iconDeliver from "../../../assets/icon-deliver.png";

const AdvertiseBanner = () => {
  return (
    <section>
      <div className="advertise_banner">
        <div className="container advertise_wrapper">
          <div className="row">
            <div className="col-lg-3">
              <div className="ad_column">
                <div className="ad_item">
                  <div className="ad_title">
                    <div className="ad_text">Tươi ngon từ vườn</div>
                    <Image src={iconFresh} className="ad_icon_left" />
                  </div>
                  <div className="ad_content">
                    100% Nông sản tươi được trồng và chuyển đi từ Đà Lạt, đảm
                    bảo sự tươi ngon, độ dinh dưỡng của từng nông sản.
                  </div>
                </div>
                <div className="ad_item">
                  <div className="ad_title">
                    <div className="ad_text">Sạch và an toàn</div>
                    <Image src={iconCarot} className="ad_icon_left" />
                  </div>
                  <div className="ad_content">
                    Nông sản tươi được trồng theo tiêu chuẩn sạch, an toàn để
                    đảm bảo sức khỏe.
                  </div>
                </div>
              </div>
            </div>
            <div className="col-lg-6">
              <div className="ad_banner">
                <Image src={adBaner} />
              </div>
            </div>
            <div className="col-lg-3">
              <div className="ad_column">
                <div className="ad_item">
                  <div className="ad_title">
                    <Image src={iconQuality} className="ad_icon_right" />
                    <div className="ad_text">Chất lượng</div>
                  </div>
                  <div className="ad_content">
                    Chất lượng được DaLaVi chú trọng qua quá trình chọn lựa,
                    đóng gói.
                  </div>
                </div>
                <div className="ad_item">
                  <div className="ad_title">
                    <Image src={iconDeliver} className="ad_icon_right" />
                    <div className="ad_text">Giao nhận thuận lợi</div>
                  </div>
                  <div className="ad_content">
                    Dịch vụ giao nhận được DaLaVi chú trong ở khâu NHANH - AN
                    TOÀN.
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </section>
  );
};
export default AdvertiseBanner;
