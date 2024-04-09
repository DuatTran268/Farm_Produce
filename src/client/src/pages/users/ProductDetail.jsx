import React from "react";
import ProductDetails from "../../components/user/product/ProductDetails";
import Categoris from "../../components/user/categories/Categoris";
import LayoutClient from "../../components/user/common/LayoutClient";
import ProductComment from "../../components/user/commnent/ProductComment";
import "../../components/user/product/ProductDetail.css"
import CommentBox from "../../components/user/commnent/CommentBox";

const ProductDetail = () => {
  return (
    <LayoutClient>
      <div className="d-flex pt-3 category_slide justify-content-between">
        <div className="category col-3">
          <Categoris />
        </div>
        {/* slide advertising */}
        <div className="slider col-8">
          <ProductDetails />
        </div>
      </div>
      <div className="product_comment ">
        <div className=" product_comment_container row">
          <div className="product_col col-6">
            <ProductComment />
          </div>
          <div className="product_col col-6">
            <CommentBox/>
          </div>

        </div>
      </div>
    </LayoutClient>
  );
};

export default ProductDetail;
