import React from "react";
import ProductHeader from "./ProductHeader";
import ProductCategoryList from "../../../pages/users/ProductCategoryList";



const ProductCategory = () => {
  return (
    <div className="fruit_product">
      <ProductHeader name="Nhóm sản phẩm rau xanh" slug={'/category/rau'}/>
      <ProductCategoryList/>
    </div>
  )
}
export default ProductCategory;