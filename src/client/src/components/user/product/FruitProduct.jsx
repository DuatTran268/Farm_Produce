import React from "react";
import "./NavProduct.css";

import ProductHeader from "./ProductHeader";
import ProductList from "./ProductList";

const FruitProduct = () => {
  return (
    <div className="fruit_product">
      <ProductHeader name="Tất cả sản phẩm" />
      <ProductList />
    </div>
  );
};

export default FruitProduct;
