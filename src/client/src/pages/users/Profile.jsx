import React, { useEffect, useState } from "react";
import LayoutClient from "../../components/user/common/LayoutClient";
import { Form } from "react-bootstrap";
import { getUserById } from "../../api/Account";
import { useParams } from "react-router-dom";

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

  return (
    <LayoutClient>
      <div className="profile">
        <h3 className="profile_title text-center py-3">
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
                disabled
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
                disabled
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
                disabled
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
                disabled
                name="email"
                title="Email"
                required
                value={user.address}
              />
            </div>
          </div>


          <h3 className="text-center mt-5 mb-3">Đơn hàng của bạn</h3>
          {user.orders.length === 0 ? (
            <h6>Không có đơn hàng nào của bạn</h6>
          ) : (
            <div>
              {user.orders.map((order, index) => (
                <div key={index}>
                  <p>Đơn hàng số {order.id}</p>
                  <p>Ngày đặt hàng: {order.dateOrder}</p>
                  <p>Tổng giá: {order.totalPrice}</p>
                  <p>Chi tiết đơn hàng:</p>
                  <ul>
                    {order.orderItems.map((item, itemIndex) => (
                      <li key={itemIndex}>
                        {item.quantity} x {item.productName} - {item.price} VNĐ
                      </li>
                    ))}
                  </ul>
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
