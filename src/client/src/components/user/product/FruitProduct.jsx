import React from "react";
import "../../../styles/NavProduct.css";

import ProductHeader from "./ProductHeader";
import ProductList from "./ProductList";

const FruitProduct = () => {
  return (
    <div className="fruit_product">
      
      <ProductHeader/>
      {/* fruitproduct */}
      <ProductList/>
    </div>
  );
};

export default FruitProduct;
