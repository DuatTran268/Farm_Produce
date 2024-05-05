import React, { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { getCategoryBySlugOfItSelf } from "../../../api/Category";

const CategoryName = () => {
  const [productUrlName, setProductUrlName] = useState([]);
  const params = useParams();
  const { slug } = params;

  useEffect(() => {
    getCategoryBySlugOfItSelf(slug).then((data) => {
      if (data) {
        setProductUrlName(data);
      } else {
        setProductUrlName({});
      }
    });
  }, [slug]);

  useEffect(() => {
    document.title = `Danh mục sản phẩm ${productUrlName.name || ''}`;
  }, [productUrlName.name]);
  return (
    <>
      <h3 className="text-success text-center py-3">Danh mục sản phẩm: {productUrlName.name}</h3>
    </>
  );
};
export default CategoryName;
