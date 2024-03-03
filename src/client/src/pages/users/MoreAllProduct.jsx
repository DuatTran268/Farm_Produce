import React from "react";
import ProductList from "../../components/user/product/ProductList";
import LayoutClient from "../../components/user/common/LayoutClient";



const MoreAllProduct = () => {
  return (
    <LayoutClient>
      <div className="more_title">
        Tất cả các sản phẩm
      </div>
      <ProductList/>
    </LayoutClient>
  )
}
export default MoreAllProduct;