import React from "react";
import Header from "../../components/user/common/Header";
import Footer from "../../components/user/common/Footer";
import FruitProduct from "../../components/user/product/FruitProduct";
import ProductList from "../../components/user/product/ProductList";



const MoreAllProduct = () => {
  return (
    <>
      <Header/>
      <section className="container">
        <ProductList/>
      </section>
      <Footer/>
    </>
  )
}
export default MoreAllProduct;