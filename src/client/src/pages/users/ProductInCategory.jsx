import React, { useEffect, useState } from "react";
import "../../components/user/product/Product.css";
import { Image } from "react-bootstrap";
import { useParams } from "react-router-dom";
import { getProductByCategorySlug } from "../../api/Product";
import BannerProductList from "../../assets/banner_product_list.png";
import LayoutClient from "../../components/user/common/LayoutClient";
import CategoryName from "../../components/user/categories/CategoryNames";
import ProductTemplate from "../../components/user/product/ProductTemplate";
import imagenotfound from "../../assets/imagenotfound.jpg";

const ProductInCategory = () => {
  const [productCategory, setProductCategory] = useState([]);

  const params = useParams();
  const { slug } = params;

  useEffect(() => {
    getProductByCategorySlug(slug).then((data) => {
      if (data) {
        setProductCategory(data.items);

        console.log("Checkkkk set data: ", data.items);

      } else {
        setProductCategory([]);
      }
    });
  }, [slug]);

  const getThumbnailUrl = (product) => {
    if (product.images && product.images.length > 0) {
      return product.images[0].urlImage; // Trả về URL của ảnh đầu tiên trong mảng images của sản phẩm
    } else {
      return  // Trả về URL của ảnh mặc định nếu không có ảnh trong mảng images
    }
  };

  return (
    <LayoutClient>
      <div className="product_body">
        <CategoryName />
        <div className="product_body_flex">
          <div className="product_list">

            {productCategory.length > 0 ? (
              <>
                {productCategory.map((product, index) => {
                  return (
                    <div className="product_item col-11 col-md-6 col-lg-3 " key={index}>
                      <ProductTemplate
                          item={product} 
                        urlSlug={product.urlSlug}
                        name={product.name}
                        priceVirtual={product.priceVirtual}
                        price={product.price}
                        thumbnailUrl={getThumbnailUrl(product)}
                        unit={product.unit.name}
                      />
                    </div>
                  );
                })}
              </>
            ) : (
              <>
                <h3 className="text-danger px-3">Không có sản phẩm nào </h3>
              </>
            )}


          </div>
        </div>
      </div>
    </LayoutClient>
  );
};

export default ProductInCategory;
