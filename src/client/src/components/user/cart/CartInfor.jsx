import React, { useState } from "react";
import "./Cart.css";
import { Table } from "react-bootstrap";
import { Link, useNavigate } from "react-router-dom";
import { useCart } from "react-use-cart";
import { useSelector } from "react-redux";
import Popup from "../../common/Popup";

const CartInfor = () => {
  const { cartTotal } = useCart();
  const formatCurrency = (number) => {
    return number.toLocaleString("vi-VN", {
      style: "currency",
      currency: "VND",
    });
  };

  const currentUser = useSelector((state) => state.auth.login.currentUser);
  const [popupMessage, setPopupMessage] = useState("");
  const [popupVisible, setPopupVisible] = useState(false);
  const navigate = useNavigate();

  // kiem tra trang thai da dang nhap chua
  const isLoggedIn = !!currentUser;
  const handleCheckout = () => {
    if (!isLoggedIn) {
      setPopupMessage("Bạn cần phải thực hiện đăng nhập để thanh toán");
      setPopupVisible(true);
    } else {
      navigate(`/checkout`); // Chuyển hướng đến trang thanh toán
    }
  };

  const handleCancel = () => {
    setPopupVisible(false);
  };
  const handleConfirm = async () => {
    navigate(`/login`);
    setPopupVisible(false);
  };

  return (
    <section className="d-flex justify-content-end">
      <div className="cart_bottom">
        <div className="cart_bottom_title">Tổng cộng tiền giỏ hàng</div>
        <tr>
          <td className="cart-lable w-100">Tổng tiền</td>
          <td className="cart-value w-100 text_total_price">
            {formatCurrency(cartTotal)}
          </td>
        </tr>
        <button
          className="btn btn-success cart_update mt-3"
          onClick={handleCheckout}
        >
          Tiến hành thanh toán
        </button>
      </div>
      {popupVisible && (
        <Popup
          message={popupMessage}
          onCancel={handleCancel}
          onConfirm={handleConfirm}
        />
      )}
    </section>
  );
};
export default CartInfor;
