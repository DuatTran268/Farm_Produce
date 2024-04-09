import React from "react";
import "./Product.css";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faCartShopping } from "@fortawesome/free-solid-svg-icons";
import { Link } from "react-router-dom";



const ProductHeader = ({name}) => {
  return (
    <>
      <div className="product_header">
        <div className="product_header_title">
          <div className="product_header_icon">
            <FontAwesomeIcon icon={faCartShopping} />
          </div>
          <span className="product_header_name">{name}</span>
        </div>
        <div className="view_more">
          <Link to={"/product/viewmore"} className="view_more_link">
            Xem thêm ...
          </Link>
        </div>
      </div>
    </>
  )
}
export default ProductHeader;