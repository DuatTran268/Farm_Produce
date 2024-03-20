import React, { useEffect, useState } from "react";
import "../../styles/user/Product.css";
import { Image } from "react-bootstrap";
import {  useParams } from "react-router-dom";
import { getProductByCategorySlug } from "../../api/Product";
import BannerProductList from "../../assets/banner_product_list.png";
import LayoutClient from "../../components/user/common/LayoutClient";
import CategoryName from "../../components/user/categories/CategoryNames";
import ProductTemplate from "../../components/user/product/ProductTemplate";

const ProductInCategory = () => {
  const [productCategory, setProductCategory] = useState([]);

  const params = useParams();
  const { slug } = params;

  useEffect(() => {
    getProductByCategorySlug(slug).then((data) => {
      if (data) {
        setProductCategory(data.items);
      } else {
        setProductCategory({});
      }
    });
  }, [slug]);

  return (
    <LayoutClient>
      <div className="product_body">
        <CategoryName />
        <div className="product_body_flex">
          <div className="product_banner">
            <Image src={BannerProductList} alt="productbanner" />
          </div>
          <div className="product_list">
            {productCategory.length > 0 ? (
              <>
                {productCategory.map((product, index) => {
                  return (
                    <div className="product_item col-11 col-md-6 col-lg-3 ">
                      <ProductTemplate
                        key={index}
                        urlSlug={product.urlSlug}
                        name={product.name}
                        price={product.price}
                      />
                    </div>
                  );
                })}
              </>
            ) : (
              <>
                <h3 className="text-danger px-3">Không có sản phẩm nào </h3>
              </>
            )}
          </div>
        </div>
      </div>
    </LayoutClient>
  );
};

export default ProductInCategory;
