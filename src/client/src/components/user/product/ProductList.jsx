import React, { useEffect, useState } from "react";

import { Link } from "react-router-dom";
import { Image } from "react-bootstrap";
import BannerProductList from "../../../assets/banner_product_list.png";
import "../../../styles/user/Product.css";
import { getAllProduct } from "../../../api/Product";

const ProductList = () => {

  const [getProduct, setGetProduct] = useState([]);

  useEffect(() => {
    getAllProduct().then((data) => {
      if (data){
        setGetProduct(data);
      }
      else{
        setGetProduct([]);
      }
    })
  }, []);


  
  return (
    <>
      <div className="product_body">
        <div className="product_body_flex">
          <div className="product_banner">
            <Image src={BannerProductList} alt="productbanner"/>
          </div>
          <div className="product_list">
            {getProduct.map((product, index) => {
              return (
                <div
                  className="product_item col-11 col-md-6 col-lg-3 "
                  key={index}
                >
                  <Link  to={`/detail/${product.urlSlug}`} className="product_link">
                    <div className="card p-0 overflow-hidden shadow">
                      <div className="product_image">
                        <img src="https://nongsandalat.vn/wp-content/uploads/2021/10/mut-dau-tay-1-370x290.jpg" className="product_img" />
                      </div>
                      <div className="product_content">
                        <p className="product_title">{product.name}</p>
                        <div className="product_bottom">
                          <div className="product_price">
                            <div className="product_price_origin">
                              {product.price} VNĐ
                            </div>
                            <div className="product_price_discount">
                              {product.price} VNĐ
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