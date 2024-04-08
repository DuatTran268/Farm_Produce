import React, { useEffect, useState } from "react";

import { Image } from "react-bootstrap";
import BannerProductList from "../../../assets/banner_product_list.png";
import "../../../styles/user/Product.css";
import ProductTemplate from "./ProductTemplate";
import { useParams } from "react-router-dom";
import { getFilterProduct } from "../../../api/Product";
import Loading from "../../common/Loading";
import { useSelector } from "react-redux";

const ProductList = () => {
  const [getProduct, setGetProduct] = useState([]);
  const [isVisibleLoading, setIsVisibleLoading] = useState(true);
  const [pageNumber, setPageNumber] = useState(1);
  const productFilter = useSelector((state) => state.productFilter);
  let { id } = useParams,
    p = 1,
    ps = 5;
  function updatePageNumber(inc) {
    setPageNumber((currentVal) => currentVal + inc);
  }

  useEffect(() => {
    getFilterProduct(productFilter.name, ps, pageNumber).then((data) => {
      if (data) {
        setGetProduct(data.items);
        console.log("Checkdata", data);
      } else {
        setGetProduct([]);
      }
      setIsVisibleLoading(false);
    });
  }, [productFilter, ps, p, pageNumber]);

  return (
    <>
      <div className="product_body">
        <div className="product_body_flex">
          <div className="product_banner">
            <Image src={BannerProductList} alt="productbanner" />
          </div>
          <div className="product_list">
            {isVisibleLoading ? (
                <Loading />
            ) : (
              <>
                {getProduct.map((item, index) => {
                  return (
                    <>
                      <div
                        className="product_item col-11 col-md-6 col-lg-3 "
                        key={item.id}
                      >
                        <ProductTemplate
                          item={item} // Thêm dòng này để truyền item vào ProductTemplate
                          urlSlug={item.urlSlug}
                          name={item.name}
                          price={item.price}
                        />
                      </div>
                    </>
                  );
                })}
              </>
            )}
          </div>
        </div>
      </div>
    </>
  );
};
export default ProductList;
