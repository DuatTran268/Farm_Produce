import React from "react";
import DCategory from "../../data/DCategory";
import { Link } from "react-router-dom";
import "../../styles/Categories.css";
import { Image } from "react-bootstrap";
import icon from "../../assets/logo.png";

const Categoris = () => {
  return (
    <>
      <div className="category_wrapper">
        <div className="category_title">Danh mục sản phẩm</div>
        <div className="category_list">
          {DCategory.map((category, index) => {
            return (
              <div className="category_item" key={index}>
                <Link to={""} className="category_link">
                  <Image src={icon} alt="icon" className="icon" width={30} />
                  <div className="category_name">{category.name}</div>
                </Link>
              </div>
            );
          })}
        </div>
      </div>
    </>
  );
};
export default Categoris;
