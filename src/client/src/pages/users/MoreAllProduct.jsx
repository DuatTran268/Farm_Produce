import React from "react";
import ProductList from "../../components/user/product/ProductList";
import LayoutClient from "../../components/user/common/LayoutClient";



const MoreAllProduct = () => {
  return (
    <LayoutClient>
      <div className="more_title">
        <h3 className="text-success text-center mt-3 mb-3">Tất cả các sản phẩm</h3>
      </div>
      <ProductList/>
    </LayoutClient>
  )
}
export default MoreAllProduct;