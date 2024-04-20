import React from "react";
import "./Cart.css";
import { Button } from "react-bootstrap";
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

  if (isEmpty)
    return (
      <>
        <h1 className="text_titlecart mt-5 mb-5">Giỏ hàng của bạn không có gì</h1>
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
                <td>Tên sản phẩm</td>
                <td>Giá</td>
                <td>Số lượng</td>
                <td></td>
                <td>Xoá</td>
              </tr>
              {items.map((item, index) => {
                return (
                  <>
                    <tr key={index} className="align-middle">
                      <td>{item.name}</td>
                      <td>{item.price} VNĐ</td>
                      <td>{item.quantity}</td>
                      <td>
                        <FontAwesomeIcon
                          icon={faSubtract}
                          className="btn btn-info ms-2"
                          onClick={() =>
                            updateItemQuantity(
                              item.id,
                              item.quantity > 1 ? item.quantity - 1 : 1 // Giữ lại số lượng cũ nếu số lượng mới nhỏ hơn 1
                            )
                          }
                        >
                          -{" "}
                        </FontAwesomeIcon>

                        <FontAwesomeIcon
                          icon={faPlus}
                          className="btn btn-info ms-2"
                          onClick={() =>
                            updateItemQuantity(item.id, item.quantity + 1)
                          }
                        >
                          +{" "}
                        </FontAwesomeIcon>
                      </td>
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
