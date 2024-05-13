import React, { useEffect, useState } from "react";
import LayoutClient from "../../components/user/common/LayoutClient";
import { Form, Table } from "react-bootstrap";
import { getUserById } from "../../api/Account";
import { useParams } from "react-router-dom";
import "../../styles/user/Profile.css";
import { format } from "date-fns";
import { deleteOrder } from "../../api/Order";
import { enqueueSnackbar } from "notistack";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faCancel, faTrash } from "@fortawesome/free-solid-svg-icons";
import Popup from "../../components/common/Popup";
import { Button } from "react-bootstrap";

const Profile = () => {
  const initialState = {
    id: "",
    name: "",
    email: "",
    address: "",
    orders: [],
  };

  const [user, setUser] = useState(initialState);
  let { id } = useParams();
  id = id ?? "";

  useEffect(() => {
    document.title = "Trang cá nhân";
    getUserById(id).then((data) => {
      if (data) {
        setUser({
          ...data,
        });
      } else {
        setUser(initialState);
      }
    });
  }, []);

  const formatCurrency = (number) => {
    return number.toLocaleString("vi-VN", {
      style: "currency",
      currency: "VND",
    });
  };

  const [IdToDelete, setIdToDelete] = useState(null);
  const [popupMessage, setPopupMessage] = useState("");
  const [popupVisible, setPopupVisible] = useState(false);
  const [reRender, setRender] = useState(false);

  const handleDelete = (order) => {
    const { id, orderStatusName } = order;
    console.log("Check order status name", orderStatusName);
    if (orderStatusName === "Chờ xác nhận") {
      setIdToDelete(id);
      setPopupMessage("Bạn có muốn huỷ đơn hàng này?");
      setPopupVisible(true);
    } else {
      enqueueSnackbar("Không thể huỷ vì " + orderStatusName + " đơn hàng.", {
        variant: "warning",
      });
    }
  };

  const handleConfirmDelete = async () => {
    const response = await deleteOrder(IdToDelete);
    if (response) {
      enqueueSnackbar("Đã xoá thành công", {
        variant: "success",
      });
      // Cập nhật danh sách đơn hàng sau khi xoá thành công
      setUser((prevUser) => ({
        ...prevUser,
        orders: prevUser.orders.filter((order) => order.id !== IdToDelete),
      }));
      // Kích hoạt render lại component
      setRender((prev) => !prev);
    } else {
      enqueueSnackbar("Xoá thất bại", {
        variant: "error",
      });
    }
    setPopupVisible(false);
  };

  const handleCancelDelete = () => {
    setPopupVisible(false);
  };

  return (
    <LayoutClient>
      <div className="profile">
        <h3 className="profile_title py-3 text-title">
          Thông tin tài khoản của bạn
        </h3>
        <div className="profile_content">
          <div className="row justify-content-center">
            <div className="col-sm-6">
              <Form.Label className="col-sm-6 col-form-label">
                Mã số ID của bạn
              </Form.Label>
              <Form.Control
                type="text"
                readOnly
                name="id"
                title="Mã số"
                required
                value={user.id}
              />
            </div>
          </div>

          <div className="row justify-content-center">
            <div className="col-sm-6">
              <Form.Label className="col-sm-6 col-form-label">
                Tên của bạn
              </Form.Label>
              <Form.Control
                type="text"
                readOnly
                name="name"
                title="Name"
                required
                value={user.name}
              />
            </div>
          </div>

          <div className="row justify-content-center">
            <div className="col-sm-6">
              <Form.Label className="col-sm-6 col-form-label">
                Email của bạn
              </Form.Label>
              <Form.Control
                type="Email"
                readOnly
                name="email"
                title="Email"
                required
                value={user.email}
              />
            </div>
          </div>

          <div className="row justify-content-center">
            <div className="col-sm-6">
              <Form.Label className="col-sm-6 col-form-label">
                Địa chỉ của bạn
              </Form.Label>
              <Form.Control
                type="text"
                readOnly
                name="email"
                title="Email"
                required
                value={user.address}
              />
            </div>
          </div>

          <h3 className=" text-title">Đơn hàng đã đặt mua của bạn</h3>
          {user.orders.length === 0 ? (
            <h6 className="text-title">Không có đơn hàng nào của bạn</h6>
          ) : (
            <div className="row">
              {user.orders.map((order, index) => (
                <div key={index} className="col-6 card_order">
                  <div className="card_order_infor">
                    <h5 className="text-title">Mã đơn hàng: {order.id}</h5>
                    <Button className="btn-danger" onClick={() => handleDelete(order)}>
                      Huỷ đơn hàng
                      <FontAwesomeIcon className="px-2" icon={faCancel} color="white " />
                    </Button>
                    <Table>
                      <thead>
                        <tr>
                          <th>Ngày đặt</th>
                          <th>Trạng thái</th>
                          <th>Thanh toán</th>
                        </tr>
                      </thead>
                      <tbody>
                        <tr>
                          <td>
                            {format(new Date(order.dateOrder), "dd/MM/yyyy")}
                          </td>
                          <td className="text-danger">
                            {order.orderStatusName}
                          </td>
                          <td>{order.paymentMethodName}</td>
                        </tr>
                      </tbody>
                    </Table>

                    <Table>
                      <thead>
                        <tr>
                          <th>Sản phẩm</th>
                          <th>Đơn giá</th>
                          <th>Số lượng</th>
                        </tr>
                      </thead>
                      <tbody>
                        <tr>
                          <td>
                            {order.orderItems.map((productItem, i) => (
                              <div key={i}>
                                <p>{productItem.productName}</p>
                              </div>
                            ))}
                          </td>
                          <td>
                            {order.orderItems.map((productItem, i) => (
                              <div key={i}>
                                <p>{formatCurrency(productItem.price)}</p>
                              </div>
                            ))}
                          </td>
                          <td>
                            {order.orderItems.map((productItem, i) => (
                              <div key={i}>
                                <p>{productItem.quantity}</p>
                              </div>
                            ))}
                          </td>
                        </tr>
                      </tbody>
                    </Table>
                    <p className="text_total_price">
                      Tổng thanh toán: {formatCurrency(order.totalPrice)}
                    </p>
                  </div>
                </div>
              ))}
            </div>
          )}
        </div>
      </div>
      {popupVisible && (
        <Popup
          message={popupMessage}
          onCancel={handleCancelDelete}
          onConfirm={handleConfirmDelete}
        />
      )}
    </LayoutClient>
  );
};
export default Profile;
