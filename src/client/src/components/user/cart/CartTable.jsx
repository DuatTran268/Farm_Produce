import React from "react";
import "../../../styles/user/Cart.css";
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
        <h1 className="text-center mt-5 mb-5">Giỏ hàng của bạn không có gì</h1>
        <div className="text-center">
          <Link to={`/home`}>
            <Button className="btn btn-success mt-5 mb-5 text-center">
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
          <h5 className="text-danger py-3">
            Sản phẩm: ({totalUniqueItems}) Tổng sản phẩm: ({totalItems})
          </h5>
          <table className="table table-light table-hover m-0">
            <tbody>
              <tr className="text-success">
                <td>Hình ảnh</td>
                <td>Tên sản phẩm</td>
                <td>Giá</td>
                <td>Số lượng</td>
                <td></td>
                <td>Xoá</td>
              </tr>
              {items.map((item, index) => {
                // if (!item || !item.id) {
                //   return null; // Bỏ qua nếu item không tồn tại hoặc không có thuộc tính id
                // }

                // const itemId = item?.id;
                return (
                  <>
                    <tr key={index} className="align-middle">
                      <td>
                        {/* <img
                          src={item.image}
                          style={{ height: "6rem" }}
                          alt={item.name}
                        /> */}
                      </td>
                      <td>{item.name}</td>
                      <td>{item.price} USD</td>
                      <td>{item.quantity}</td>
                      <td>
                        <FontAwesomeIcon
                          icon={faSubtract}
                          className="btn btn-info ms-2"
                          onClick={() =>
                            updateItemQuantity(item.id, item.quantity - 1)
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
        <div className="col-auto ms-auto">
          <h3 className="text-success mt-2">Tổng tiền: {cartTotal} USD</h3>
        </div>
        <div className="col-auto">
          <Button className="btn btn-danger m-2" onClick={() => emptyCart()}>
            Xoá tất cả
            <FontAwesomeIcon icon={faTrash} className="ms-2" />
          </Button>
          <Button className="btn btn-success">
            Đặt ngay
            <FontAwesomeIcon icon={faCartShopping} className="ms-2" />
          </Button>
        </div>
      </div>
    </section>
  );
};
export default CartTable;
