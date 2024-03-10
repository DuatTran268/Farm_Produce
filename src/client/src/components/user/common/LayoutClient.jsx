import React from "react";
import Header from "../layout/Header";
import Footer from "../layout/Footer";

const LayoutClient = ({ children }) => {
  return (
    <div className="">
      <Header />
      <div className="container">{children}</div>
      <Footer />
    </div>
  );
};
export default LayoutClient;
