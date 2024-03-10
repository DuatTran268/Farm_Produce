import React from "react";
import { Link } from "react-router-dom";
import "../../../styles/user/ProductDetail.css";
import dautay from "../../../assets/mutdau.jpg";
import { Image } from "react-bootstrap";

const ProductDetails = () => {
  return (
    <section>
      <div className="product_detail">
        <div className="product_detail_img col-5">
          <Image src={dautay} width={300} />
        </div>
        <div className="product_detail_content col-7">
          <div className="product_detail_title">Dâu tây nhật bản</div>
          <div className="product_detail_price">175.000đ</div>
          <div className="product_detail_desc">
            Dâu tây Nhật Bản cho quả đỏ, mọng nước, hương thơm mùi kẹo ngọt và
            có vị ngọt thanh đậm đà, khác với tất cả các loại dâu khác đang được
            trồng tại Đà Lạt và vùng lân cận hiện nay. Có các lựa chọn về size
            dâu dành cho quý khách: Quy cách đóng hộp: 500gr/hộp Size 25
            trái/hộp; 30 trái; 36 trái; 49 trái và 64 trái/hộp Quý khách NÊN đặt
            trước ít nhất 01 ngày để DaLaVi lên đơn, lên lịch hái ngày hôm sau
            và chuyển đi từ Đà Lạt nhằm đảm bảo sự TƯƠI NGON. Tư vấn và đặt
            hàng: 0914107107
          </div>
          <div className="product_detail_size">
            <label for="cars">Lựa chọn size: </label>
            <select name="cars" id="cars">
              <option value="volvo">S</option>
              <option value="saab">M</option>
              <option value="opel">L</option>
              <option value="audi">XL</option>
            </select>
          </div>
          <div className="product_detail_quantity_cart">
            <div className="product_detail_quantity">
              <span>Số lượng</span>
              <input type="number" min="1" max="100" />
            </div>
            <Link className="btn btn-success product_detail_addcart" to={'/cart'}>
              Thêm vào giỏ hàng
            </Link>
          </div>
        </div>
      </div>
    </section>
  );
};
export default ProductDetails;
