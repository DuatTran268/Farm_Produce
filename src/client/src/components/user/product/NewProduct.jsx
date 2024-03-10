import { faCartShopping } from "@fortawesome/free-solid-svg-icons";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import React from "react";
import { Link } from "react-router-dom";
import "../../../styles/user/NavProduct.css";
import DProduct from "../../../data/DProduct";
import "../../../styles/user/NewProduct.css";
import Slider from "react-slick";
import "slick-carousel/slick/slick.css";
import "slick-carousel/slick/slick-theme.css";

const NewProduct = () => {
  const settings = {
    dots: false,
    infinite: true,
    speed: 500,
    slidesToShow: 2,
    slidesToScroll: 1,

    // slider auto scroll
    autoplay: {
      delay: 3000, // delay 3s
      disableOnInteraction: false,
    },
  };
  return (
    <div className="new_product">
      {/* reuse */}
      <div className="product_header">
        <div className="product_header_title">
          <div className="product_header_icon">
            <FontAwesomeIcon icon={faCartShopping} />
          </div>
          <span className="product_header_name">Sản phẩm mới</span>
        </div>
        <div className="view_more">
          <Link to={"/product/viewmore"} className="view_more_link">
            Xem thêm ...
          </Link>
        </div>
      </div>
      {/* new product body */}
      <div className="product_body">
        <div className="product_note">
          Các sản phẩm mới nhất của công ty Nông sản Đà Lạt
        </div>

         
        <Slider {...settings}>
              {DProduct.map((product, index) => {
                return (
                  <div
                    className="new_product_wrapper"
                    key={index}
                  >
                  <Link className="product_item" to={'/detail/'}>
                    <div className="new_product_image">
                      <img
                        className="new_product_img"
                        src={product.image}
                        alt={product.name}
                      ></img>
                    </div>
                    <div className="new_product_content">
                      <div className="new_product_name">
                        <h4>{product.name}</h4>
                      </div>

                      <div className="new_product_desc">
                        {product.description}
                      </div>
                      <div className="new_product_buy">
                        <div className="new_product_price">{product.price}</div>
                        <div className="new_product_add">
                          <Link className="new_product_addcart" to={"/cart"}>
                            Mua ngay
                            <FontAwesomeIcon
                              icon={faCartShopping}
                              className="new_product_icon"
                            />
                          </Link>
                        </div>
                      </div>
                    </div>
                  </Link>
                  </div>
                );
              })}
        </Slider>
          
      </div>
    </div>
  );
};

export default NewProduct;
