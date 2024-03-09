import React, { useEffect, useState } from "react";
import { Link, useParams } from "react-router-dom";
import "../../../styles/user/ProductDetail.css";
import dautay from "../../../assets/mutdau.jpg";
import { Image } from "react-bootstrap";
import { getDetailProductByUrlSlug } from "../../../api/Product";

const ProductDetails = () => {
  const params = useParams();
  const [productDetail, setProductDetail] = useState(null);


  const {slug} = params;

  useEffect(() => {
    document.title ="Chi tiết sản phẩm";
    getDetailProductByUrlSlug(slug).then((data) => {
      if (data){
        setProductDetail(data);
        console.log(data );

      }
      else{
        setProductDetail({});
      }
    });
  }, [slug])

  if (productDetail){
    return (
      <section>
        <div className="product_detail">
          <div className="product_detail_img col-5">
            <Image src={dautay} width={300} />
          </div>
          <div className="product_detail_content col-7">
            <div className="product_detail_title">{productDetail.name}</div>
            <div className="product_detail_price">{productDetail.price}</div>
            
            <div className="product_detail_desc">
              {productDetail.description}
            </div>
            <div className="product_detail_size">
              <label for="cars">Lựa chọn size: </label>
              <select name="cars" id="cars">
                <option value="volvo">S</option>
                <option value="saab">M</option>
                <option value="opel">L</option>
                <option value="audi">XL</option>
              </select>
            </div>
            <div className="product_detail_quantity_cart">
              <div className="product_detail_quantity">
                <span>Số lượng</span>
                <input type="number" min="1" max="100" />
              </div>
              <Link className="btn btn-success product_detail_addcart" to={'/cart'}>
                Thêm vào giỏ hàng
              </Link>
            </div>
            
          </div>
        </div>
      </section>
    );

  }

};
export default ProductDetails;
