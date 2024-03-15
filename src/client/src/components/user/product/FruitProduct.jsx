import React from "react";
import "../../../styles/user/NavProduct.css";

import ProductHeader from "./ProductHeader";
import ProductList from "./ProductList";

const FruitProduct = () => {
  return (
    <div className="fruit_product">
      
      <ProductHeader name="Nhóm rau"/>
      {/* fruitproduct */}
      <ProductList/>
    </div>
  );
};

export default FruitProduct;
