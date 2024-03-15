import React, { useEffect, useState } from "react";
import "../../styles/user/Product.css";
import { Image } from "react-bootstrap";
import { Link, useParams } from "react-router-dom";
import { getProductByCategorySlug } from "../../api/Product";
import BannerProductList from "../../assets/banner_product_list.png";
import LayoutClient from "../../components/user/common/LayoutClient";
import CategoryName from "../../components/user/categories/CategoryNames";

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
            <CategoryName/>  
        <div className="product_body_flex">
          <div className="product_banner">
            <Image src={BannerProductList} alt="productbanner" />
          </div>
          <div className="product_list">
            
            {productCategory.length > 0 ? (
              <>
                {productCategory.map((product, index) => {
                  return (
                    <div
                      className="product_item col-11 col-md-6 col-lg-3 "
                      key={index}
                    >
                      <Link
                        to={`/detail/${product.urlSlug}`}
                        className="product_link"
                      >
                        <div className="card p-0 overflow-hidden shadow">
                          <div className="product_image">
                            <img
                              src="https://nongsandalat.vn/wp-content/uploads/2021/10/mut-dau-tay-1-370x290.jpg"
                              className="product_img"
                            />
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
                              <Link className="btn btn-success" to={"/cart"}>
                                Mua ngay
                              </Link>
                            </div>
                          </div>
                        </div>
                      </Link>
                    </div>
                  );
                })}
              </>
            ) : (
              <>
                <h3 className="text-danger px-3" >Không có sản phẩm nào </h3>
              </>
            )}
          </div>
        </div>
      </div>
    </LayoutClient>
  );
};

export default ProductInCategory;
