import React, { useEffect, useState } from "react";

import { Image } from "react-bootstrap";
import BannerProductList from "../../../assets/banner_product_list.png";
import "./Product.css";
import ProductTemplate from "./ProductTemplate";
import { useParams } from "react-router-dom";
import { getFilterProduct } from "../../../api/Product";
import Loading from "../../common/Loading";
import { useSelector } from "react-redux";

import imagenotfound from "../../../assets/imagenotfound.jpg";


const ProductList = () => {
  const [getProduct, setGetProduct] = useState([]);
  const [isVisibleLoading, setIsVisibleLoading] = useState(true);
  const [pageNumber, setPageNumber] = useState(1);
  const productFilter = useSelector((state) => state.productFilter);
  let { id } = useParams,
    p = 1,
    ps = 8;
  function updatePageNumber(inc) {
    setPageNumber((currentVal) => currentVal + inc);
  }

  useEffect(() => {
    getFilterProduct(productFilter.name, true, ps, pageNumber).then((data) => {
      if (data) {
        setGetProduct(data.items);
        // console.log("Check data item product list: ", data.items)
      } else {
        setGetProduct([]);
      }
      setIsVisibleLoading(false);
    });
  }, [productFilter, ps, p, pageNumber]);

  // Hàm lấy URL của ảnh thumbnail
  const getThumbnailUrl = (item) => {
    if (item.images && item.images.length > 0) {
      return item.images[0].urlImage; // Trả về URL của ảnh đầu tiên trong mảng images của sản phẩm
    } else {
      return  // Trả về URL của ảnh mặc định nếu không có ảnh trong mảng images
    }
  };



  return (
    <>
      <div className="product_body">
        <div className="product_body_flex">
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
                          item={item} 
                          thumbnailUrl={getThumbnailUrl(item)}
                          urlSlug={item.urlSlug}
                          name={item.name}
                          priceVirtual={item.priceVirtual}
                          price={item.price}
                          unit={item.unit.name}
                          
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
