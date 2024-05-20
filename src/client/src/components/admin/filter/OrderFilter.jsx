import React from "react";

import { Form } from "react-bootstrap";
import { useDispatch, useSelector } from "react-redux";
import { reset, updateId, updateName } from "../../../redux/OrderRedux";

const OrderFilter = () => {
  const orderFilter = useSelector((state) => state.orderFilter),
    dispatch = useDispatch();

  const handleReset = (e) => {
    dispatch(reset());
  };

  return (
    <div className="row mb-3">
      <Form method="get" onReset={handleReset} className="col-3">
        <Form.Group className="col-auto box_search">
          <Form.Label className="visually-hidden">Tên người dùng</Form.Label>
          <Form.Control
            className="filter_box"
            type="text"
            placeholder="Tên người đặt đơn"
            name="name"
            value={orderFilter.name}
            onChange={(e) => dispatch(updateName(e.target.value))}
          />
        </Form.Group>
      </Form>
      <Form method="get" onReset={handleReset} className="col-3">
        <Form.Group className="col-auto box_search">
          <Form.Label className="visually-hidden">ID đơn hàng</Form.Label>
          <Form.Control
            className="filter_box"
            type="text"
            placeholder="ID đơn hàng"
            name="id"
            value={orderFilter.id}
            onChange={(e) => dispatch(updateId(e.target.value))}
          />
        </Form.Group>
      </Form>
    </div>
  );
};
export default OrderFilter;
