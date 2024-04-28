import React, { useEffect, useState } from "react";
import "./Order.css";
import { useSnackbar } from "notistack";
import { useNavigate, useParams } from "react-router-dom";
import {
  createOrder,
  getComboboxPaymentMethod,
  getComboboxStatusOrder,
} from "../../../api/Order";
import { Button, Form, FormControl } from "react-bootstrap";
import BoxEdit from "../../admin/edit/BoxEdit";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faMessage, faMoneyBill } from "@fortawesome/free-solid-svg-icons";
import { useSelector } from "react-redux";
import { useCart } from "react-use-cart"; // Import useCart
import YourOrder from "./YourOrder";

const FormOrder = () => {
  const user = useSelector((state) => state.auth.login.currentUser);
  const { enqueueSnackbar } = useSnackbar();
  const navigate = useNavigate();
  const { slug } = useParams();
  let { id } = useParams();
  id = id ?? "";
  const [validated, setValidated] = useState(false);
  const { isEmpty, items, cartTotal, emptyCart } = useCart(); // Lấy thông tin giỏ hàng


  useEffect(() => {
    if (user.id) {
      setOrder((prevState) => ({
        ...prevState,
        id: user.id,
        applicationUserId: user.id,
        name: user.username,
        email: user.email,
      }));
    }
  }, [user]);

  useEffect(() => {
    const cartData = JSON.parse(localStorage.getItem("react-use-cart"));
    console.log("Check cart-data: ", cartData);

    if (cartData && cartData.items) {
      const orderItems = cartData.items.map((item) => ({
        productId: item.id,
        quantity: item.quantity,
        price: item.price,
      }));

      setOrder((prevState) => ({
        ...prevState,
        orders: [{ ...prevState.orders[0], orderItems }],
      }));
    }

    getComboboxPaymentMethod().then((data) => {
      if (data) {
        setFilterPayment({
          paymentMethodList: data.paymentMethodList,
        });
      } else {
        setFilterPayment({ paymentMethodList: [] });
      }
    });

    getComboboxStatusOrder().then((data) => {
      if (data) {
        console.log("Check order status: ", data);
        setFilterStatusOrder({
          orderStatusList: data.orderStatusList,
        });
      } else {
        setFilterStatusOrder({ orderStatusList: [] });
      }
    });
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
    },
    [filterPayment, setFilterPayment] = useState({ paymentMethodList: [] }),
    [filterStatusOrder, setFilterStatusOrder] = useState({
      orderStatusList: [],
    });

  const [order, setOrder] = useState(initialState);

  useEffect(() => {
    document.title = "Đặt hàng với chúng tôi";
  });

  const handleSubmit = async (e) => {
    e.preventDefault();
    if (e.currentTarget.checkValidity() === false) {
      e.stopPropagation();
      setValidated(true);
    } else {
      try {
      const totalPrice = cartTotal;

      const updatedOrder = { ...order };

      updatedOrder.orders[0].totalPrice = totalPrice;

        const data = JSON.stringify(order);
        console.log("Form data:", data);

        const response = await createOrder(id, data);
        if (response) {
          enqueueSnackbar("Cảm ơn bạn đặt hàng", {
            variant: "success",
          });
          navigate(`/home`);
          emptyCart(); // Xóa toàn bộ sản phẩm trong giỏ hàng sau khi thanh toán thành công
        } else {
          enqueueSnackbar("Đã xảy ra lỗi khi đặt hàng", {
            variant: "error",
          });
        }
      } catch (error) {
        console.error("Error:", error);
      }
    }
  };

  return (
    <section className="infor_order row col-12">
      <div className="infor_order_left col-6">
        <YourOrder />
      </div>
      <div className="infor_order_left col-6">
        <Form
          method="post"
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
                onChange={(e) =>
                  setOrder({ ...order, address: e.target.value })
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
                placeholder="Email"
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
              <Form.Select
                name="paymentMethodId"
                title="payment Method Id"
                value={order.orders[0].paymentMethodId}
                required
                onChange={(e) =>
                  setOrder({
                    ...order,
                    orders: [
                      { ...order.orders[0], paymentMethodId: e.target.value },
                    ],
                  })
                }
              >
                {filterPayment.paymentMethodList.length > 0 &&
                  filterPayment.paymentMethodList.map((item, index) => (
                    <option key={index} value={item.value}>
                      {item.text}
                    </option>
                  ))}
              </Form.Select>
            }
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
                value={order.orders[0].discountId}
                onChange={(e) =>
                  setOrder({
                    ...order,
                    orders: [
                      { ...order.orders[0], discountId: e.target.value },
                    ],
                  })
                }
              />
            }
            notempty={"Không được bỏ trống"}
          />

          <BoxEdit
            control={
              <Form.Control
                className="form_control_order"
                placeholder="Ngày đặt hàng"
                type="date"
                name="dateOrder"
                title="dateOrder"
                required
                value={order.orders[0].dateOrder}
                onChange={(e) =>
                  setOrder({
                    ...order,
                    orders: [{ ...order.orders[0], dateOrder: e.target.value }],
                  })
                }
              />
            }
            notempty={"Không được bỏ trống"}
          />

          <BoxEdit
            control={
              <Form.Select
                name="orderStatusId"
                title="order Status Id"
                value={order.orders[0].orderStatusId}
                required
                // disabled={true}
                onChange={(e) =>
                  setOrder({
                    ...order,
                    orders: [
                      { ...order.orders[0], orderStatusId: e.target.value },
                    ],
                  })
                }
              >
                {filterStatusOrder.orderStatusList.length > 0 &&
                  filterStatusOrder.orderStatusList.map((item, index) => (
                    <option
                      key={index}
                      value={item.value}
                    >
                      {item.text}
                    </option>
                  ))}
              </Form.Select>
            }
          />

          <Form.Control
            type="hidden"
            name="applicationUserId"
            value={order.applicationUserId}
          />


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
            </React.Fragment>
          ))}
          <div className="text-center">
            <Button variant="success" type="submit">
              Thanh toán
              <FontAwesomeIcon icon={faMoneyBill} className="px-2" />
            </Button>
          </div>
        </Form>
      </div>
    </section>
  );
};
export default FormOrder;
