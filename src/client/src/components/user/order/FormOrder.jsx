import React, { useEffect, useState } from "react";
import "./Order.css";
import { useSnackbar } from "notistack";
import { useNavigate, useParams } from "react-router-dom";
import {
  createOrder,
  getComboboxPaymentMethod,
  getComboboxStatusOrder,
  getInforOfVoucherDiscount,
} from "../../../api/Order";
import { Button, Form, FormControl, Table } from "react-bootstrap";
import BoxEdit from "../../admin/edit/BoxEdit";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faMessage, faMoneyBill } from "@fortawesome/free-solid-svg-icons";
import { useSelector } from "react-redux";
import { useCart } from "react-use-cart"; // Import useCart
import YourOrder from "./YourOrder";
import { getDiscountByName } from "../../../api/Discount";

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
          // dateOrder: "",
          totalPrice: 0,
          orderStatusId: 0,
          applicationUserId: "",
          paymentMethodId: 0,
          discountId: "",
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
  const [selectedPaymentMethodId, setSelectedPaymentMethodId] = useState(0); // Thêm trạng thái tạm thời cho paymentMethodId

  useEffect(() => {
    document.title = "Đặt hàng với chúng tôi";
  });

  const [codeName, setCodeName] = useState("");
  const [errorMessage, setErrorMessage] = useState("");
  const [discountedTotal, setDiscountedTotal] = useState(0); // State để lưu giá trị tổng tiền sau khi giảm giá
  const handleSubmitDiscount = async () => {
    try {
      const voucherInfo = await getInforOfVoucherDiscount(codeName);

      // Kiểm tra xem có dữ liệu trả về từ API không
      if (voucherInfo) {
        const discountPrice = voucherInfo.discountPrice;
        console.log("Check discount price", discountPrice);

        // Kiểm tra nếu mã giảm giá hợp lệ
        if (discountPrice > 0) {
          const totalPriceAfterDiscount = cartTotal * (1 - discountPrice / 100);
          setDiscountedTotal(totalPriceAfterDiscount);
          setErrorMessage("");
        } else {
          // Nếu mã giảm giá không áp dụng, sử dụng giá trị tổng giỏ hàng ban đầu
          setDiscountedTotal(cartTotal);
          setErrorMessage("");
        }
      } else {
        setErrorMessage("Mã giảm giá không hợp lệ");
      }
    } catch (error) {
      console.error("Lỗi khi gửi yêu cầu đến API: ", error);
      setErrorMessage("Đã xảy ra lỗi khi xử lý yêu cầu");
    }
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    if (e.currentTarget.checkValidity() === false) {
      e.stopPropagation();
      setValidated(true);
    } else {
      try {
        // const totalPrice = discountedTotal;
        let totalPrice; // Khởi tạo biến totalPrice để lưu giá trị tổng giá tiền

        // ktra nhap vao ma giảm gia, neu nhap roi thi sử dụng discountedTotal
        if (discountedTotal !== 0) {
          totalPrice = discountedTotal;
        } else {
          // chưa nhập thì sử dụng cartTotal
          totalPrice = cartTotal;
        }

        const updatedOrder = { ...order };

        updatedOrder.orders[0].totalPrice = totalPrice;
        updatedOrder.orders[0].paymentMethodId = selectedPaymentMethodId; // Cập nhật paymentMethodId từ trạng thái tạm thời

        const data = JSON.stringify(order);
        console.log("Form data:", data);

        const response = await createOrder(id, data);
        if (response) {
          enqueueSnackbar("Cảm ơn bạn đặt hàng", {
            variant: "success",
          });
          navigate(`/checkout/orderinfor`);
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

  const formatCurrency = (number) => {
    return number.toLocaleString("vi-VN", {
      style: "currency",
      currency: "VND",
    });
  };

  return (
    <section className="infor_order row col-12">
      <div className="infor_order_left col-6">
        {/* <YourOrder /> */}
        <div className="checkout_title">Đơn hàng của bạn</div>
        <div className="checkout_order_content">
          <Table>
            <thead>
              <tr>
                <th>Sản phẩm</th>
                <th>Đơn giá</th>
                <th>Số lượng</th>
                <th>Thành tiền</th>
              </tr>
            </thead>
            <tbody>
              <tr>
                <td>
                  {items.map((item, index) => {
                    return (
                      <div className="name_product_table" key={index}>
                        {item.name}
                      </div>
                    );
                  })}
                </td>
                <td>
                  {items.map((item, index) => {
                    return <div key={index}>{formatCurrency(item.price)}</div>;
                  })}
                </td>
                <td>
                  {items.map((item, index) => {
                    return <div key={index}>{item.quantity}</div>;
                  })}
                </td>
                <td>
                  {items.map((item, index) => {
                    return (
                      <div key={index}>
                        {formatCurrency(item.price * item.quantity)}
                      </div>
                    );
                  })}
                </td>
              </tr>
              <tr>
                <td colSpan={3}>Tổng tiền</td>
                <td className="name_product_table">
                  {formatCurrency(cartTotal)}
                </td>
              </tr>
            </tbody>
          </Table>

          {/* discount cart input */}
          <div>
            <input
              type="text"
              value={codeName}
              onChange={(e) => setCodeName(e.target.value)}
              placeholder="Nhập mã giảm giá"
            />
            <button onClick={handleSubmitDiscount}>Áp dụng</button>
            {errorMessage && <p style={{ color: "red" }}>{errorMessage}</p>}
            <p className="text-danger">
              Số tiền phải thanh toán sau khi giảm giá: {formatCurrency(discountedTotal)}
            </p>
          </div>
        </div>
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
                className="form_control_order"
                name="paymentMethodId"
                title="payment Method Id"
                value={selectedPaymentMethodId}
                required
                onChange={(e) =>
                  setSelectedPaymentMethodId(parseInt(e.target.value))
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
                value={codeName}
                readOnly={true}
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

          {/* <BoxEdit
            control={
              <Form.Control
                className="form_control_order"
                placeholder="Ngày đặt hàng"
                type="date"
                name="dateOrder"
                title="dateOrder"
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
          /> */}

          <BoxEdit
            control={
              <Form.Select
                className="form_control_order"
                name="orderStatusId"
                title="order Status Id"
                value={order.orders[0].orderStatusId}
                required
                disabled={true}
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
                    <option key={index} value={item.value}>
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