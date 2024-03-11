import React, { useEffect, useState } from "react";
import DCategory from "../../../data/DCategory";
import { Link } from "react-router-dom";
import "../../../styles/user/Categories.css"
import { Image } from "react-bootstrap";
import icon from "../../../assets/logo.png";
import { getCategoryLimit } from "../../../api/Category";

const Categoris = () => {
  const [getCategories, setGetCategory] = useState([]);

  useEffect (() => {
    getCategoryLimit().then((data) => {
      if (data){
        setGetCategory(data);
        console.log("Check data category", data)
      }
      else{
        setGetCategory([]);
      }
    });
  }, []);





  return (
    <>
      <div className="category_wrapper">
        <div className="category_title">Danh mục sản phẩm</div>
        <div className="category_list">
          {getCategories.map((category, index) => {
            return (
              <div className="category_item" key={index}>
                <Link to={`/category/${category.urlSlug}`}className="category_link">
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
