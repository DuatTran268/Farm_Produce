import React from "react";
import ProductHeader from "./ProductHeader";
import ProductCategoryList from "../../../pages/users/ProductCategoryList";



const ProductCategory = () => {
  return (
    <div className="fruit_product">
      <ProductHeader name="Nhóm đặc sản Đà Lạt" slug={'/category/dac-san-da-lat'}/>
      <ProductCategoryList/>
    </div>
  )
}
export default ProductCategory;