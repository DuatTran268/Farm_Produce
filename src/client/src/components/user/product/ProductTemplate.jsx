import React from "react";
import { Link } from "react-router-dom";
import { Image } from "react-bootstrap";
import "../../../styles/user/Product.css";

const ProductTemplate = ({ urlSlug, name, price }) => {
  return (
    <section>
      <div>
        <Link to={`/detail/${urlSlug}`} className="product_link">
          <div className="card p-0 overflow-hidden shadow">
            <div className="product_image">
              <Image
                src={
                  "https://nongsandalat.vn/wp-content/uploads/2021/10/mut-dau-tay-1-370x290.jpg"
                }
                className="product_img"
              />
            </div>
            <div className="product_content">
              <p className="product_title">{name}</p>
              <div className="product_bottom">
                <div className="product_price">
                  <div className="product_price_origin">{price} VNĐ</div>
                  <div className="product_price_discount">{price} VNĐ</div>
                </div>
                <Link className="btn btn-success" to={"/cart"}>
                  Mua ngay
                </Link>
              </div>
            </div>
          </div>
        </Link>
      </div>
    </section>
  );
};
export default ProductTemplate;
