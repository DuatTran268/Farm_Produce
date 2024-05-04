import React, { useEffect, useState } from "react";
import LayoutClient from "../../components/user/common/LayoutClient";
import { Form, Table } from "react-bootstrap";
import { getUserById } from "../../api/Account";
import { useParams } from "react-router-dom";
import "../../styles/user/Profile.css";
import { format } from "date-fns";

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
        console.log("Check data infor: ", data);
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
                          <td>{order.orderStatusName}</td>
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
                                <p>{productItem.price}</p>
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
    </LayoutClient>
  );
};
export default Profile;
