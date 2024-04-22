import React, { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { getProductByCategorySlug } from "../../api/Product";
import { Image } from "react-bootstrap";
import BannerProductList from "../../assets/banner_product_list.png"
import ProductTemplate from "../../components/user/product/ProductTemplate";


const ProductCategoryList = () => {
  const [productCategory, setProductCategory] = useState([]);
  const params = useParams();
  const { slug } = params;

  useEffect(() => {
    getProductByCategorySlug('rau').then((data) => {
      if (data) {
        setProductCategory(data.items);
      } else {
        setProductCategory({});
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


   // Hàm định dạng giá tiền thành VNĐ
   const formatCurrency = (number) => {
    return number.toLocaleString("vi-VN", {
      style: "currency",
      currency: "VND",
    });
  };

  
  return (
      <div className="product_body">
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
                        price={product.price}
                        thumbnailUrl={getThumbnailUrl(product)}
                        priceVirtual={product.priceVirtual}
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
  );
}
export default ProductCategoryList;