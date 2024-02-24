import { faCartShopping } from "@fortawesome/free-solid-svg-icons";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import React from "react";
import { Link } from "react-router-dom";
import "../../../styles/NavProduct.css";


const NewProduct = () => {
  return (
    <div className="new_product">
      {/* reuse */}
      <div className="product_header">
        <div className="product_header_title">
          <div className="product_header_icon">
            <FontAwesomeIcon icon={faCartShopping} />
          </div>
          <span className="product_header_name">Sản phẩm mới</span>
        </div>
        <div className="view_more">
          <Link to={"/aa"} className="view_more_link">Xem thêm ...</Link>
        </div>
      </div>
      <div className="new_product_body">

      </div>
    </div>
  );
};

export default NewProduct;
