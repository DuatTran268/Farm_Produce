import React from "react";

import { Link } from "react-router-dom";
import { Image } from "react-bootstrap";
import BannerProductList from "../../../assets/banner_product_list.png";
import "../../../styles/Product.css";
import DProduct from "../../../data/DProduct";

const ProductList = () => {
  return (
    <>
      <div className="product_body">
        <div className="product_body_flex">
          <div className="product_banner">
            <Image src={BannerProductList} alt={"productbanner"} />
          </div>
          <div className="product_list">
            {DProduct.map((product, index) => {
              return (
                <div
                  className="product_item col-11 col-md-6 col-lg-3 "
                  key={index}
                >
                  <Link to={'/detail/'} className="product_link">
                    <div className="card p-0 overflow-hidden shadow">
                      <div className="product_image">
                        <img src={product.image} className="product_img" />
                      </div>
                      <div className="product_content">
                        <p className="product_title">{product.name}</p>
                        <div className="product_bottom">
                          <div className="product_price">
                            <div className="product_price_origin">
                              {product.price}
                            </div>
                            <div className="product_price_discount">
                              {product.price}
                            </div>
                          </div>
                          <Link className="btn btn-success" to={'/cart'}>Mua ngay</Link>
                        </div>
                      </div>
                    </div>
                  </Link>
                </div>
              );
            })}
          </div>
        </div>
      </div>
    </>
  )
}
export default ProductList;