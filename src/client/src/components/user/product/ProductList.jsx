import React, { useEffect, useState } from "react";

import { Image } from "react-bootstrap";
import BannerProductList from "../../../assets/banner_product_list.png";
import "../../../styles/user/Product.css";
import { getAllProduct } from "../../../api/Product";
import ProductTemplate from "./ProductTemplate";

const ProductList = () => {
  const [getProduct, setGetProduct] = useState([]);

  useEffect(() => {
    getAllProduct().then((data) => {
      if (data) {
        setGetProduct(data);
        console.log("Checkdata product", data);
      } else {
        setGetProduct([]);
      }
    });
  }, []);

  return (
    <>
      <div className="product_body">
        <div className="product_body_flex">
          <div className="product_banner">
            <Image src={BannerProductList} alt="productbanner" />
          </div>
          <div className="product_list">
            {getProduct.map((item, index) => {
              return (
                <>
                  <div className="product_item col-11 col-md-6 col-lg-3 " key={index}>
                    <ProductTemplate
                      urlSlug={item.urlSlug}
                      name={item.name}
                      price={item.price}
                    />
                  </div>
                </>
              );
            })}
          </div>
        </div>
      </div>
    </>
  );
};
export default ProductList;
