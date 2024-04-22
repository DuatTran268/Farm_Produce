import React from "react";
import "./Cart.css";
import { Button, Image } from "react-bootstrap";
import { useCart } from "react-use-cart";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import {
  faCartShopping,
  faPlus,
  faSubtract,
  faTrash,
} from "@fortawesome/free-solid-svg-icons";
import { Link } from "react-router-dom";
import CartInfor from "./CartInfor";
import imageNotFound from "../../../assets/imagenotfound.jpg";
const CartTable = () => {
  const {
    isEmpty,
    totalUniqueItems,
    items,
    totalItems,
    cartTotal,
    updateItemQuantity,
    removeItem,
    emptyCart,
  } = useCart();

  const formatCurrency = (number) => {
    return number.toLocaleString("vi-VN", {
      style: "currency",
      currency: "VND",
    });
  };

  if (isEmpty)
    return (
      <>
        <h1 className="text_titlecart mt-5 mb-5">
          Giỏ hàng của bạn không có gì
        </h1>
        <div className="text-center">
          <Link to={`/home`}>
            <Button className="btn_go_buyproduct mt-5 mb-5 text-center text-bg-success">
              Đi mua sản phẩm thôi
            </Button>
          </Link>
        </div>
      </>
    );

  return (
    <section className="py-4 container">
      <div className="row justify-content-center">
        <div className="col-12">
          <div className="d-flex justify-content-between">
            <h5 className="text-danger py-3">
              Sản phẩm: ({totalUniqueItems}) Tổng sản phẩm: ({totalItems})
            </h5>
            <div className="col-auto ">
              <Button
                className="btn btn-danger m-2"
                onClick={() => emptyCart()}
              >
                Xoá tất cả
                <FontAwesomeIcon icon={faTrash} className="ms-2" />
              </Button>
            </div>
          </div>
          <table className="table table-light table-hover m-0">
            <tbody>
              <tr className="bg_header_table">
                <td>Hình ảnh</td>
                <td>Tên sản phẩm</td>
                <td>Đơn giá</td>
                <td>Số lượng</td>
                <td>Tạm tính</td>
                <td>Xoá</td>
              </tr>
              {items.map((item, index) => {
                return (
                  <>
                    <tr key={index} className="align-middle">
                      <td>
                      {item.images && item.images.length > 0 ? (
                          <Image
                            src={`https://localhost:7047/${item.images[0].urlImage}`}
                            width={50}
                          />
                        ) : (
                          <Image src={imageNotFound} width={50} />
                        )}
                      </td>
                      <td>{item.name}</td>
                      <td>{formatCurrency(item.price)}</td>
                      <td>
                        <div className="td_count_quantity">
                          <FontAwesomeIcon
                            icon={faSubtract}
                            className="btn_count_quantity"
                            onClick={() =>
                              updateItemQuantity(
                                item.id,
                                item.quantity > 1 ? item.quantity - 1 : 1 // Giữ lại số lượng cũ nếu số lượng mới nhỏ hơn 1
                              )
                            }
                          >
                            -{" "}
                          </FontAwesomeIcon>

                          {/* show quantity */}
                          {/* <span className="px-2">{item.quantity}</span> */}
                          <input
                            className="input_value_quantity"
                            type="number"
                            min="1"
                            value={item.quantity}
                            onChange={(e) => {
                              const newQuantity = parseInt(e.target.value);
                              updateItemQuantity(
                                item.id,
                                newQuantity > 0 ? newQuantity : 1
                              ); // Đảm bảo số lượng là một số dương
                            }}
                          />

                          <FontAwesomeIcon
                            icon={faPlus}
                            className="btn_count_quantity"
                            onClick={() =>
                              updateItemQuantity(item.id, item.quantity + 1)
                            }
                          >
                            +{" "}
                          </FontAwesomeIcon>
                        </div>
                      </td>

                      <td>{formatCurrency(item.price * item.quantity)}</td>

                      <td>
                        <FontAwesomeIcon
                          icon={faTrash}
                          onClick={() => removeItem(item.id)}
                          className="text-danger ms-2"
                        >
                          Xoá
                        </FontAwesomeIcon>
                      </td>
                    </tr>
                  </>
                );
              })}
            </tbody>
          </table>
        </div>
      </div>
      <CartInfor />
    </section>
  );
};
export default CartTable;
