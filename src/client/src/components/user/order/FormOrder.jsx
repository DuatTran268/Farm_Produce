import React, { useEffect, useState } from "react";
import "./Order.css";
import { useSnackbar } from "notistack";
import { useNavigate, useParams } from "react-router-dom";
import { createOrder } from "../../../api/Order";
import { Button, Form } from "react-bootstrap";
import BoxEdit from "../../admin/edit/BoxEdit";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faMessage } from "@fortawesome/free-solid-svg-icons";
import { useSelector } from "react-redux";
import { useCart } from "react-use-cart";

const FormOrder = () => {
  const user = useSelector((state) => state.auth.login.currentUser);
  const { enqueueSnackbar } = useSnackbar();
  const navigate = useNavigate();
  const { slug } = useParams();
  let { id } = useParams();
  id = id ?? "";
  const [validated, setValidated] = useState(false);

  //Lấy thông tin người dùng từ localStorage và điền vào biểu mẫu

  useEffect(() => {
    if (user.id) {
      setOrder((prevState) => ({ ...prevState, id: user.id }));
      setOrder((prevState) => ({ ...prevState, applicationUserId: user.id }));
      setOrder((prevState) => ({ ...prevState, name: user.username }));
      setOrder((prevState) => ({ ...prevState, email: user.email }));
    }
  }, [user]);

  useEffect(() => {
    // Lấy dữ liệu giỏ hàng từ localStorage
    const cartData = JSON.parse(localStorage.getItem("react-use-cart"));
    console.log("Check cart-data: ", cartData);

    if (cartData && cartData.items) {
      // Tạo một mảng orderItems từ các mục hàng trong giỏ hàng
      const orderItems = cartData.items.map((item) => ({
        productId: item.id,
        quantity: item.quantity,
        price: item.price,
        // Thêm các trường khác tùy thuộc vào cấu trúc của thông tin sản phẩm trong giỏ hàng
      }));

      console.log("Check orderitem", orderItems);

      // Cập nhật state của biểu mẫu với thông tin sản phẩm từ giỏ hàng
      setOrder((prevState) => ({
        ...prevState,
        orders: [{ ...prevState.orders[0], orderItems }],
      }));
    }
  }, []);

  const initialState = {
    id: "",
    name: "",
    email: "",
    address: "",
    phoneNumber: "",
    orders: [
      {
        id: 0,
        dateOrder: "",
        totalPrice: 0,
        orderStatusId: 0,
        applicationUserId: "",
        paymentMethodId: 0,
        discountId: 0,
        orderItems: [
          {
            id: 0,
            productId: 0,
            quantity: 0,
            price: 0,
          },
        ],
      },
    ],
  };
  const [order, setOrder] = useState(initialState);

  useEffect(() => {
    document.title = "Đặt hàng với chúng tôi";
  });

  const handleSubmit = (e) => {
    e.preventDefault();
    if (e.currentTarget.checkValidity() === false) {
      e.stopPropagation();
      setValidated(true);
    } else {
      let data = new FormData(e.target);

      console.log("Form data:", data);
      for (const pair of data.entries()) {
        console.log("Check form: ", pair[0] + ": " + pair[1]);
      }

      createOrder(id, data).then((data) => {
        if (data) {
          enqueueSnackbar("Cảm ơn bạn đặt hàng", {
            variant: "success",
          });
          navigate(`/home`);
        } else {
          enqueueSnackbar("Đã xảy ra lỗi khi đặt hàng", {
            variant: "error",
          });
        }
      });
    }
  };

  const { isEmpty, items, cartTotal } = useCart();

  return (
    <section className="checkout_row row">
      <Form
        method="post"
        encType="multipart/form-data"
        onSubmit={handleSubmit}
        noValidate
        validated={validated}
      >
        <Form.Control type="hidden" name="id" value={order.id} />

        <BoxEdit
          control={
            <Form.Control
              className="form_control_order"
              placeholder="Họ và tên"
              type="text"
              name="name"
              title="Name"
              required
              value={order.name}
              onChange={(e) => setOrder({ ...order, name: e.target.value })}
            />
          }
          notempty={"Không được bỏ trống"}
        />
        <BoxEdit
          control={
            <Form.Control
              className="form_control_order"
              placeholder="Địa chỉ"
              type="text"
              name="address"
              title="Address"
              required
              value={order.address}
              onChange={(e) => setOrder({ ...order, address: e.target.value })}
            />
          }
          notempty={"Không được bỏ trống"}
        />

        <BoxEdit
          control={
            <Form.Control
              className="form_control_order"
              placeholder="Số điện thoại"
              type="text"
              name="phoneNumber"
              title="Phone Number"
              required
              value={order.phoneNumber}
              onChange={(e) =>
                setOrder({ ...order, phoneNumber: e.target.value })
              }
            />
          }
          notempty={"Không được bỏ trống"}
        />

        <BoxEdit
          control={
            <Form.Control
              className="form_control_order"
              placeholder="Số điện thoại"
              type="email"
              name="email"
              title="Email"
              required
              value={order.email}
              onChange={(e) => setOrder({ ...order, email: e.target.value })}
            />
          }
          notempty={"Không được bỏ trống"}
        />

        <BoxEdit
          control={
            <Form.Control
              className="form_control_order"
              placeholder="Phương thức thanh toán"
              type="text"
              name="paymentMethodId"
              title="Payment MethodId"
              required
              value={order.orders.paymentMethodId}
              onChange={(e) =>
                setOrder({ ...order, paymentMethodId: e.target.value })
              }
            />
          }
          notempty={"Không được bỏ trống"}
        />

        <BoxEdit
          control={
            <Form.Control
              className="form_control_order"
              placeholder="Mã giảm giá"
              type="text"
              name="discountId"
              title="discountId"
              required
              value={order.orders.discountId}
              onChange={(e) =>
                setOrder({ ...order, discountId: e.target.value })
              }
            />
          }
          notempty={"Không được bỏ trống"}
        />

        <BoxEdit
          control={
            <Form.Control
              className="form_control_order"
              placeholder="Mã giảm giá"
              type="date"
              name="dateOrder"
              title="dateOrder"
              required
              value={order.orders.dateOrder}
              onChange={(e) =>
                setOrder({ ...order, dateOrder: e.target.value })
              }
            />
          }
          notempty={"Không được bỏ trống"}
        />

        <BoxEdit
          control={
            <Form.Control
              className="form_control_order"
              placeholder="Trạng thái đơn hàng"
              type="text"
              name="orderStatusId"
              title="orderStatusId"
              required
              value={order.orders.orderStatusId}
              onChange={(e) =>
                setOrder({ ...order, orderStatusId: e.target.value })
              }
            />
          }
          notempty={"Không được bỏ trống"}
        />

        <Form.Control
          type="hidden"
          name="applicationUserId"
          value={order.applicationUserId}
        />

        <Form.Control type="hidden" name="totalPrice" value={cartTotal} />

        {/* Các trường thông tin sản phẩm (ẩn) */}
        {order.orders[0].orderItems.map((item, index) => (
          <React.Fragment key={index}>
            <Form.Control
              type="hidden"
              name={`orderItems[${index}].productId`}
              value={item.productId}
            />
            <Form.Control
              type="hidden"
              name={`orderItems[${index}].quantity`}
              value={item.quantity}
            />
            <Form.Control
              type="hidden"
              name={`orderItems[${index}].price`}
              value={item.price}
            />
            {/* Thêm các trường ẩn khác cho thông tin sản phẩm */}
          </React.Fragment>
        ))}

        <div className="text-center">
          <Button variant="success" type="submit">
            Thanh toán
            <FontAwesomeIcon icon={faMessage} className="px-2" />
          </Button>
        </div>
      </Form>
    </section>
  );
};
export default FormOrder;
