import React, { useEffect, useState } from "react";
import { Link, Navigate, useParams } from "react-router-dom";
import "../../../styles/user/ProductDetail.css";
import dautay from "../../../assets/mutdau.jpg";
import { Image } from "react-bootstrap";
import { getDetailProductByUrlSlug } from "../../../api/Product";
import { useCart } from "react-use-cart";


const ProductDetails = () => {
  // const params = useParams();
  // const [productDetail, setProductDetail] = useState(null);
  // const {slug} = params;
  // const { addItem } = useCart();

  // const handleAddToCart = () => {
  //   // Hàm xử lý khi người dùng nhấn vào nút "Thêm vào giỏ hàng"
  //   addItem({
  //     id: productDetail.id, // ID của sản phẩm
  //     name: productDetail.name, // Tên sản phẩm
  //     price: productDetail.price, // Giá sản phẩm
  //     quantity: parseInt(document.getElementById("quantity").value), // Số lượng
  //   });
  // };


  // useEffect(() => {
  //   document.title ="Chi tiết sản phẩm";
  //   getDetailProductByUrlSlug(slug).then((data) => {
  //     if (data){
  //       setProductDetail(data);
  //     }
  //     else{
  //       setProductDetail({});
  //     }
  //   });
  // }, [slug])

  // if (productDetail){
  //   return (
  //     <section>
  //       <div className="product_detail">
  //         <div className="product_detail_img col-5">
  //           <Image src={dautay} width={300} />
  //         </div>
  //         <div className="product_detail_content col-7">
  //           <div className="product_detail_title">{productDetail.name}</div>
  //           <div className="product_detail_price">{productDetail.price}</div>
            
  //           <div className="product_detail_desc">
  //             {productDetail.description}
  //           </div>
  //           <div className="product_detail_size">
  //             <label for="cars">Lựa chọn size: </label>
  //             <select name="cars" id="cars">
  //               <option value="volvo">S</option>
  //               <option value="saab">M</option>
  //               <option value="opel">L</option>
  //               <option value="audi">XL</option>
  //             </select>
  //           </div>
  //           <div className="product_detail_quantity_cart">
  //             <div className="product_detail_quantity" >
  //               <span>Số lượng</span>
  //               <input type="number" min="1" max="100"  id="quantity"/>
  //             </div>
  //             <Link className="btn btn-success product_detail_addcart"  onClick={
  //               handleAddToCart
  //               }>
  //               Thêm vào giỏ hàng
  //             </Link>
  //           </div>
  //         </div>
  //       </div>
  //     </section>
  //   );
  // }


  const params = useParams();
  const [productDetail, setProductDetail] = useState(null);
  const { slug } = params;
  const { addItem } = useCart(); // Truy cập hàm addItem từ useCart

  useEffect(() => {
    document.title = "Chi tiết sản phẩm";
    getDetailProductByUrlSlug(slug).then((data) => {
      if (data) {
        setProductDetail(data);
      } else {
        setProductDetail({});
      }
    });
  }, [slug]);

  const handleAddToCart = () => {
    // Hàm xử lý khi người dùng nhấn vào nút "Thêm vào giỏ hàng"
    addItem({
      id: productDetail.id, // ID của sản phẩm
      name: productDetail.name, // Tên sản phẩm
      price: productDetail.price, // Giá sản phẩm
      quantity: parseInt(document.getElementById("quantity").value), // Số lượng
    });
  };

  if (productDetail) {
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
              <label htmlFor="size">Lựa chọn size: </label>
              <select name="size" id="size">
                <option value="S">S</option>
                <option value="M">M</option>
                <option value="L">L</option>
                <option value="XL">XL</option>
              </select>
            </div>
            <div className="product_detail_quantity_cart">
              <div className="product_detail_quantity">
                <span>Số lượng</span>
                <input type="number" id="quantity" min="1" max="100" />
              </div>
              <button
                className="btn btn-success product_detail_addcart"
                onClick={handleAddToCart} // Gọi hàm xử lý khi nhấn vào nút "Thêm vào giỏ hàng"
              >
                Thêm vào giỏ hàng
              </button>
            </div>
          </div>
        </div>
      </section>
    );
  }
};
export default ProductDetails;
