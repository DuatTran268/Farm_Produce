import React, { useEffect, useState } from "react";
import LayoutCommon from "../../../components/admin/common/LayoutCommon";
import { useSnackbar } from "notistack";
import { useNavigate, useParams } from "react-router-dom";
import {
  UpdateInforOrder,
  getComboboxStatusOrder,
  getOrderById,
} from "../../../api/Order";
import { Button, Form, Tab, Table } from "react-bootstrap";
import BoxEdit from "../../../components/admin/edit/BoxEdit";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faRightFromBracket, faSave } from "@fortawesome/free-solid-svg-icons";
import BtnError from "../../../components/common/BtnError";
import "./AdOrder.css";
import { format } from "date-fns";

const AdOrderEdit = () => {
  const { enqueueSnackbar, closeSnackbar } = useSnackbar();
  const navigate = useNavigate();

  const initialState = {
      id: 0,
      orderStatusId: 0,
      address: "",
      phoneNumber: "",
    },
    [orderInfor, setOrderInfor] = useState(initialState),
    [filterStatusOrder, setFilterStatusOrder] = useState({
      orderStatusList: [],
    });

  let { id } = useParams();
  id = id ?? 0;

  const formatCurrency = (number) => {
    if (number) {
        return number.toLocaleString("vi-VN", {
            style: "currency",
            currency: "VND",
        });
    } else {
        return ""; // hoặc giá trị mặc định khác tùy thuộc vào yêu cầu của bạn
    }
};
  useEffect(() => {
    document.title = "Chi tiết đơn hàng";
    getOrderById(id).then((data) => {
      console.log("Check id: ", id);
      if (data) {
        setOrderInfor(data);
        console.log("Check data: ", data);
      } else {
        setOrderInfor(initialState);
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

  const [validated, setValidated] = useState(false);
  const handleSubmit = async (e) => {
    e.preventDefault();
    if (e.currentTarget.checkValidity() === false) {
      e.stopPropagation();
      setValidated(true);
    } else {
      try {
        const data = JSON.stringify(orderInfor);
        const response = await UpdateInforOrder(data);
        if (response) {
          enqueueSnackbar("Thay đổi thông tin thành công", {
            variant: "success",
          });
          navigate(`/admin/order`);
        } else {
          enqueueSnackbar("Đã xảy ra lỗi khi thực hiện", {
            variant: "error",
          });
        }
      } catch (error) {
        console.error("Error:", error);
      }
    }
  };

  return (
    <LayoutCommon>
      <div className="wrapper">
        <h3 className="text-success py-3">Chi tiết đơn hàng</h3>
      </div>

      <Form
        method="post"
        onSubmit={handleSubmit}
        noValidate
        validated={validated}
      >
        <h5 className="text-success py-3">Thông tin thay đổi</h5>
        <Form.Control type="hidden" name="id" value={orderInfor.id} />
        {/* <BoxEdit
          label={"Địa chỉ"}
          control={
            <Form.Control
              className="form_control_orderInfor"
              placeholder="Họ và tên"
              type="text"
              name="address"
              title="Address"
              required
              value={orderInfor.address}
              onChange={(e) =>
                setOrderInfor({ ...orderInfor, address: e.target.value })
              }
            />
          }
          notempty={"Không được bỏ trống"}
        />
        <BoxEdit
          label={"Số điện thoại"}
          control={
            <Form.Control
              className="form_control_orderInfor"
              placeholder="Số điện thoại"
              type="number"
              name="phoneNumber"
              title="PhoneNumber"
              required
              value={orderInfor.phoneNumber}
              onChange={(e) =>
                setOrderInfor({ ...orderInfor, phoneNumber: e.target.value })
              }
            />
          }
          notempty={"Không được bỏ trống"}
        /> */}

        <BoxEdit
          label={"Trạng thái đơn hàng"}
          control={
            <Form.Select
              name="orderStatusId"
              title="order Status Id"
              value={orderInfor.orderStatusId}
              required
              onChange={(e) =>
                setOrderInfor({
                  ...orderInfor,
                  orderStatusId: e.target.value, // Sửa lại thành e.target.value
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

        <div className="text-center">
          <Button variant="success" type="submit">
            Lưu các thay đổi
            <FontAwesomeIcon icon={faSave} className="px-1" />
          </Button>

          <BtnError
            icon={faRightFromBracket}
            slug={"/admin/order"}
            name="Hủy và quay lại"
          />
        </div>
      </Form>

      <h5 className="text-success py-3">Thông tin đơn hàng và sản phẩm</h5>
      <div className="detail_infor_order">
        <div className="row">
          <div className="col-6">
            <Table>
                <tr>
                  <td className="">Họ tên</td>
                  <td className="">{orderInfor.userName}</td>
                </tr>
                <tr>
                  <td className="">Số điện thoại</td>
                  <td className="">{orderInfor.phoneNumber}</td>
                </tr>
                <tr>
                  <td className="">Địa chỉ</td>
                  <td className="">{orderInfor.address}</td>
                </tr>
                <tr>
                  <td className="">Ngày đặt hàng</td>
                  {/* <td>{format(new Date(orderInfor.dateOrder), "dd/MM/yyyy")}</td> */}
                </tr>
                <tr>
                  <td className="">Phương thức thanh toán</td>
                  <td className="">{orderInfor.paymentMethodName}</td>

                </tr>
                <tr>
                  <td className="">Tổng tiền</td>
                  {/* <td className="">{formatCurrency(orderInfor.totalPrice)}</td> */}
                  <td className="">{orderInfor.totalPrice ? formatCurrency(orderInfor.totalPrice) : ""}</td>
                </tr>
            </Table>
          </div>
          <div className=" col-6">
            {orderInfor.orderItems ? (
              <>
                {orderInfor.orderItems.map((itemOrder, index) => (
                  <div key={index} className="infor_product">
                    <Table>
                      <tr>
                        <td className="w-50">Mã sản phẩm</td>
                        <td className="w-50">{itemOrder.id}</td>
                      </tr>
                      <tr>
                        <td className="">Tên Sản phẩm</td>
                        <td className="">{itemOrder.productName}</td>
                      </tr>
                      <tr>
                        <td className="">Đơn giá</td>
                        <td className="">{formatCurrency(itemOrder.price)}</td>
                      </tr>
                      <tr>
                        <td className="">Số lượng</td>
                        <td className="">{itemOrder.quantity}</td>
                      </tr>
                    </Table>
                  </div>
                ))}
              </>
            ) : (
              <div>Không có thông tin sản phẩm nào</div>
            )}
          </div>
        </div>
      </div>
    </LayoutCommon>
  );
};
export default AdOrderEdit;
