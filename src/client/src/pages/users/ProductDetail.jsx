import React from "react";
import ProductDetails from "../../components/user/product/ProductDetails";
import Categoris from "../../components/user/categories/Categoris";
import LayoutClient from "../../components/user/common/LayoutClient";
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
    </LayoutClient>
  );
};

export default ProductDetail;
