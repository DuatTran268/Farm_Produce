import { faCartShopping } from "@fortawesome/free-solid-svg-icons";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import React, { useEffect, useState } from "react";
import { Link } from "react-router-dom";
import "../../../styles/user/NavProduct.css";
import "../../../styles/user/NewProduct.css";
import Slider from "react-slick";
import "slick-carousel/slick/slick.css";
import "slick-carousel/slick/slick-theme.css";
import { getProductNewestLimit } from "../../../api/Product";
import ProductHeader from "./ProductHeader";
import { useCart } from "react-use-cart";


const NewProduct = () => {

  const [getProduct, setProduct] = useState([]);

  useEffect(() => {
    getProductNewestLimit().then((data) => {
      if (data){
        setProduct(data);
      }
      else{
        setProduct([]);
      }
    });
  }, [])

  
  const { addItem } = useCart();
  const handleAddToCart = (product) => {
    addItem(product);
  };


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
      <ProductHeader name="Sản phẩm mới"/>
      {/* new product body */}
      <div className="product_body">
        <div className="product_note">
          Các sản phẩm mới nhất của công ty Nông sản Đà Lạt
        </div>

         
        <Slider {...settings}>
              {getProduct.map((product, index) => {
                return (
                  <div
                    className="new_product_wrapper"
                    key={index}
                  >
                  <Link className="product_item" to={`/detail/${product.urlSlug}`}>
                    <div className="new_product_image">
                      <img
                        className="new_product_img"
                        src="https://nongsandalat.vn/wp-content/uploads/2021/10/mut-dau-tay-1-370x290.jpg"
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
                        <div className="new_product_price">{product.price} VND</div>
                        <div className="new_product_add">
                          <Link className="new_product_addcart" to={"/cart"} onClick={() => handleAddToCart(product)} >
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
