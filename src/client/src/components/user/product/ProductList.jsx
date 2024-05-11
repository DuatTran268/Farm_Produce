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
import ProductFilter from "../../admin/filter/ProductFilter";

const ProductList = () => {
  const [getProduct, setGetProduct] = useState([]);
  const [isVisibleLoading, setIsVisibleLoading] = useState(true);
  const [pageNumber, setPageNumber] = useState(1);
  const productFilter = useSelector((state) => state.productFilter);
  // Thêm useState để lưu trạng thái sắp xếp
  const [sortOrder, setSortOrder] = useState();
  const [sortColumn, setSortColumn] = useState("Price");

  const handleSortChange = (selectedSortOrder) => {
    // Cập nhật giá trị của sortOrder
    setSortOrder(selectedSortOrder);
  };


  let { id } = useParams,
    p = 1,
    ps = 8;

  function updatePageNumber(inc) {
    setPageNumber((currentVal) => currentVal + inc);
  }

  useEffect(() => {
    getFilterProduct(
      productFilter.name,
      true,
      ps,
      pageNumber,
      sortColumn,
      sortOrder
    ).then((data) => {
      if (data) {
        setGetProduct(data.items);
        console.log("Check data item product list: ", data.items);
      } else {
        setGetProduct([]);
      }
      setIsVisibleLoading(false);
    });
  }, [productFilter, ps, p, pageNumber, sortColumn, sortOrder]);

  // Hàm lấy URL của ảnh thumbnail
  const getThumbnailUrl = (item) => {
    if (item.images && item.images.length > 0) {
      return item.images[0].urlImage; // Trả về URL của ảnh đầu tiên trong mảng images của sản phẩm
    } else {
      return; // Trả về URL của ảnh mặc định nếu không có ảnh trong mảng images
    }
  };

  return (
    <>
      <div className="product_body">
        <div className="box_filter_allproduct">
          {/* <button onClick={() => handleSortChange("ASC")}>
            Sắp xếp giá tăng dần
          </button>
          <button onClick={() => handleSortChange("DESC")}>
            Sắp xếp giá giảm dần
          </button> */}
          <select onChange={(e) => handleSortChange(e.target.value)}  className="option_sort">
          <option className="option_sort_value">---Sắp xếp theo giá---</option>
            <option value="ASC">Giá tăng dần</option>
            <option value="DESC">Giá giảm dần</option>
          </select>
          <ProductFilter />
        </div>
        <div className="product_body_flex">
          <div className="product_list">
            {isVisibleLoading ? (
              <Loading />
            ) : (
              <>
                {getProduct.length > 0 ? (
                  getProduct.map((item, index) => {
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
                            quantityAvailable={item.quantityAvailable}
                            price={item.price}
                            unit={item.unit.name}
                          />
                        </div>
                      </>
                    );
                  })
                ) : (
                  <div className="text-danger">
                    <h4>Không tìm thấy sản phẩm nào</h4>
                  </div>
                )}
              </>
            )}
          </div>
        </div>
      </div>
    </>
  );
};
export default ProductList;
