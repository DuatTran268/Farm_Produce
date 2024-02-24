import React from "react";
import "../../../styles/NavProduct.css";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faCartShopping } from "@fortawesome/free-solid-svg-icons";
import { Link } from "react-router-dom";
import { Button, Image } from "react-bootstrap";
import BannerProductList from "../../../assets/banner_product_list.png";
import "../../../styles/Product.css";
import DProduct from "../../../data/DProduct";

const FruitProduct = () => {
  return (
    <div className="fruit_product">
      <div className="product_header">
        <div className="product_header_title">
          <div className="product_header_icon">
            <FontAwesomeIcon icon={faCartShopping} />
          </div>
          <span className="product_header_name">Nhóm trái cây</span>
        </div>
        <div className="view_more">
          <Link to={"/aa"} className="view_more_link">
            Xem thêm ...
          </Link>
        </div>
      </div>

      {/* fruitproduct */}
      <div className="product_body">
        <div className="product_body_flex">
          <div className="product_banner">
            <Image src={BannerProductList} alt="product banner" />
          </div>
          <div className="product_list">
            {DProduct.map((product, index) => {
              return (
                <div
                  className="product_item col-11 col-md-6 col-lg-3 mb-2 "
                  key={index}
                >
                  <div className="card p-0 overflow-hidden shadow">
                    <div className="product_image">
                      <img src={product.image} className="product_img" />
                    </div>
                    <div className="product_content">
                      <p className="product_title">{product.name}</p>
                      <div className="product_bottom">
                        <div className="product_price">{product.price}</div>
                        <Button className="btn-success">Mua ngay</Button>
                      </div>
                    </div>
                  </div>
                </div>
              );
            })}
          </div>
        </div>
      </div>
    </div>
  );
};

export default FruitProduct;
